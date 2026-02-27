using Microsoft.Extensions.DependencyInjection;
using MiniMes.Client.Forms; // MainForm, LoginForm이 있는 곳
using MiniMes.Infrastructure.Interfaces;
using MiniMes.Infrastructure.Services;
using MiniMES.Infastructure.interfaces;
using MiniMES.Infastructure.Services;
using MiniMes.Domain.Auth;
using MiniMes.Client.ViewModels;

namespace MiniMes.Client
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. DI 컨테이너 설정 및 서비스 등록
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // 2. PLC 서비스 미리 활성화 (WPF의 OnStartup 로직)
            var plcService = ServiceProvider.GetRequiredService<SerialDeviceService>();

            // 3. 실행 루프 시작 (로그아웃 대응)
            while (true)
            {
                // 로그인 창 띄우기 (DI 컨테이너에서 가져옴)
                using (var loginForm = ServiceProvider.GetRequiredService<LoginForm>())
                {
                    // 로그인 성공 시 (DialogResult.OK)
                    if (loginForm.ShowDialog() == DialogResult.OK && UserSession.IsLoggedIn)
                    {
                        // 메인 창 띄우기
                        using (var mainForm = ServiceProvider.GetRequiredService<MainForm>())
                        {
                            var result = mainForm.ShowDialog();

                            // 메인 폼이 'Retry'를 반환하며 닫히면 다시 로그인 루프로 (로그아웃 버튼 클릭 시)
                            if (result == DialogResult.Retry) continue;

                            // 그 외(X 버튼 등)는 프로그램 종료
                            break;
                        }
                    }
                    else
                    {
                        // 로그인 취소 시 프로그램 종료
                        break;
                    }
                }
            }

            // 4. 종료 처리 (WPF의 OnExit)
            plcService?.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // --- 인프라 및 서비스 등록 (Singleton) ---
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IWorkOrderService, WorkOrderService>();
            services.AddSingleton<IWorkResultService, WorkResultServicePro>();
            services.AddSingleton<IDashboardService, DashboardService>();
            services.AddSingleton<IWorkOrderRepository, WorkOrderRepository>();

            // SerialDeviceService 등록 (WPF 로직 그대로 유지)
            services.AddSingleton<SerialDeviceService>(sp =>
            {
                var repo = sp.GetRequiredService<IWorkOrderRepository>();
                return new SerialDeviceService(repo);
            });

            // --- 뷰모델 등록 (Transient) ---
            services.AddTransient<LoginViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<WorkOrderListViewModel>();
            services.AddTransient<WorkOrderEditViewModel>();
            services.AddTransient<WorkResultListViewModel>();
            services.AddTransient<WorkResultRegisterViewModel>();

            // --- WinForms 화면 등록 ---
            // WPF의 View 대신 WinForms의 Form을 등록합니다.
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
        }
    }
}