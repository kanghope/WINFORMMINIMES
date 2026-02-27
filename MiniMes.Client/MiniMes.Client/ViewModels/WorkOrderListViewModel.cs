using MiniMes.Client.ViewModels;
using MiniMes.Domain.Commons;
using MiniMes.Domain.DTOs;
using MiniMes.Infrastructure.Interfaces;
using MiniMes.Infrastructure.Services;
using MiniMES.Infastructure.interfaces;
using MiniMES.Infastructure.Services;
using MiniMes.Domain.Auth;
using System;
using System.Collections.Generic; // ObservableCollection 대용으로 List 사용 가능성 대비
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing; // [수정] WPF의 System.Windows.Media.Brush 대신 WinForms는 System.Drawing.Color 사용
using System.Linq;
using System.Threading; // CancellationTokenSource를 위해 필요
using System.Threading.Tasks;

namespace MiniMes.Client.ViewModels
{

    // INotifyPropertyChanged: "내 데이터가 바뀌면 화면에 알려주겠다"는 약속입니다.
    /// <summary>
    /// [작업지시 목록 화면용 비즈니스 로직]
    /// WinForms의 DataGridView와 연결되어 데이터를 주고받는 핵심 클래스입니다.
    /// </summary>
    public class WorkOrderListViewModel : BaseViewModel
    {
        // [1. 도구들] DB와 통신하거나 설비와 연결하는 서비스들 (DI 주입 방식)
        private readonly IWorkOrderService _service;
        private readonly IWorkResultService _WorkResultsService;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly SerialDeviceService _serialDeviceService;

        // [추가] 연속 클릭 시 이전 비동기 작업을 취소하기 위한 소스
        private CancellationTokenSource? _loadCts;

        // [2. 데이터 저장소] 화면(WinForms)과 연결될 데이터들

        // [핵심 수정] ObservableCollection 대신 BindingList 사용 (WinForms 전용)
        public BindingList<WorkOrderDto> WorkOrders { get; } = new BindingList<WorkOrderDto>();
        // 실시간 통신 로그 및 상태 정보
        public BindingList<string> CommunicationLogs { get; } = new BindingList<string>();

        private string _deviceStatusText = "연결 대기 중";
        public string DeviceStatusText { get => _deviceStatusText; set { _deviceStatusText = value; OnPropertyChanged(); } }

        // [수정] WinForms는 Brush 대신 Color 또는 string(상태명)을 뷰모델에서 관리하는 것이 편합니다.
        private Color _deviceStatusColor = Color.Gray;
        public Color DeviceStatusColor { get => _deviceStatusColor; set { _deviceStatusColor = value; OnPropertyChanged(); } }

        private string _lastSignalTime = "-";
        public string LastSignalTime { get => _lastSignalTime; set { _lastSignalTime = value; OnPropertyChanged(); } }

        private int _packetCount = 0;
        public int PacketCount { get => _packetCount; set { _packetCount = value; OnPropertyChanged(); } }

        // 콤보박스용 리스트
        public ObservableCollection<StatusItem> StatusOptions { get; set; }

        private string? _selectedStatusCode;
        public string SelectedStatusCode
        {
            get => _selectedStatusCode;
            set
            {
                if (_selectedStatusCode != value)
                {
                    _selectedStatusCode = value;
                    OnPropertyChanged();
                    // [참고] WinForms에서는 값이 바뀔 때 자동으로 검색을 실행하게 하려면 여기서 메서드 호출
                }
            }
        }

