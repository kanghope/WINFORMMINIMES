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
    public partial class WorkResultRegisterForm : Form
    {
        // WPF의 DataContext 역할을 수행할 뷰모델 변수
        private readonly WorkResultRegisterViewModel _viewModel;
        public WorkResultRegisterForm(WorkResultRegisterViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            // 1. 데이터 바인딩 설정 (WPF의 Binding 문법 대체)
            InitBindings();
        }

        /// <summary>
        /// 뷰모델과 WinForms 컨트롤을 연결합니다.
        /// </summary>
        private void InitBindings()
        {
            // 품목 코드: Text 속성 연결 및 읽기전용 상태 바인딩
            txtItemCode.DataBindings.Add("Text", _viewModel, nameof(_viewModel.ItemCode), true, DataSourceUpdateMode.Never);
            txtItemCode.ReadOnly = true;
            // 지시 수량: Text 속성 연결 및 읽기전용 상태 바인딩
            txtOrderQuant.DataBindings.Add("Text", _viewModel, nameof(_viewModel.OrderQuantity), true, DataSourceUpdateMode.Never);
            txtOrderQuant.ReadOnly = true;

            // 지시 수량: Value 속성 연결 (NumericUpDown 사용)
            txtGoodQuantity.DataBindings.Add("Text", _viewModel, nameof(_viewModel.GoodQuantity), true, DataSourceUpdateMode.OnPropertyChanged);

            txtBadQuantity.DataBindings.Add("Text", _viewModel, nameof(_viewModel.BadQuantity), true, DataSourceUpdateMode.OnPropertyChanged);

        }
        /// <summary>
        /// 뷰모델과 WinForms 컨트롤을 연결합니다.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // WPF의 'if (DataContext is WorkOrderEditViewModel vm)' 로직과 동일
            // 1. 유효성 검사 실행
            if (_viewModel.Validate())
            {
                _viewModel.ExecuteRegisterAsync();
                // [통과 시]
                // 뷰모델에 저장이 끝나면 isSaved를 true로 바꿔주도록 설계되어있음
                if(_viewModel.IsSaved)
                {
                    // 부모 창에 성공 알림 (WPF의 DialogResult = true)
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                // [탈락 시]
                // 뷰모델의 에러 메시지를 사용자에게 경고창으로 보여줍니다.
                MessageBox.Show(_viewModel.ValidationMessage, "입력 오류",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 저장하지 않음을 명시
            _viewModel.IsSaved = false;

            // 부모 창에 취소 알림 (WPF의 DialogResult = false)
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
