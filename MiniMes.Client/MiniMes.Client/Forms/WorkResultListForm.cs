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

namespace MiniMes.Client.Forms
{
    public partial class WorkResultListForm : Form
    {
        private readonly WorkResultListViewModel _viewModel;
        public WorkResultListForm(WorkResultListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;

            // 1. 데이터 바인딩 초기화
            InitBindings();

            // 2. 폼이 로드될 때 자동으로 데이터 조회
            this.Load += async (s, e) => await LoadDataAsync();
        }

        private void InitBindings()
        {
            // 상단 요약 정보 바인딩
            lblItemCode.DataBindings.Add("Text", _viewModel, nameof(_viewModel.ItemCode), true, DataSourceUpdateMode.Never, "", "품목 코드: {0}");
            lblWorkOrderId.DataBindings.Add("Text", _viewModel, nameof(_viewModel.WorkOrderId), true, DataSourceUpdateMode.Never, "", "지시 ID: {0}");
            lblOrderQty.DataBindings.Add("Text", _viewModel, nameof(_viewModel.OrderQuantity), true, DataSourceUpdateMode.Never, "", "지시 수량: {0}");
            lblStatus.DataBindings.Add("Text", _viewModel, nameof(_viewModel.WorkOrderStatus), true, DataSourceUpdateMode.Never, "", "현재 상태: {0}");

            // 그리드 바인딩 (ObservableCollection은 BindingSource를 거치는 것이 안정적입니다)
            dgvResults.AutoGenerateColumns = false;
            dgvResults.DataSource = _viewModel.Results;

            // 컬럼 설정 (WPF의 Columns 구성과 동일하게)
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ResultId", HeaderText = "실적 ID", Width = 70 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GoodQuantity", HeaderText = "양품 수량", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BadQuantity", HeaderText = "불량 수량", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ResultDate",
                HeaderText = "등록 시간",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" }
            });
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                btnRefresh.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //그리드의 현재 선택이나 포커스를 해제하여 충돌방지
                dgvResults.DataSource = null;

                await _viewModel.ExecuteLoadResultsAsync();

                dgvResults.DataSource = _viewModel.Results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 불러오는 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
