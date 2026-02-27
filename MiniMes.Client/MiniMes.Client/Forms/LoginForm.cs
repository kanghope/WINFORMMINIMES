using MiniMes.Infrastructure.Interfaces; // 기존 인터페이스 참조
using MiniMes.Domain.Auth; // 기존 UserSession 활용
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMes.Client.ViewModels; // 뷰모델 참조

namespace MiniMes.Client.Forms
{
    public partial class LoginForm : Form
    {
        // 1. AuthService 대신 ViewModel을 주입받습니다.
        private readonly LoginViewModel _viewModel;

        // 1. 디자이너 전용 기본 생성자 (이게 없으면 상속 시 디자인 창이 안 열립니다)
        public LoginForm(LoginViewModel viewModel)
        {
            InitializeComponent();

            // 엔터 키 누를 시 btnLogin 클릭 이벤트 실행
            this.AcceptButton = btnLogin;

            //ESC 키 누를 시 창이 닫히게 하려면
            this.CancelButton = btnClose;

            _viewModel = viewModel;

            // 데이터 바인딩 (WPF의 Binding 대체)
            txtUserId.DataBindings.Add("Text", _viewModel, nameof(_viewModel.UserId),
                                        true, DataSourceUpdateMode.OnPropertyChanged);
            txtPassword.DataBindings.Add("Text", _viewModel, nameof(_viewModel.Password),
                                        true, DataSourceUpdateMode.OnPropertyChanged);
        }

      

        // WPF의 LoginCommand (ExecuteLoginAsync) 이식
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. 뷰모델의 로그인 로직 실행
            bool isSuccess = await _viewModel.ExecuteLoginAsync();

            if (isSuccess)
            {
                // 2. 윈폼 방식의 창 닫기
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("로그인 정보가 올바르지 않습니다.", "로그인 실패",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            Application.Exit();
        }

        
    }
}
