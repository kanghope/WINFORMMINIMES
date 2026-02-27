using Microsoft.Extensions.DependencyInjection;
using MiniMes.Domain.Auth;
using System;
using System.Drawing;
using System.Windows.Forms;
using MiniMes.Client.Forms;      // UserControl(View)들이 위치한 곳
using MiniMes.Client.ViewModels; // ViewModel들이 위치한 곳

namespace MiniMes.Client
{
    /// <summary>
    /// [MainForm] 프로그램의 메인 껍데기 화면입니다.
    /// 메뉴 선택에 따라 중앙 패널에 다양한 뷰(UserControl)를 교체하여 보여줍니다.
    /// </summary>
    public partial class MainForm : Form
    {
        // 의존성 주입(DI)을 위한 서비스 공급자 (ViewModel 등을 꺼내올 때 사용)
        private readonly IServiceProvider _serviceProvider;

        // ---------------------------------------------------------------------
        // 1. 생성자 및 초기화
        // ---------------------------------------------------------------------
        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // 윈도우 시작 위치를 화면 중앙으로 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. 로그인한 사용자의 정보를 상단 바에 표시
            LoadUserInfo();

            // 2. 프로그램 켜자마자 첫 화면으로 '대시보드'를 띄움
            OnOpenDashboardClick(null, null);
        }

        /// <summary>
        /// 로그인 세션(UserSession)에서 정보를 가져와 UI에 뿌려줍니다.
        /// </summary>
        private void LoadUserInfo()
        {
            // UserSession은 정적(static) 클래스로 로그인 시 정보가 담겨 있음
            txtUserName.Text = $"{UserSession.UserName ?? "미로그인"} 님";

            // 권한에 따른 텍스트 및 색상 변경
            if (UserSession.UserRole == "ADMIN")
            {
                txtUserRole.Text = "시스템 관리자";
                txtUserRole.ForeColor = Color.LimeGreen;
            }
            else
            {
                txtUserRole.Text = "일반 작업자";
                txtUserRole.ForeColor = Color.WhiteSmoke;
            }
        }

        // ---------------------------------------------------------------------
        // 2. 화면 교체 핵심 로직 (WPF의 ContentControl 대체)
        // ---------------------------------------------------------------------

        /// <summary>
        /// 메인 영역(pnlMainContent)에 새로운 화면을 끼워 넣습니다.
        /// </summary>
        /// <param name="view">표시할 Control 객체</param>
        private void ShowContentInPanel(Control view)
        {
            // 1. 기존에 떠 있던 화면을 메모리에서 해제하고 패널에서 제거
            if (pnlMainContent.Controls.Count > 0)
            {
                foreach (Control ctrl in pnlMainContent.Controls)
                {
                    ctrl.Dispose(); // 메모리 누수 방지
                }
                pnlMainContent.Controls.Clear();
            }

            // --- 추가해야 할 부분 ---
            if (view is Form frm)
            {
                frm.TopLevel = false;          // 독립 창이 아님을 선언 (매우 중요)
                frm.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            }
            // -----------------------

            // 2. 새로운 뷰의 설정을 '채우기(Fill)'로 변경하여 패널 크기에 맞춤
            view.Dock = DockStyle.Fill;

            // 3. 패널에 추가하고 화면에 표시
            pnlMainContent.Controls.Add(view);

            view.Show(); // Form일 경우 Show()를 호출해야 화면에 나타납니다.
        }

        private void ShowContentInPanelUser(UserControl view)
        {
            // 1. 기존 화면 제거 및 메모리 해제
            // Controls를 역순으로 제거하는 것이 안전합니다.
            for (int i = pnlMainContent.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = pnlMainContent.Controls[i];
                pnlMainContent.Controls.RemoveAt(i);
                ctrl.Dispose(); // 이벤트 구독 해제 및 메모리 정리 (OnHandleDestroyed 호출됨)
            }

            // 2. 유저 컨트롤 기본 설정
            view.Dock = DockStyle.Fill;

            // 3. 패널에 추가
            // UserControl은 별도의 Show() 호출 없이 Add만으로 충분합니다.
            pnlMainContent.Controls.Add(view);
        }

        // ---------------------------------------------------------------------
        // 3. 메뉴 클릭 이벤트 (화면 이동)
        // ---------------------------------------------------------------------

        /// <summary>
        /// [대시보드] 메뉴 클릭 시
        /// </summary>
        private void OnOpenDashboardClick(object sender, EventArgs e)
        {
            // 1. DI 컨테이너에서 ViewModel을 생성(가져오기)
            var viewModel = _serviceProvider.GetService<DashboardViewModel>();

            if (viewModel == null)
            {
                MessageBox.Show("뷰모델을 로드할 수 없습니다.");
                return;
            }

            // 2. View(UserControl)를 생성하고 ViewModel을 주입
            var view = new DashboardForm(viewModel);

            // 3. 메인 패널에 표시
            ShowContentInPanelUser(view);
        }

        /// <summary>
        /// [작업지시 목록] 메뉴 클릭 시 (미구현 부분 구현 완료)
        /// </summary>
        private void OnOpenWorkOrderClick(object sender, EventArgs e)
        {
            // 2. DI 컨테이너(ServiceProvider)에서 뷰모델을 꺼냄
            // (Program.ServiceProvider 혹은 AppContext 등에 저장된 위치를 참조하세요)
            var viewModel = Program.ServiceProvider?.GetService<WorkOrderListViewModel>();

            if (viewModel == null)
            {
                MessageBox.Show("뷰모델을 로드할 수 없습니다.");
                return;
            }

            // 3. 표시할 뷰(Form 또는 UserControl) 생성
            // 만약 WorkOrderListForm이 Form이라면:
            var view = new WorkOrderListForm(viewModel);

            // 5. 메인 화면의 패널에 표시
            ShowContentInPanel(view);
        }

        /// <summary>
        /// [로그아웃] 버튼 클릭 시
        /// </summary>
        private void OnLogoutClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그아웃 하시겠습니까?", "확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 1. 세션 정보 삭제
                UserSession.Clear();

                // 2. 폼 상태를 Retry로 설정하여 Program.cs에서 다시 로그인창을 띄우게 유도
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        /// <summary>
        /// [프로그램 종료] 버튼 클릭 시
        /// </summary>
        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
