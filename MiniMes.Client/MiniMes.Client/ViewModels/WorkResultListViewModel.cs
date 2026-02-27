using System;
using System.Collections.ObjectModel; // 실시간 UI 반영 컬렉션
using System.Threading.Tasks;          // 비동기 처리용
using MiniMes.Domain.DTOs;
using MiniMes.Infrastructure.Interfaces;

namespace MiniMes.Client.ViewModels
{
    /// <summary>
    /// [실적 목록 조회 화면용 비즈니스 로직]
    /// 특정 작업 지시(예: 지시번호 #101) 하나에 대해 쌓인 모든 생산 실적들을 관리합니다.
    /// </summary>
    public class WorkResultListViewModel : BaseViewModel
    {
        // ---------------------------------------------------------------------
        // 1. 도구 및 데이터 저장소 (Private Fields)
        // ---------------------------------------------------------------------

        // DB에서 실적 정보를 가져오기 위한 서비스
        private readonly IWorkResultService _resultService;

        // "누구의 실적인가?"를 알기 위한 기준 정보 (원본 데이터)
        private readonly WorkOrderDto _workOrder;

        // ---------------------------------------------------------------------
        // 2. 화면에 보여줄 데이터 (UI Binding Properties)
        // ---------------------------------------------------------------------

        // [Results] 실적 데이터들의 묶음입니다.
        // ObservableCollection을 사용하여 데이터가 추가될 때 윈폼 그리드가 눈치채게 합니다.
        private ObservableCollection<WorkResultDto> _results = new ObservableCollection<WorkResultDto>();
        public ObservableCollection<WorkResultDto> Results
        {
            get => _results;
            set { _results = value; OnPropertyChanged(); }
        }

        // [상단 정보용 속성들] 
        // 팝업창 윗부분에 "지시번호: 101, 품목: 바나나우유" 처럼 보여줄 읽기 전용 데이터입니다.
        public int WorkOrderId => _workOrder.Id;
        public string ItemCode => _workOrder.ItemCode;
        public int OrderQuantity => _workOrder.Quantity;
        public string WorkOrderStatus => _workOrder.DisplayStatus; // '대기', '생산중' 등의 명칭

        // ---------------------------------------------------------------------
        // 3. 생성자 (Initialize)
        // ---------------------------------------------------------------------

        /// <param name="workOrder">어떤 작업의 실적을 볼 것인지 넘겨받음</param>
        /// <param name="resultService">데이터를 조회할 서비스(도구)를 주입받음</param>
        public WorkResultListViewModel(WorkOrderDto workOrder, IWorkResultService resultService)
        {
            // 넘겨받은 소중한 데이터와 서비스를 내 주머니에 저장
            _workOrder = workOrder ?? throw new ArgumentNullException(nameof(workOrder));
            _resultService = resultService ?? throw new ArgumentNullException(nameof(resultService));

            // [참고] 윈폼에서는 ICommand(RelayCommand)를 제거하고 직접 메서드를 호출하는 것이 관리가 쉽습니다.
        }

        // ---------------------------------------------------------------------
        // 4. 실행 로직 (Execution Methods)
        // ---------------------------------------------------------------------

        /// <summary>
        /// 실제 DB로부터 실적 목록을 비동기로 긁어오는 핵심 함수입니다.
        /// 윈폼의 '조회' 버튼 클릭 이벤트에서 이 함수를 호출(await)하면 됩니다.
        /// </summary>
        public async Task ExecuteLoadResultsAsync()
        {
            // 1. 화면에 로딩 중임을 표시 (필요 시 IsLoading 프로퍼티 활용 가능)

            // 2. 기존에 보여주던 리스트를 싹 비웁니다.
            Results.Clear();

            try
            {
                // 3. 서비스에게 요청: "이 작업지시 ID(예: 101번)에 해당하는 실적 다 가져와!"
                var data = await _resultService.GetResultsByWorkOrder(_workOrder.Id);

                if (data != null)
                {
                    // 4. 가져온 데이터를 하나하나 결과 주머니(Results)에 담습니다.
                    // 이때 ObservableCollection이 UI에 신호를 보내서 리스트가 실시간으로 채워집니다.
                    foreach (var item in data)
                    {
                        Results.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // 오류가 나면 디버그 창에 기록합니다.
                // 윈폼 화면단에서 이 예외를 잡아 MessageBox를 띄우는 것이 좋습니다.
                System.Diagnostics.Debug.WriteLine($"실적 로드 중 오류 발생: {ex.Message}");
                throw; // 에러를 위로 던져서 화면(View)에서 알림창을 띄우게 함
            }
        }
    }
}