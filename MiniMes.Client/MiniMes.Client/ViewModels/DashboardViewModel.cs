using MiniMES.Infastructure.Services;
using MiniMes.Domain.DTOs;
using System.Threading.Tasks;
using System.ComponentModel;
using MiniMES.Infastructure.interfaces; // INotifyPropertyChanged를 쓰기 위해 필요해요!

namespace MiniMes.Client.ViewModels
{
    /// <summary>
    /// 대시보드 화면의 데이터와 로직을 담당하는 뷰모델입니다.
    /// BaseViewModel을 상속받아 알림(PropertyChanged) 기능을 사용합니다.
    /// </summary>
    public class DashboardViewModel : BaseViewModel
    {
        // 실시간 PLC 통신 및 DB 조회를 위한 서비스 선언
        private readonly SerialDeviceService _plcService;
        private readonly IDashboardService _dbService;

        #region [UI 바인딩용 속성들]
        // 윈폼 컨트롤(Label, TextBox 등)과 연결될 실제 데이터들입니다.

        private int _totalGood;//실적수량
        public int TotalGood
        {
            get => _totalGood;
            set { _totalGood = value; OnPropertyChanged(); } // 값 변경 시 화면에 즉시 알림
        }

        private int _totalBad;//불량수량
        public int TotalBad
        {
            get => _totalBad;
            set { _totalBad = value; OnPropertyChanged(); }
        }

        private int _activeOrderCount;//주문수량
        public int ActiveOrderCount
        {
            get => _activeOrderCount;
            set { _activeOrderCount = value; OnPropertyChanged(); }
        }

        private double _achievementRate;//달성률 
        public double AchievementRate
        {
            get => _achievementRate;
            set { _achievementRate = value; OnPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// 생성자: 필요한 서비스를 주입받고 초기 설정을 수행합니다.
        /// </summary>
        public DashboardViewModel(SerialDeviceService plcService, IDashboardService dbService)
        {
            // 의존성 주입 (Dependency Injection) 확인
            _plcService = plcService ?? throw new ArgumentNullException(nameof(plcService));
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));

            // [중요] PLC 서비스에서 데이터 갱신이 필요하다는 신호(이벤트)를 보내면 수신하도록 등록
            // WPF에서 람다로 썼던 부분을 메모리 관리가 용이하게 메서드 형태로 등록합니다.
            _plcService.OnRefreshRequired += OnPlcDataRefreshed;

            // 프로그램 시작 시 처음 한 번 데이터를 가져옵니다. (비동기 호출)
            InitInitialData();
        }

        /// <summary>
        /// 최초 데이터 로드를 위한 별도 메서드입니다.
        /// </summary>
        private async void InitInitialData()
        {
            await RefreshDashboard();
        }

        /// <summary>
        /// PLC 서비스로부터 데이터 갱신 요청 이벤트를 받았을 때 실행됩니다.
        /// </summary>
        /// <param name="needRefresh">갱신 필요 여부</param>
        private async void OnPlcDataRefreshed(bool needRefresh)
        {
            if (needRefresh)
            {
                // [참고] WinForms의 데이터 바인딩 시스템은 백그라운드 스레드에서 
                // 속성값이 바뀌어도 내부적으로 UI 스레드 동기화를 시도합니다.
                // WPF의 Dispatcher.Invoke 없이도 대부분 정상 작동합니다.
                await RefreshDashboard();
            }
        }

        /// <summary>
        /// 실제 DB에서 오늘 생산 현황 요약을 가져와 속성들을 갱신하는 핵심 로직입니다.
        /// </summary>
        public async Task RefreshDashboard()
        {
            try
            {
                // DB 서비스로부터 최신 요약 데이터(DTO) 호출
                var data = await _dbService.GetTodayProductionSummaryAsync();

                if (data != null)
                {
                    // 가져온 데이터를 뷰모델 속성에 할당합니다.
                    // 이때 OnPropertyChanged가 발생하여 바인딩된 WinForms UI가 자동으로 바뀝니다.
                    TotalGood = data.TotalGoodQty;
                    TotalBad = data.TotalBadQty;
                    ActiveOrderCount = data.ActiveOrderCount;
                    AchievementRate = data.AchievementRate;
                }
            }
            catch (Exception ex)
            {
                // 실무에서는 로그 파일에 기록하거나 디버그 창에 출력합니다.
                System.Diagnostics.Debug.WriteLine($"대시보드 갱신 중 오류 발생: {ex.Message}");
            }
        }

        /// <summary>
        /// 뷰모델이 소멸될 때 이벤트를 해제하여 메모리 누수를 방지합니다.
        /// </summary>
        public void Cleanup()
        {
            if (_plcService != null)
            {
                _plcService.OnRefreshRequired -= OnPlcDataRefreshed;
            }
        }
    }
}
