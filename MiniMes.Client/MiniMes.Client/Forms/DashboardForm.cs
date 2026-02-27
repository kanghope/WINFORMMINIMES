using MiniMes.Client.ViewModels;
using MiniMes.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace MiniMes.Client.Forms
{
    public partial class DashboardForm : UserControl
    {
        private readonly DashboardViewModel? _viewModel;

        public DashboardForm(DashboardViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;

            // 생성 시점에 바로 구독!
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            // 초기 데이터 바인딩 시뮬레이션
            UpdateUI();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // WinForms는 자동 바인딩이 약하므로 Invoke를 통해 UI를 갱신합니다.
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateUI));
            }
            else
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (_viewModel == null || this.IsDisposed) return;
            lblGoodValue.Text = _viewModel.TotalGood.ToString("N0");
            lblBadValue.Text = _viewModel.TotalBad.ToString("N0");
            lblRateValue.Text = $"{_viewModel.AchievementRate}%";
            lblActiveValue.Text = _viewModel.ActiveOrderCount.ToString("N0");
        }

        // 컨트롤이 해제될 때 이벤트 구독 취소
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
            base.OnHandleDestroyed(e);
        }
    }
}
