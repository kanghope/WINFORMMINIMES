using MiniMes.Domain.Auth;
using MiniMes.Infrastructure.Interfaces;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace MiniMes.Client.ViewModels
{
    public class LoginViewModel :INotifyPropertyChanged
    {
        private readonly IAuthService _authService;

        private string _userId = string.Empty;
        private string _password = string.Empty;
        private bool _isBusy;

        public string UserId
        {
            get => _userId;
            set { _userId = value; OnPropertyChanged(nameof(UserId)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        // 로그인 중 버튼 비활성화 등을 위한 상태값
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        // WinForms에서는 ICommand(RelayCommand)보다는 직접 async 메서드를 호출하는 게 더 직관적입니다.
        public async Task<bool> ExecuteLoginAsync()
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
                return false;

            IsBusy = true;
            try
            {
                var user = await _authService.AuthenticateAsync(UserId, Password);

                if (user != null)
                {
                    // 1. 세션 정보 저장
                    UserSession.UserId = user.USER_ID;
                    UserSession.UserName = user.USER_NAME;
                    UserSession.UserRole = user.USER_ROLE;
                    UserSession.LoginTime = DateTime.Now;

                    return true; // 성공 시 true 반환
                }
                return false; // 실패 시 false 반환
            }
            finally
            {
                IsBusy = false;
            }
        }

        // ---------------------------------------------------------------------
        // 6. 알림 인터페이스 (WPF의 핵심)
        // ---------------------------------------------------------------------
        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