        private WorkOrderDto? _selectedWorkOrder;
        public WorkOrderDto? SelectedWorkOrder
        {
            get => _selectedWorkOrder;
            set { _selectedWorkOrder = value; OnPropertyChanged();
                    // 선택이 변경될 때 버튼 활성화 상태 갱신
                    RefreshCommandStates();
                }
        }

        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(); 
                // 로딩 상태가 변경될 때 버튼 활성화 상태 갱신
                RefreshCommandStates();
            } }

        private string _statisticsSummary = "통계 대기 중...";
        public string StatisticsSummary { get => _statisticsSummary; set { _statisticsSummary = value; OnPropertyChanged(); } }

        private double? _LoadingProgress;
        public double? LoadingProgress { get => _LoadingProgress; set { _LoadingProgress = value; OnPropertyChanged(); } }

        // 검색 기간 및 검색어
        // 검색 기간 설정
        private DateTime _startDate = DateTime.Now.AddDays(-30); // 기본 일주일 전
        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        // 검색어 (지시 ID나 품목코드)
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        // ---------------------------------------------------------
        // [추가된 로직: 모든 버튼의 활성화 상태 프로퍼티]
        // ---------------------------------------------------------

        /// <summary> 새로고침 & 통계계산: 로딩 중이 아닐 때만 가능 </summary>
        public bool CanRefreshOrCalculate => !IsLoading;

        /// <summary> 지시 등록: 로딩 중이 아닐 때만 가능 </summary>
        public bool CanAddWork => !IsLoading;

        /// <summary> 수정/삭제: 대기(W) 상태일 때만 가능 </summary>
        public bool CanEditOrDelete => SelectedWorkOrder != null && SelectedWorkOrder.Status == "W" && !IsLoading;

        /// <summary> 작업 시작: 대기(W) 상태일 때만 가능 </summary>
        public bool CanStartWork => SelectedWorkOrder != null && SelectedWorkOrder.Status == "W" && !IsLoading;

        /// <summary> 작업 종료: 진행중(P) 상태일 때만 가능 </summary>
        public bool CanStopWork => SelectedWorkOrder != null && SelectedWorkOrder.Status == "P" && !IsLoading;

        /// <summary> 실적 등록: 진행중(P) 상태일 때만 가능 </summary>
        public bool CanRegisterResult => SelectedWorkOrder != null && SelectedWorkOrder.Status == "P" && !IsLoading;

        /// <summary> 실적 조회: 선택된 항목이 있으면 가능 </summary>
        public bool CanViewResults => SelectedWorkOrder != null && !IsLoading;

        /// <summary>
        /// 모든 버튼의 활성화 상태(Property)를 화면에 다시 그리도록 알립니다.
        /// </summary>
        private void RefreshCommandStates()
        {
            OnPropertyChanged(nameof(CanRefreshOrCalculate));
            OnPropertyChanged(nameof(CanAddWork));
            OnPropertyChanged(nameof(CanEditOrDelete));
            OnPropertyChanged(nameof(CanStartWork));
            OnPropertyChanged(nameof(CanStopWork));
            OnPropertyChanged(nameof(CanRegisterResult));
            OnPropertyChanged(nameof(CanViewResults));
        }
        // ---------------------------------------------------------

        // [3. 이벤트 정의] 윈폼 화면에게 "창을 열어라"라고 시키는 통로 (Delegate 사용)
        // WPF의 Action 이벤트를 그대로 활용하여, View(Form)에서 구독하도록 합니다.
        public event Action<WorkOrderEditViewModel, string>? OpenEditWindowRequested;
        public event Action<WorkResultRegisterViewModel, string>? OpenRegisterWindowRequested;
        public event Action<WorkResultListViewModel, string>? OpenResultWindowRequested;
        public event Action<string, string, string>? ShowMessageRequested; // (메시지, 제목, 아이콘타입)

        /// <summary>
        /// [디자인 모드용 빈 생성자]
        /// </summary>
        public WorkOrderListViewModel() : this(new WorkOrderService(), new WorkResultService(), new WorkOrderRepository(), new SerialDeviceService())
        {
        }

        /// <summary>
        /// [실제 사용 생성자] 필요한 서비스를 주입받아 초기화합니다.
        /// </summary>
        public WorkOrderListViewModel(IWorkOrderService workOrderService, IWorkResultService workResultsService, IWorkOrderRepository workOrderRepository, SerialDeviceService serialDeviceService)
        {
            _service = workOrderService;
            _WorkResultsService = workResultsService;
            _workOrderRepository = workOrderRepository;
            _serialDeviceService = serialDeviceService ?? throw new ArgumentNullException(nameof(serialDeviceService));

            // 콤보박스 초기화
            StatusOptions = new ObservableCollection<StatusItem>
            {
                new StatusItem { DisplayName = "전체", StatusCode = "ALL" },
                new StatusItem { DisplayName = "대기", StatusCode = "W" },
                new StatusItem { DisplayName = "진행중", StatusCode = "P" },
                new StatusItem { DisplayName = "완료", StatusCode = "C" }
            };
            _selectedStatusCode = "ALL";//생성자에서는 필드에 직접 값을 세팅함

            // [핵심 수정] PLC 서비스 이벤트 구독 (WPF Dispatcher 제거)
            // WinForms는 이벤트를 받을 때 뷰모델은 데이터만 갱신하고, 
            // 실제 UI 조작은 View단에서 Control.Invoke를 사용하는 것이 정석입니다.
            InitServiceEvents();
        }

        private void InitServiceEvents()
        {
            _serialDeviceService.OnDataReceived += (data) => {
                InvokeOnUI(() => {

                    AddLog($"RX: {data}");
                    PacketCount++;
                    LastSignalTime = DateTime.Now.ToString("HH:mm:ss.fff");
                });
                    
            };

            _serialDeviceService.OnDeviceStarted += (eqCode) => {
                InvokeOnUI(() => {
                    DeviceStatusText = $"{eqCode} 연결됨";
                    DeviceStatusColor = Color.LimeGreen;
                    AddLog($"SYSTEM: {eqCode} 설비와 통신이 시작되었습니다.");

                });
                
            };

            _serialDeviceService.OnRefreshRequired += (flag) => {
                InvokeOnUI(async () => {
                    await ExecuteLoadCommandAsync();
                });
                    

                
            };

            _serialDeviceService.OnConnectionStatusChanged += (isConnected) => {
                InvokeOnUI(() => {
                    DeviceStatusText = isConnected ? "🟢 설비 연결됨" : "🔴 연결 끊김 (재시도 중)";
                    DeviceStatusColor = isConnected ? Color.LimeGreen : Color.Red;
                    AddLog(isConnected ? "SYSTEM: 설비와 연결되었습니다." : "SYSTEM: 설비 연결이 해제되었습니다.");

                });
                
            };

   
            _serialDeviceService.OnWorkFinishedByDevice += async (eqCode) => {
                if (SelectedWorkOrder != null)
                {
                    await ExecuteStopWorkAsync();
                    ShowMessageRequested?.Invoke($"설비({eqCode}) 종료 신호로 자동 마감되었습니다.", "자동 종료", "Info");
                }
            };
        }

        // [4. 실제 행동 메서드들] (WPF의 RelayCommand 내부 로직을 일반 메서드로 전환)
        // 윈폼의 버튼 클릭 이벤트에서 아래 메서드들을 직접 호출합니다.

        /// <summary>
        /// 데이터를 비동기로 불러와 리스트를 채웁니다.
        /// </summary>
        public async Task ExecuteLoadCommandAsync()
        {
            if (_isLoading) return;

            _loadCts?.Cancel();
            _loadCts = new CancellationTokenSource();
            var token = _loadCts.Token;

            try
            {
                IsLoading = true;
                StatisticsSummary = "데이터를 불러오는 중...";

                // 서비스로부터 데이터 호출 (비동기)
                var data = await _service.GetAllWorkOrdersAsync(SelectedStatusCode, StartDate, EndDate, SearchText);
                var dataList = data.ToList();
                double totalCount = dataList.Count;

                if (token.IsCancellationRequested) return;

                WorkOrders.Clear();
                LoadingProgress = 0;

                // 백그라운드에서 통계 계산 (무거운 작업 분리)
                var statsTask = Task.Run(() =>
                {
                    var total = dataList.Sum(x => x.Quantity);
                    var complete = dataList.Count(x => x.StatusEnum == WorkOrderStatus.Complete);
                    return $"총 지시수량: {total:N0} | 완료 건수: {complete:N0}건";
                }, token);

                // 리스트 채우기 (배치 처리로 UI 멈춤 방지)
                int batchSize = 100;
                for (int i = 0; i < dataList.Count; i += batchSize)
                {
                    if (token.IsCancellationRequested) return;

                    var batch = dataList.Skip(i).Take(batchSize);
                    foreach (var item in batch) WorkOrders.Add(item);

                    if (totalCount > 0)
                        LoadingProgress = ((double)WorkOrders.Count / totalCount) * 100;

                    StatisticsSummary = $"{WorkOrders.Count:N0}건 로드 중...";
                    await Task.Delay(5); // WinForms UI 스레드에 메시지 루프 기회 제공
                }

               
                LoadingProgress = 100;

                //프로그래스바가 끝까지 차오르는것을 볼수있도록 잠시 대기
                await Task.Delay(300);

                StatisticsSummary = await statsTask;

            }
            catch (Exception ex)
            {
                ShowMessageRequested?.Invoke($"데이터 로드 오류: {ex.Message}", "오류", "Error");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // 삭제 로직 (MessageBox로 한 번 더 물어보기 추가)
        public async Task ExecuteDeleteCommandAsync()
        {
            if (SelectedWorkOrder == null) return;

            // [수정] WinForms용 MessageBox 사용
            if (MessageBox.Show("정말로 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await _service.DeleteWorkOrder(SelectedWorkOrder.Id);
                await ExecuteLoadCommandAsync();
                ShowMessageRequested?.Invoke("삭제되었습니다.", "성공", "Info");
            }
        }

        /// <summary>
        /// 새로운 작업 지시를 등록합니다.
        /// </summary>
        public async Task ExecuteAddCommandAsync()
        {
            var newDto = new WorkOrderDto { Id = 0, ItemCode = "", Quantity = 0, Status = "W" };
            var editVm = new WorkOrderEditViewModel(newDto);

            // 뷰에게 팝업창 요청
            OpenEditWindowRequested?.Invoke(editVm, "작업지시등록");

            if (editVm.IsSaved)
            {
                var savedDto = editVm.CommitChanges();
                await _service.CreateWorkOrder(savedDto);
                await ExecuteLoadCommandAsync();
                ShowMessageRequested?.Invoke("정상적으로 등록 되었습니다.", "성공", "Info");
            }
        }

        /// <summary>
        /// 선택된 작업을 시작하고 설비 통신(PLC)을 연결합니다.
        /// </summary>
        public async Task ExecuteStartWorkAsync()
        {
            if (SelectedWorkOrder == null || SelectedWorkOrder.Status != "W") return;

            try
            {
                IsLoading = true;
                // 1. DB 상태 변경
                await _workOrderRepository.StartWorkOrder(SelectedWorkOrder.Id, UserSession.UserId, "EQ01");

                // 2. PLC 통신 시작
                _serialDeviceService.Open("COM1");
                AddLog("SYSTEM: COM1 포트 Open 및 설비 가동 시작");

                await ExecuteLoadCommandAsync();
                ShowMessageRequested?.Invoke("작업 및 통신이 시작되었습니다.", "알림", "Info");
            }
            catch (Exception ex)
            {
                ShowMessageRequested?.Invoke($"설비 연결 실패: {ex.Message}", "오류", "Error");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 작업을 종료하고 통신을 닫습니다.
        /// </summary>
        public async Task ExecuteStopWorkAsync()
        {
            if (SelectedWorkOrder == null || SelectedWorkOrder.Status != "P") return;

            try
            {
                IsLoading = true;
                await _workOrderRepository.StopWorkOrder(SelectedWorkOrder.Id, UserSession.UserId);

                _serialDeviceService.Dispose(); // 통신 종료

                DeviceStatusText = "통신 종료";
                DeviceStatusColor = Color.Gray;
                AddLog("SYSTEM: 통신 포트를 닫았습니다.");

                await ExecuteLoadCommandAsync();
                ShowMessageRequested?.Invoke("작업이 정상 종료되었습니다.", "알림", "Info");
            }
            catch (Exception ex)
            {
                ShowMessageRequested?.Invoke($"종료 처리 오류: {ex.Message}", "오류", "Error");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task ExecuteCalculateStatsAsync()
        {
            if (WorkOrders.Count == 0) return;

            IsLoading = true;
            StatisticsSummary = "계산 중...";

            try
            {
                // 핵심: 10만 건의 데이터를 분석하는 무거운 작업은 Task.Run으로 백그라운드 팀에 맡깁니다.
                var result = await Task.Run(() =>
                {
                    // 이 안은 UI 스레드가 아니므로 마음껏 CPU를 써도 화면이 멈추지 않습니다.
                    var totalQty = WorkOrders.Sum(x => x.Quantity);
                    var waitCount = WorkOrders.Count(x => x.StatusEnum == WorkOrderStatus.Wait);
                    var processingCount = WorkOrders.Count(x => x.StatusEnum == WorkOrderStatus.Processing);
                    var completeCount = WorkOrders.Count(x => x.StatusEnum == WorkOrderStatus.Complete);

                    return $"총 수량: {totalQty:N0} | 대기: {waitCount}건, 진행: {processingCount}건, 완료: {completeCount}건";
                });

                // await 이후에는 다시 UI 스레드로 돌아오므로 안전하게 프로퍼티를 갱신합니다.
                StatisticsSummary = result;
            }
            catch (Exception ex)
            {
                StatisticsSummary = "계산 오류";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void AddLog(string msg)
        {
            // WinForms UI는 가끔 백그라운드에서 오면 크래시가 날 수 있으나,
            // 뷰모델의 ObservableCollection 갱신 자체는 비즈니스 로직으로 간주합니다.
            // UI 스레드 동기화 보장
            InvokeOnUI(() => {
                CommunicationLogs.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {msg}");
                if (CommunicationLogs.Count > 100) CommunicationLogs.RemoveAt(100);
            });
            
        }

        // [추가] 지시 수정 메서드
        public async Task ExecuteEditCommandAsync()
        {
            if (SelectedWorkOrder == null || SelectedWorkOrder.Status != "W") return;
            var editVm = new WorkOrderEditViewModel(SelectedWorkOrder);
            OpenEditWindowRequested?.Invoke(editVm, "작업 지시 수정");
            if (editVm.IsSaved)
            {
                await _service.UpdateWorkOrder(editVm.CommitChanges());
                await ExecuteLoadCommandAsync();
            }
        }

        // [추가] 실적 등록 메서드
        public async Task ExecuteRegisterResultCommandAsync()
        {
            if (SelectedWorkOrder == null || SelectedWorkOrder.Status != "P")
            {
                ShowMessageRequested?.Invoke("진행 중인 작업만 실적 등록이 가능합니다.", "알림", "Info");
                return;
            }
            var regVm = new WorkResultRegisterViewModel(SelectedWorkOrder);
            OpenRegisterWindowRequested?.Invoke(regVm, $"실적 등록: {SelectedWorkOrder.ItemCode}");
            if (regVm.IsSaved)
            {
                await ExecuteLoadCommandAsync();
                ShowMessageRequested?.Invoke("실적이 등록되었습니다.", "성공", "Info");
            }
        }

        // [추가] 실적 조회 메서드
        public async Task ExecuteViewResultsCommnad()
        {
            if (SelectedWorkOrder == null) return;

            IsLoading = true;
            try
            {
                var listVm = new WorkResultListViewModel(SelectedWorkOrder, _WorkResultsService);
                await listVm.ExecuteLoadResultsAsync(); // 실적 로드 완료 후 창 열기
                OpenResultWindowRequested?.Invoke(listVm, "실적 내역 조회");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 특정 속성 이름을 기준으로 데이터를 정렬합니다. (WinForms BindingList 한계 극복)
        /// </summary>
        public void SortWorkOrders(string propertyName, bool ascending)
        {
            if (WorkOrders == null || !WorkOrders.Any()) return;

            // 1. 리플렉션을 이용해 정렬 대상 프로퍼티 정보 추출
            var prop = typeof(WorkOrderDto).GetProperty(propertyName);
            if (prop == null) return;

            // 2. LINQ 정렬 수행
            var sortedData = ascending
                ? WorkOrders.OrderBy(x => prop.GetValue(x, null)).ToList()
                : WorkOrders.OrderByDescending(x => prop.GetValue(x, null)).ToList();

            // 3. BindingList 갱신 (리스트를 새로 만드는 대신 기존 항목 교체)
            // RaiseListChangedEvents를 잠시 꺼서 성능 최적화
            WorkOrders.RaiseListChangedEvents = false;
            WorkOrders.Clear();
            foreach (var item in sortedData)
            {
                WorkOrders.Add(item);
            }
            WorkOrders.RaiseListChangedEvents = true;
            WorkOrders.ResetBindings(); // 화면 갱신 강제 지시
        }

     
    }
}
/*
 

 */