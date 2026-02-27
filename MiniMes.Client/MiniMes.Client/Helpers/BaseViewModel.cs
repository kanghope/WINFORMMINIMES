using Microsoft.Extensions.DependencyInjection;
using MiniMes.Client; // 프로젝트 이름이 MiniMes.Client인 경우
using MiniMes.Domain.Auth;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using MiniMes.Client.Forms;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public BaseViewModel()
    {
        // LoginViewModel이 아닌 다른 뷰모델이 생성될 때 로그인 체크
        if (this.GetType().Name != "LoginViewModel" && !UserSession.IsLoggedIn)
        {
            // WinForms는 UI 스레드 접근 방식이 다르므로, 
            // 현재 활성화된 폼(MainForm 등)이 있다면 그 스레드를 빌려 사용합니다.
            Task.Run(() => RedirectToLogin());
        }
    }

    private void RedirectToLogin()
    {
        // 1. 알림창 띄우기
        // 사용자가 '확인'을 누르기 전까지는 코드가 아래로 진행되지 않고 대기합니다.
        System.Windows.Forms.MessageBox.Show("세션이 만료되었거나 로그인이 필요합니다.\n다시 로그인해주세요.", "세션 만료",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

        // 2. 현재 열려 있는 모든 폼 목록 가져오기
        // 현재 떠 있는 모든 창(MainForm, 팝업창 등)의 리스트를 미리 뽑아둡니다.
        var allOpenForms = System.Windows.Forms.Application.OpenForms.Cast<Form>().ToList();

        // 3. 기존 창들을 화면에서 즉시 숨기기
        // Close()를 바로 하면 프로그램이 종료될 위험이 있으므로, 일단 눈에 안 보이게 Hide() 처리합니다.
        foreach (var form in allOpenForms)
        {
            form.Hide();
        }

        // 4. DI 컨테이너에서 새로운 로그인 폼 생성
        // 'using'을 사용하면 로그인 창이 닫힐 때 메모리에서 깔끔하게 해제됩니다.
        using (var loginForm = Program.ServiceProvider?.GetRequiredService<LoginForm>())
        {
            if (loginForm != null && loginForm.ShowDialog() == DialogResult.OK && UserSession.IsLoggedIn)
            {
                // [로그인 성공 시]

                // 5. 새로운 메인 폼 생성 및 표시
                // 새로운 세션 정보가 반영된 깨끗한 상태의 메인 폼을 띄웁니다.
                var mainForm = Program.ServiceProvider?.GetRequiredService<MainForm>();
                mainForm?.Show();

                // 6. 숨겨두었던 이전 폼들을 이제 진짜로 메모리에서 제거
                // 새로운 메인 폼이 떴으므로 기존 창들은 더 이상 필요 없습니다.
                foreach (var oldForm in allOpenForms)
                {
                    // 이미 위에서 Hide() 했으므로 화면 깜빡임 없이 조용히 닫힙니다.
                    oldForm.Close();
                    oldForm.Dispose(); // 메모리 해제 보장
                }
            }
            else
            {
                // [로그인 취소 또는 실패 시]
                // 로그인 창의 X를 누르거나 취소를 누르면 프로그램 전체를 종료합니다.
                System.Windows.Forms.Application.Exit();
            }
        }
    }

    // BaseViewModel 내부에 추가
    protected void InvokeOnUI(Action action)
    {
        // 현재 열려있는 폼이 하나라도 있다면 그 폼의 스레드를 이용해 실행합니다.
        if (System.Windows.Forms.Application.OpenForms.Count > 0)
        {
            var target = System.Windows.Forms.Application.OpenForms[0];
            if (target.InvokeRequired)
            {
                target.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

    // 권한 확인용 헬퍼 메서드
    protected bool CheckAdmin() => UserSession.UserRole == "ADMIN";

    // 속성 변경 알림 (CallerMemberName을 쓰면 nameof 생략 가능해서 더 편합니다)
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}