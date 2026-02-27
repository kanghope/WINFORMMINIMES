using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MiniMes.Domain.DTOs;
using MiniMes.Infrastructure.Services;
using System.Windows.Forms;
using MiniMes.Infrastructure.Interfaces; // [수정] WinForms의 메시지 박스 사용

namespace MiniMes.Client.ViewModels
{
    /// <summary>
    /// [실적 등록 화면용 비즈니스 로직]
    /// 현장 작업자가 생산 완료 후 양품과 불량 수량을 입력하여 DB에 저장하는 기능을 담당합니다.
    /// </summary>
    public class WorkResultRegisterViewModel : BaseViewModel
    {
        // ---------------------------------------------------------------------
        // 1. 도구 및 데이터 저장소 (Private Fields)
        // ---------------------------------------------------------------------

        // 실적 정보를 DB에 저장할 때 사용할 서비스
        private readonly IWorkResultService _resultService;

        // "어떤 작업에 대한 실적인가?" 정보를 담고 있는 원본 데이터
        private readonly WorkOrderDto _workOrder;

        // 저장 성공 여부 (성공 시 true로 바뀌며, 부모 창은 이 값을 보고 리스트를 새로고침함)
        public bool IsSaved { get; set; }

        // ---------------------------------------------------------------------
        // 2. 화면 표시용 데이터 (UI Binding Properties)
        // ---------------------------------------------------------------------

        // 상단 정보 표시 (읽기 전용)
        public int WorkOrderId => _workOrder.Id;      // 지시 번호
        public string ItemCode => _workOrder.ItemCode;  // 품목 코드
        public int OrderQuantity => _workOrder.Quantity; // 목표 수량

        // [사용자 입력: 양품 수량]
        private int _goodQuantity;
        public int GoodQuantity
        {
            get => _goodQuantity;
            set
            {
                _goodQuantity = value;
                Validate(); // 값이 바뀔 때마다 수량이 적절한지 검사합니다.
                OnPropertyChanged(); // 화면에 알림
                OnPropertyChanged(nameof(CanRegister)); // "등록 버튼 눌러도 되니?" 상태도 같이 확인
            }
        }

        // [사용자 입력: 불량 수량]
        private int _badQuantity;
        public int BadQuantity
        {
            get => _badQuantity;
            set
            {
                _badQuantity = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        // [유효성 검사 메시지]
        // 수량이 잘못 입력되었을 때 사용자에게 "수량이 너무 많습니다" 등의 안내를 띄울 텍스트입니다.
        private string? _validationMessage;
        public string? ValidationMessage
        {
            get => _validationMessage;
            private set { _validationMessage = value; OnPropertyChanged(nameof(ValidationMessage)); }
        }

        // [버튼 활성화 상태] 윈폼 버튼의 Enabled 속성과 연결됩니다.
        // 양품/불량이 0보다 크고 에러 메시지가 없을 때만 true를 반환합니다.
        public bool CanRegister => (GoodQuantity >= 0 && BadQuantity >= 0) && (GoodQuantity + BadQuantity <= OrderQuantity);

        // ---------------------------------------------------------------------
        // 3. 생성자 (Initialize)
        // ---------------------------------------------------------------------

        public WorkResultRegisterViewModel(WorkOrderDto workOrder)
            : this(workOrder, new WorkResultService()) // 아래 진짜 생성자 호출
        {
        }


        public WorkResultRegisterViewModel(WorkOrderDto workOrder, WorkResultService resultService)
        {
            _workOrder = workOrder ?? throw new ArgumentNullException(nameof(workOrder));
            _resultService = resultService ?? throw new ArgumentNullException(nameof(resultService));

            // 초기값 세팅
            _goodQuantity = 0;
            _badQuantity = 0;
            IsSaved = false;

            // 창이 뜨자마자 현재 상태(0, 0)가 올바른지 한 번 체크합니다.
            Validate();
        }

        // ---------------------------------------------------------------------
        // 4. 실행 로직 (Execution Methods)
        // ---------------------------------------------------------------------

        /// <summary>
        /// [등록] 버튼 클릭 시 실행될 실제 저장 로직입니다.
        /// </summary>
        public async Task ExecuteRegisterAsync()
        {
            // 1. 저장용 데이터 바구니(DTO) 만들기
            var resultDto = new WorkResultDto
            {
                WorkOrderId = this.WorkOrderId,
                GoodQuantity = this.GoodQuantity,
                BadQuantity = this.BadQuantity,
                ResultDate = DateTime.Now
            };

            try
            {
                // 2. 서비스를 통해 DB에 저장 요청
                // (내부적으로 '실적 저장' + '작업지시 완료 처리'가 동시에 일어납니다)
                await _resultService.RegisterWorkResult(resultDto);

                // 3. 성공 처리
                IsSaved = true;
            }
            catch (Exception ex)
            {
                // 4. 실패 처리
                IsSaved = false;
                // WinForms용 메시지 박스 알림
                MessageBox.Show($"실적 저장 중 오류가 발생했습니다.\n{ex.Message}", "저장 실패",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // [데이터 검증 로직]
        // MES 시스템에서 데이터의 무결성(Integrity)을 유지하는 가장 중요한 함수입니다.
        public bool Validate()
        {
            // 새로운 검사를 위해 기존 에러 메시지를 비웁니다.
            ValidationMessage = string.Empty;

            // 검사 1: 상식 밖의 숫자(음수) 입력 방지
            if (GoodQuantity < 0 || BadQuantity < 0)
            {
                ValidationMessage += "수량은 0 미만일 수 없습니다.\n";
            }

            // 검사 2: 과생산 방지 (현장 업무 규칙)
            // 실제 생산된 총합이 지시 수량을 넘어가면 경고를 줍니다.
            if (GoodQuantity + BadQuantity > OrderQuantity)
            {
                ValidationMessage += $"총 실적 수량({GoodQuantity + BadQuantity})이 지시 수량({OrderQuantity})보다 많습니다.\n";
            }

            // [반환값] 에러 메시지가 하나도 없다면(Empty) true를 반환하여 '유효함'을 알립니다.
            return string.IsNullOrEmpty(ValidationMessage);
        }

    }
}