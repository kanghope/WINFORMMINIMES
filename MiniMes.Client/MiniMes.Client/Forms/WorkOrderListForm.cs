using MiniMes.Client.ViewModels;
using MiniMes.Domain.DTOs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniMes.Client.Forms
{
    /// <summary>
    /// [View] 작업지시 목록 화면 (WinForms 버전)
    /// WPF의 WorkOrderListView.xaml.cs에 있는 모든 로직(정렬 로딩, 데이터 체크 등)을 포함합니다.
    /// </summary>
    public partial class WorkOrderListForm : Form
    {
        // 뷰모델을 저장할 변수
        private readonly WorkOrderListViewModel _viewModel;

        /// <summary>
        /// 생성자: 화면이 처음 만들어질 때 실행됩니다.
        /// </summary>
        public WorkOrderListForm(WorkOrderListViewModel viewModel)
        {
            // 디자이너에서 만든 컨트롤들을 초기화 (이 메서드는 .Designer.cs에 있음)
            InitializeComponent();

            // 전달받은 뷰모델을 클래스 변수에 저장
            _viewModel = viewModel;

            // [추가] 그리드 컬럼 설정 (WPF와 동일한 구성)
            SetupGrid();
            SetupStatusLamp();
            SetupLoadingBar();
            LayoutStatusInfo();
            // 1. 화면 컨트롤과 뷰모델 데이터를 연결 (바인딩)
            InitBindings();

            

            // 2. 화면이 완전히 켜졌을 때 실행할 이벤트 연결
            this.Load += OnFormLoaded;

            // 3. [WPF 이식] 그리드 제목(헤더) 클릭 시 정렬 로직 연결
            dgvWorkOrders.ColumnHeaderMouseClick += OnGridColumnHeaderClick;
        }

        /// <summary>
        /// [추가] DataGridView 컬럼 구성 (WPF와 동일하게)
        /// </summary>
        private void SetupGrid()
        {
            dgvWorkOrders.Columns.Clear();
            dgvWorkOrders.AutoGenerateColumns = false;

            // 1. 지시 ID
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderCell = { Value = "지시 ID" },
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // 2. 품목 코드
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            { // Custom 속성 사용 가능
                DataPropertyName = "ItemCode",
                HeaderCell = { Value = "품목 코드" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // 3. 계획 수량
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderCell = { Value = "계획 수량" },
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "N0" }
            });

            // 4. 실적 수량
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CompleteQty",
                HeaderCell = { Value = "실적 수량" },
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "N0" }
            });

            // 5. 진행률 (ProgressBar를 그리기 위한 커스텀 컬럼)
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Progress",
                HeaderCell = { Value = "진행률" },
                Width = 120,
                ReadOnly = true
            });

            // 6. 상태 (WPF의 DataTrigger를 대체할 이벤트 연결)
            dgvWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DisplayStatus", // 화면에 보여줄 텍스트 (예: "진행중")
                Name = "StatusColumn",             // 이벤트에서 찾기 위한 이름
                HeaderCell = { Value = "상태" },
                Width = 80,
                DefaultCellStyle = {
            Alignment = DataGridViewContentAlignment.MiddleCenter,
            Font = new Font(dgvWorkOrders.Font, FontStyle.Bold)
        }
            });

            foreach (DataGridViewColumn col in dgvWorkOrders.Columns)
            {
                // 모든 컬럼을 프로그램 방식 정렬로 설정 (우리가 작성한 이벤트를 타게 함)
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void LayoutStatusInfo()
        {
            // 1. 패널 설정 (WPF의 Border 느낌)
            pnlStatusInfo.BackColor = Color.FromArgb(248, 249, 250); // #F8F9FA
            pnlStatusInfo.Padding = new Padding(10);

            // 2. 상태 표시등과 텍스트 배치
            lblStatusLamp.Location = new Point(10, 15);
            lblStatusText.Location = new Point(30, 13);
            lblStatusText.Font = new Font(this.Font, FontStyle.Bold);

            // 3. 마지막 수신 시간 (제목)
            Label lblTimeTitle = new Label
            {
                Text = "마지막 수신 시간:",
                Location = new Point(10, 45),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font(this.Font.FontFamily, 8)
            };
            pnlStatusInfo.Controls.Add(lblTimeTitle);

            lblLastTime.Location = new Point(10, 60);
            lblLastTime.AutoSize = true;

            // 4. 누적 패킷량 (제목)
            Label lblPacketTitle = new Label
            {
                Text = "누적 패킷량:",
                Location = new Point(10, 85),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font(this.Font.FontFamily, 8)
            };
            pnlStatusInfo.Controls.Add(lblPacketTitle);

            lblPacketCount.Location = new Point(10, 100);
            lblPacketCount.AutoSize = true;
        }

        private void SetupLoadingBar()
        {
            // 1. 프로그레스바의 부모를 tlpMain으로 변경하여 레이아웃 시스템에 편입
            // tlpMain의 특정 위치(예: 1번 행 lblStats 위)에 배치하거나 
            // 아예 폼의 최상위 계층으로 다시 설정합니다.
            this.Controls.Remove(pgbLoading);
            this.Controls.Add(pgbLoading); // 다시 추가
            pgbLoading.BringToFront();

            // 2. 위치를 화면 정중앙으로 강제 고정 (그리드 영역 근처)
            pgbLoading.Location = new Point(
                (this.ClientSize.Width - pgbLoading.Width) / 2,
                (this.ClientSize.Height - pgbLoading.Height) / 2
            );
        }

        /// <summary>
        /// [추가] 상태 표시등(Ellipse)을 WinForms Label로 구현
        /// </summary>
        private void SetupStatusLamp()
        {
            if (lblStatusLamp == null) return;

            lblStatusLamp.Text = "";
            lblStatusLamp.Width = 15;
            lblStatusLamp.Height = 15;

            // 원형으로 모양 깎기
            // 1. 도안 그리기 도구(Path)를 준비합니다.
            // GraphicsPath는 선, 곡선, 도형 등을 그릴 수 있는 '가상의 설계도'라고 생각하면 됩니다.
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // 2. 설계도에 '타원'을 하나 그려 넣습니다.
            // (0, 0) 좌표부터 시작해서, lblStatusLamp(라벨)의 가로(Width)와 세로(Height) 크기에 딱 맞는 타원을 그립니다.
            // 만약 라벨의 가로/세로가 같다면 완벽한 '정원'이 그려집니다.
            path.AddEllipse(0, 0, lblStatusLamp.Width, lblStatusLamp.Height);

            // 3. 만들어진 설계도(path)를 바탕으로 라벨의 '실제 영역'을 재정의합니다.
            // 원래 라벨은 사각형이지만, 위에서 그린 타원 모양만큼만 남기고 나머지는 가위로 오려내듯 버립니다.
            // 이제 lblStatusLamp는 화면에 동그랗게 보이게 됩니다.
            lblStatusLamp.Region = new Region(path);
        }

        /// <summary>
        /// 뷰모델의 데이터와 WinForms 컨트롤을 연결하는 설정입니다.
        /// </summary>
        private void InitBindings()
        {
            // 1. Grid 바인딩 (BindingList와 연결)
            dgvWorkOrders.AutoGenerateColumns = false;
            // BindingSource를 사용하면 정렬 및 갱신이 더 안정적입니다.
            // 1. [데이터 중계소(통제실) 생성]
            // BindingSource는 '데이터 원본'과 'UI 컨트롤' 사이에서 중간 관리를 해주는 객체입니다.
            // 여기서 DataSource를 뷰모델에 있는 WorkOrders(작업지시 목록 리스트)로 지정합니다.
            BindingSource bs = new BindingSource { DataSource = _viewModel.WorkOrders };

            // 2. [화면(표)과 중계소 연결]
            // 그리드뷰(dgvWorkOrders)에게 "너는 이제부터 방금 만든 'bs'라는 중계소만 바라봐"라고 알려줍니다.
            // 이제 WorkOrders 리스트에 데이터가 추가/삭제되면, 이 중계소를 통해 화면에 즉시 나타납니다.
            dgvWorkOrders.DataSource = bs;

            // 2. 검색 조건 바인딩 (중요: 양방향 설정)
            // 1. [시작 날짜 달력 연결]
            // dtpStart(달력 컨트롤)의 "Value"(날짜값) 속성을
            // _viewModel(두뇌)의 "StartDate"라는 변수와 연결합니다.
            // 'true'는 데이터 형식을 자동으로 맞춰주겠다는 뜻이고,
            // 'OnPropertyChanged'는 사용자가 날짜를 클릭하자마자 즉시 뷰모델에 반영하라는 뜻입니다.
            dtpStart.DataBindings.Add("Value", _viewModel, nameof(_viewModel.StartDate), true, DataSourceUpdateMode.OnPropertyChanged);

            // 2. [종료 날짜 달력 연결]
            // dtpEnd(달력 컨트롤)의 "Value" 속성을 _viewModel의 "EndDate" 변수와 연결합니다.
            // 위와 마찬가지로 날짜가 바뀌면 즉시 뷰모델의 EndDate 값도 자동으로 업데이트됩니다.
            dtpEnd.DataBindings.Add("Value", _viewModel, nameof(_viewModel.EndDate), true, DataSourceUpdateMode.OnPropertyChanged);

            // 3. [검색어 입력창 연결]
            // txtSearch(텍스트박스)의 "Text"(글자) 속성을 _viewModel의 "SearchText" 변수와 연결합니다.
            // 사용자가 검색창에 한 글자 한 글자 타이핑할 때마다(OnPropertyChanged) 
            // 뷰모델의 SearchText 변수값이 실시간으로 바뀝니다.
            txtSearch.DataBindings.Add("Text", _viewModel, nameof(_viewModel.SearchText), true, DataSourceUpdateMode.OnPropertyChanged);

            // 3. 상태 콤보박스 바인딩
            cbStatus.DataSource = _viewModel.StatusOptions;
            cbStatus.DisplayMember = "DisplayName";
            cbStatus.ValueMember = "StatusCode";
            cbStatus.DataBindings.Add("SelectedValue", _viewModel, nameof(_viewModel.SelectedStatusCode), true, DataSourceUpdateMode.OnPropertyChanged);

            // 4. PLC 상태 정보 바인딩
            lblStatusText.DataBindings.Add("Text", _viewModel, nameof(_viewModel.DeviceStatusText));
            lblPacketCount.DataBindings.Add("Text", _viewModel, nameof(_viewModel.PacketCount));
            lblLastTime.DataBindings.Add("Text", _viewModel, nameof(_viewModel.LastSignalTime));

            // [보완] 상태 색상 바인딩 (Panel이나 Label의 BackColor와 연결)
            //pnlStatusIndicator.DataBindings.Add("BackColor", _viewModel, nameof(_viewModel.DeviceStatusColor), true);
            // [보완 및 구현] 상태 표시등 배경색 연동
            lblStatusLamp.DataBindings.Add("BackColor", _viewModel, nameof(_viewModel.DeviceStatusColor), true);

            // 5. 로딩 표시 및 진행률 바인딩
            pgbLoading.DataBindings.Clear();

            // Visible 바인딩: IsLoading이 true일 때만 보임
            pgbLoading.DataBindings.Add("Visible", _viewModel, nameof(_viewModel.IsLoading), true, DataSourceUpdateMode.OnPropertyChanged);

            // Value 바인딩: double -> int 변환 처리
            // 1. [바인딩(연결) 생성]
            // 프로그레스바의 "Value"(현재 채워진 정도) 속성을 
            // _viewModel의 "LoadingProgress" 변수와 연결합니다.
            Binding progressBinding = new Binding("Value", _viewModel, nameof(_viewModel.LoadingProgress), true);

            // 2. [포맷팅(가공) 이벤트 설정]
            // 뷰모델의 값이 화면(프로그레스바)으로 전달되기 직전에 실행되는 '가공소'입니다.
            // "데이터를 화면에 뿌리기 전에 모양을 좀 다듬을게!"라는 뜻입니다.
            //progressBinding.Format이라는 이벤트는 **"택배가 발송되기 전 포장 센터" * *입니다.
            progressBinding.Format += (s, e) =>//누가 보냈고(s), 어떤 정보를 담고 있는지(e)"**를 전달받는 바구니
            {
                // 3. [빈 값 처리]
                // 만약 전달받은 값(e.Value)이 비어있거나(null) DB에서 가져온 빈 값(DBNull)이라면,
                // 에러가 나지 않도록 일단 '0'으로 채워줍니다.
                if (e.Value == null || e.Value is DBNull)
                {
                    e.Value = 0;
                }
                else
                {
                    // 4. [숫자 예쁘게 깎기]
                    // 뷰모델에서 온 값(예: 75.6)을 소수점에서 반올림(Math.Round)하여 정수(int)로 바꿉니다.
                    int val = (int)Math.Round(Convert.ToDouble(e.Value));

                    // 5. [안전 범위 고정]
                    // 혹시라도 값이 -5가 오면 0으로, 105가 오면 100으로 강제 고정합니다.
                    // (프로그레스바가 0~100 범위를 벗어나서 뻗어버리는 것을 방지하는 안전장치입니다.)
                    e.Value = Math.Max(0, Math.Min(100, val));

                    // [핵심 3] WinForms 프로그레스바의 애니메이션 버그 해결 편법
                    // 프로그레스바는 값을 증가시킬 때 천천히 움직이지만, 
                    // 값을 살짝 낮췄다가 다시 설정하거나 강제로 Refresh하면 즉시 그려집니다.
                    if (pgbLoading.InvokeRequired)//1. 내가 UI 스레드가 아닌가?
                    {
                        // 2. 응, 아니네. 그러면 UI 스레드에게 "대리 수행"을 요청하자 (Invoke)
                        pgbLoading.Invoke(new Action(() => {
                            pgbLoading.Value = val;
                            pgbLoading.Update(); // 즉시 다시 그리기 강제
                        }));
                    }
                    else
                    {
                        // 3. 내가 UI 스레드가 맞네. 그냥 직접 수정하자.
                        pgbLoading.Value = val;
                        pgbLoading.Update();
                    }
                }
            };
            pgbLoading.DataBindings.Add(progressBinding);

            // [추가] 통계 요약 텍스트 바인딩
            lblStats.DataBindings.Clear(); // 중복 바인딩 방지
            lblStats.DataBindings.Add("Text", _viewModel, nameof(_viewModel.StatisticsSummary), true, DataSourceUpdateMode.OnPropertyChanged);

            // [추가] 통신 로그 목록 바인딩
            // DataSource를 연결하면 ObservableCollection에 데이터가 추가될 때마다 리스트박스가 갱신됩니다.
            lbLogs.DataSource = _viewModel.CommunicationLogs;

            // [추가] 모든 버튼의 Enabled(활성화) 속성을 ViewModel과 연결
            // 1. 조회 및 통계 관련
            btnRefresh.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanRefreshOrCalculate), true, DataSourceUpdateMode.OnPropertyChanged);
            // 만약 btnCalculate 버튼이 있다면 추가
            if (btnCalculate != null)
                btnCalculate.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanRefreshOrCalculate), true, DataSourceUpdateMode.OnPropertyChanged);

            // 2. 등록 버튼
            btnAdd.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanAddWork), true, DataSourceUpdateMode.OnPropertyChanged);

            // 3. 수정 및 삭제 버튼
            btnEdit.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanEditOrDelete), true, DataSourceUpdateMode.OnPropertyChanged);
            btnDelete.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanEditOrDelete), true, DataSourceUpdateMode.OnPropertyChanged);

            // 4. 작업 시작 및 종료 버튼
            btnStart.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanStartWork), true, DataSourceUpdateMode.OnPropertyChanged);
            btnStop.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanStopWork), true, DataSourceUpdateMode.OnPropertyChanged);

            // 5. 실적 등록 및 조회 버튼
            btnRegisterResult.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanRegisterResult), true, DataSourceUpdateMode.OnPropertyChanged);
            btnViewResults.DataBindings.Add("Enabled", _viewModel, nameof(_viewModel.CanViewResults), true, DataSourceUpdateMode.OnPropertyChanged);

            // [참고] DataGridView의 SelectionChanged 이벤트를 통해 선택된 항목을 ViewModel에 즉시 알림
            dgvWorkOrders.SelectionChanged += (s, e) => UpdateSelectedOrder();

            // [중요] WPF의 DataTrigger 역할을 수행할 이벤트 핸들러 연결
            dgvWorkOrders.CellFormatting += DgvWorkOrders_CellFormatting;

            // WPF의 ProgressBar 역할을 대신할 그리기 이벤트 연결
            dgvWorkOrders.CellPainting += DgvWorkOrders_CellPainting;
        }


        //이 로직은 "진행률" 컬럼을 찾아서 현재 값(실적/계획)을 계산해 막대그래프를 그려줍니다.
        private void DgvWorkOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // "Progress"라는 이름을 가진 컬럼의 본문 셀(RowIndex >= 0)만 처리
            if (e.ColumnIndex >= 0 && dgvWorkOrders.Columns[e.ColumnIndex].Name == "Progress" && e.RowIndex >= 0)
            {
                var row = dgvWorkOrders.Rows[e.RowIndex].DataBoundItem as WorkOrderDto;
                if (row == null) return;

                // 1. 배경과 테두리 먼저 그리기
                // e (이벤트 가방) 안에 있는 PaintBackground 도구를 꺼내서 실행합니다.
                // 1. e.CellBounds: "지금 그려야 할 이 칸의 '네모난 영역(좌표와 크기)'에다가"
                // 2. true: "기본 배경색(선택되었을 때의 파란색 등)을 포함해서 그려라!"
                e.PaintBackground(e.CellBounds, true);

                // 2. 진행률 계산 (실적 / 계획)
                double percentage = 0;
                if (row.Quantity > 0)
                {
                    percentage = (double)row.CompleteQty / row.Quantity;
                }
                percentage = Math.Min(1.0, Math.Max(0, percentage)); // 0~1 사이로 고정
                /*
                 진행률 80%"라고 말하면 80이라는 숫자를 떠올리지만, 프로그래밍(특히 WinForms나 WPF의 그리기 로직)에서는 
                0.0 ~ 1.0 (0% ~ 100%) 사이의 비율(Ratio) 값을 기준으로 계산하는 경우가 훨씬 많기 때문
                 */

                // 3. 프로그레스바 영역 계산 (여백 2px 정도 줌)
                int rectWidth = (int)((e.CellBounds.Width - 10) * percentage);
                /*
                 숫자로 보는 예시 (전체 너비가 110px일 때)
                여백 빼기: 110 - 10 = 100 (이제 막대를 그릴 수 있는 순수 공간은 100px입니다.)
                진행률 곱하기: 만약 80%(0.8)가 완료되었다면?
                100 * 0.8 = 80
                결과: rectWidth는 80이 됩니다. 즉, 110px 너비의 셀 안에서 80px 길이의 파란색 막대가 그려지는 것이죠.
                 */

                //이 코드는 화면에 * *"진행률 막대(채워지는 부분)" * *와 * *"바깥쪽 테두리(전체 틀)" * *라는 두 개의 사각형 설계도를 그리는 작업
                // 1. 실제로 파란색으로 채워질 "진행률 막대"의 영역을 잡습니다.
                // (시작위치X + 5): 셀 왼쪽 벽에서 5픽셀 띄워서 시작
                // (시작위치Y + 5): 셀 위쪽 벽에서 5픽셀 내려와서 시작
                // rectWidth: 앞에서 계산한 (비율에 따른) 막대 길이만큼만 가로로 그림
                // (전체높이 - 11): 위아래 여백(5+5=10)에 테두리 두께(1)를 고려해 높이를 조절
                Rectangle progressRect = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 5, rectWidth, e.CellBounds.Height - 11);

                // 2. 막대그래프를 감싸는 "회색 테두리(전체 칸)"의 영역을 잡습니다.
                // 시작 위치는 위와 동일하게 (X+5, Y+5)입니다.
                // (전체너비 - 10): 진행률에 상관없이 항상 '셀 너비 - 양옆 여백 10'만큼 꽉 차게 그림
                // (전체높이 - 11): 위와 동일하게 높이를 맞춤
                Rectangle borderRect = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 5, e.CellBounds.Width - 10, e.CellBounds.Height - 11);

                // 4. 색상 설정 및 그리기 (WPF의 #0078D7 느낌)
                // 1. 도구 준비 (도서관에서 책을 빌리듯, 메모리 자원을 잠시 빌려옵니다)
                // backBrush: 배경을 칠할 연한 회색 붓 (#EEEEEE)
                using (Brush backBrush = new SolidBrush(Color.FromArgb(238, 238, 238)))

                // progressBrush: 진행 상태를 나타낼 파란색 붓 (#0078D7, 윈도우 기본 강조색 느낌)
                using (Brush progressBrush = new SolidBrush(Color.FromArgb(0, 120, 215)))

                // borderPen: 테두리를 그릴 얇은 회색 펜 (Color.LightGray)
                using (Pen borderPen = new Pen(Color.LightGray))
                {
                    // 2. 실행 (실제로 도화지에 그리는 순서가 중요합니다!)

                    // [첫 번째 층] 전체 배경 칠하기 (연한 회색으로 빈 통을 만듭니다)
                    // borderRect 영역 전체를 backBrush로 꽉 채웁니다.
                    e.Graphics.FillRectangle(backBrush, borderRect);

                    // [두 번째 층] 진행률 바 칠하기 (그 위에 파란색 물을 채웁니다)
                    // progressRect(계산된 길이만큼의 영역)를 progressBrush로 덧칠합니다.
                    e.Graphics.FillRectangle(progressBrush, progressRect);

                    // [세 번째 층] 테두리 선 긋기 (마지막으로 깔끔하게 외곽선을 따줍니다)
                    // borderRect 영역의 가장자리를 따라 borderPen으로 선을 긋습니다.
                    // ※ 주의: Fill은 안을 채우는 것이고, Draw는 선만 긋는 것입니다.
                    e.Graphics.DrawRectangle(borderPen, borderRect);

                } // 3. 반납 (블록이 끝나면 붓 2개와 펜 1개는 자동으로 파기되어 메모리가 정리됩니다)

                // 5. 텍스트 그리기 (예: 50%)
                // 1. 표시할 텍스트 형식 만들기
                // percentage는 0.0~1.0 사이의 값입니다 (예: 0.85).
                // (percentage * 100): 0.85를 85로 바꿉니다.
                // :F0: 소수점 아래는 표시하지 말고(0자리) 반올림하라는 뜻입니다.
                // 결과적으로 "85%"와 같은 문자열이 만들어집니다.
                string text = $"{(percentage * 100):F0}%";

                // 2. 텍스트 그리기 (WinForms의 전용 도구인 TextRenderer 사용)
                // e.Graphics: 그림을 그릴 도화지
                // text: 위에서 만든 "85%" 글자
                // e.CellStyle.Font: 이 그리드 셀에 설정된 기본 폰트 사용
                // e.CellBounds: 글자를 그릴 영역 (여기선 셀 전체 영역을 기준으로 설정)
                // Color.Black: 글자 색상은 검정색
                // TextFormatFlags: 글자 배치 옵션 (비트 연산자 '|'로 여러 기능을 합침)
                //    - VerticalCenter: 세로 방향으로 정중앙 배치
                //    - HorizontalCenter: 가로 방향으로 정중앙 배치
                TextRenderer.DrawText(e.Graphics, text, e.CellStyle.Font, e.CellBounds, Color.Black,
                                      TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true; // 기본 그리기 무시
            }
        }
       

        /// <summary>
        /// WPF의 DataTrigger 기능을 WinForms에서 구현
        /// 데이터 값에 따라 셀의 스타일(색상 등)을 실시간으로 결정합니다.
        /// </summary>
        private void DgvWorkOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. "상태" 컬럼인지 확인 (인덱스 대신 Name으로 찾는 것이 안전합니다)
            if (dgvWorkOrders.Columns[e.ColumnIndex].Name == "StatusColumn" && e.RowIndex >= 0)
            {
                // 2. 현재 행의 실제 데이터 객체(DTO)를 가져옵니다.
                var row = dgvWorkOrders.Rows[e.RowIndex].DataBoundItem as WorkOrderDto;
                if (row == null) return;

                // 3. Status 코드값(P, C, W)에 따라 글자 색상(ForeColor)을 변경합니다.
                // WPF의 DataTrigger Value와 동일한 로직 적용
                switch (row.Status)
                {
                    case "P": // 진행중 (WPF: LightGreen -> WinForms: LimeGreen이 가독성이 더 좋습니다)
                        //e.CellStyle.ForeColor = Color.LimeGreen;
                        e.CellStyle.BackColor = Color.LimeGreen;
                        break;
                    case "C": // 완료 (WPF: Blue)
                        e.CellStyle.BackColor = Color.Blue;
                        break;
                    case "W": // 대기 (WPF: LightPink)
                        e.CellStyle.BackColor = Color.LightPink;
                        break;
                    default:
                        e.CellStyle.BackColor = Color.Black;
                        break;
                }
            }
        }
        /// <summary>
        /// 화면이 로드된 후 실행되는 메인 설정입니다.
        /// </summary>
        private async void OnFormLoaded(object sender, EventArgs e)
        {
            // tlpMain보다 위로 올라오도록 순서를 조정합니다.
            pgbLoading.BringToFront();

            // [A. 뷰모델의 알림 요청 이벤트 처리]
            // 에러 메시지나 알림창을 띄우라는 요청이 오면 실행됩니다.
            _viewModel.ShowMessageRequested += (msg, title, type) => {
                var icon = type == "Error" ? MessageBoxIcon.Error : MessageBoxIcon.Information;
                // 통신 스레드에서 호출될 수 있으므로 UI 스레드 안전 처리를 위해 BeginInvoke 사용
                this.BeginInvoke(new Action(() => MessageBox.Show(this, msg, title, MessageBoxButtons.OK, icon)));
            };

            // [B. 팝업창 열기 요청 이벤트 구독]
            _viewModel.OpenEditWindowRequested += OnOpenEditWindowRequested;
            _viewModel.OpenRegisterWindowRequested += OnOpenRegisterWindowRequested;
            _viewModel.OpenResultWindowRequested += OnOpenResultWindowRequested;

            // [C. 버튼 클릭 이벤트 설정 - 뷰모델 메서드 직접 연결]
            //합계계산 버튼
            btnCalculate.Click += async (s, e) => await _viewModel.ExecuteCalculateStatsAsync();
            // 검색 버튼
            btnSearch.Click += async (s, ev) => await _viewModel.ExecuteLoadCommandAsync();
            // 새로고침 버튼
            btnRefresh.Click += async (s, ev) => await _viewModel.ExecuteLoadCommandAsync();
            // 등록 버튼
            btnAdd.Click += async (s, ev) => await _viewModel.ExecuteAddCommandAsync();

            // 수정 버튼
            btnEdit.Click += async (s, ev) => {
                UpdateSelectedOrder(); // 현재 선택된 행 정보를 뷰모델에 알림
                // [구현 추가] 뷰모델에 수정 로직이 있다면 호출, 없다면 공통 메서드로 유도
                if (_viewModel.SelectedWorkOrder != null)
                    await _viewModel.ExecuteEditCommandAsync();
            };

            // 삭제 버튼
            btnDelete.Click += async (s, ev) => {
                UpdateSelectedOrder(); // 현재 선택된 행 정보를 뷰모델에 알림
                // [구현 추가] 삭제 확인 절차 및 명령 실행
                if (_viewModel.SelectedWorkOrder == null) return;
                var dr = MessageBox.Show("선택한 작업지시를 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    await _viewModel.ExecuteDeleteCommandAsync();
                }
            };

            // 작업 시작 버튼 (PLC 가동)
            btnStart.Click += async (s, ev) => {
                UpdateSelectedOrder();
                await _viewModel.ExecuteStartWorkAsync();
            };

            // 작업 종료 버튼 (PLC 중지)
            btnStop.Click += async (s, ev) => {
                UpdateSelectedOrder();
                await _viewModel.ExecuteStopWorkAsync();
            };

            // [보완] 실적 등록 버튼
            btnRegisterResult.Click += async (s, ev) => {
                await _viewModel.ExecuteRegisterResultCommandAsync();
            };

            // 실적 조회 버튼 (WPF 로직 반영을 위해 클릭 시 뷰모델의 동작 유도)
            btnViewResults.Click += async (s, ev) => {
                UpdateSelectedOrder();
                // 뷰모델에서 실적 데이터를 조회하고 OpenResultWindowRequested 이벤트를 발생시켜야 함
                await _viewModel.ExecuteViewResultsCommnad();
            };

            // [D. 초기 데이터 로드] 화면이 켜지자마자 데이터를 한 번 불러옵니다.
            await _viewModel.ExecuteLoadCommandAsync();
        }

        /// <summary>
        /// 표(Grid)에서 사용자가 클릭한 줄의 데이터를 뷰모델의 'SelectedWorkOrder'에 넣어줍니다.
        /// </summary>
        private void UpdateSelectedOrder()
        {
            if (dgvWorkOrders.CurrentRow?.DataBoundItem is WorkOrderDto selected)
            {
                _viewModel.SelectedWorkOrder = selected;
            }
        }

        #region [팝업창 처리 핸들러 - WPF 기능 이식]

        // 1. 작업지시 등록/수정 창 열기
        private void OnOpenEditWindowRequested(WorkOrderEditViewModel editVm, string title)
        {
            using (var editForm = new WorkOrderEditForm(editVm))
            {
                editForm.Text = title;
                editForm.StartPosition = FormStartPosition.CenterParent; // 부모창 중앙에 띄움
                editForm.ShowDialog(); // 모달(팝업이 닫힐 때까지 부모 조작 불가)로 띄움
            }
        }

        // 2. 작업실적 등록 창 열기
        private void OnOpenRegisterWindowRequested(WorkResultRegisterViewModel registerVm, string title)
        {
            using (var registerForm = new WorkResultRegisterForm(registerVm))
            {
                registerForm.Text = title;
                registerForm.StartPosition = FormStartPosition.CenterParent;
                registerForm.ShowDialog();
            }
        }

        // 3. 실적 목록 조회 창 열기 (WPF의 if(results.Any()) 로직 포함)
        private void OnOpenResultWindowRequested(WorkResultListViewModel resultVm, string title)
        {
            // [중요] WPF 소스와 동일하게 데이터가 있는지 먼저 검사합니다.
            if (resultVm.Results != null && resultVm.Results.Any())
            {
                // 실적 데이터가 있으면 창을 띄웁니다.
                using (var resultForm = new WorkResultListForm(resultVm))
                {
                    resultForm.Text = title;
                    resultForm.StartPosition = FormStartPosition.CenterParent;
                    resultForm.ShowDialog();
                }
            }
            else
            {
                // [WPF 이식] 실적 데이터가 없으면 경고 메시지를 보여줍니다.
                MessageBox.Show($"선택된 작업 지시 ({resultVm.ItemCode})에 대해 등록된 실적 정보가 없습니다.",
                                "조회 실패",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        #endregion

        #region [그리드 정렬 로직 - WPF의 DataGrid_Sorting 기능 이식]

        /// <summary>
        /// 사용자가 그리드 제목을 눌러 정렬할 때 로딩 바를 보여주는 기능입니다.
        /// </summary>
        private async void OnGridColumnHeaderClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 || e.ColumnIndex < 0) return;

            string propertyName = dgvWorkOrders.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(propertyName)) return;

            _viewModel.IsLoading = true;

            // 현재 정렬 방향 확인
            SortOrder currentOrder = dgvWorkOrders.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            bool isAsc = (currentOrder != SortOrder.Ascending);

            // [핵심] 뷰모델에 정렬 명령 전달
            _viewModel.SortWorkOrders(propertyName, isAsc);

            // UI 화살표 업데이트
            foreach (DataGridViewColumn col in dgvWorkOrders.Columns) col.HeaderCell.SortGlyphDirection = SortOrder.None;
            dgvWorkOrders.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = isAsc ? SortOrder.Ascending : SortOrder.Descending;

            _viewModel.IsLoading = false;
        }

        #endregion
    }
}