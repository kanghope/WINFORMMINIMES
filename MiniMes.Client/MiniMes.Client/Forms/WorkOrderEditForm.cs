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
    /// <summary>
    /// [View] 작업지시 등록/수정 팝업창 (WinForms 버전)
    /// WPF의 WorkOrderEditView.xaml.cs와 동일한 비즈니스 로직 흐름을 따릅니다.
    /// </summary>
    public partial class WorkOrderEditForm : Form
    {
        // WPF의 DataContext 역할을 수행할 뷰모델 변수
        private readonly WorkOrderEditViewModel _viewModel;
        /// <summary>
        /// 생성자: 뷰모델을 주입받아 화면을 초기화합니다.
        /// </summary>
        public WorkOrderEditForm(WorkOrderEditViewModel viewModel)
        {
            InitializeComponent(); // 디자인 요소 초기화

            _viewModel = viewModel;

            // 1. 데이터 바인딩 설정 (WPF의 Binding 문법 대체)
            InitBindings();

            // 2. 버튼 이벤트 연결
            btnSave.Click += SaveButton_Click;
            btnCancel.Click += CancelButton_Click;
        }

        /// <summary>
        /// 뷰모델과 WinForms 컨트롤을 연결합니다.
        /// </summary>
        private void InitBindings()
        {
            // 품목 코드: Text 속성 연결 및 읽기전용 상태 바인딩
            txtItemCode.DataBindings.Add("Text", _viewModel, nameof(_viewModel.ItemCode), true, DataSourceUpdateMode.OnPropertyChanged);
            txtItemCode.DataBindings.Add("ReadOnly", _viewModel, nameof(_viewModel.IsItemCodeReadOnly), true);

            // 지시 수량: Value 속성 연결 (NumericUpDown 사용)
            nudQuantity.DataBindings.Add("Value", _viewModel, nameof(_viewModel.Quantity), true, DataSourceUpdateMode.OnPropertyChanged);

            // 지시 ID: Text 속성 연결 (단방향 - 읽기전용)
            txtId.DataBindings.Add("Text", _viewModel, nameof(_viewModel.id), true);
        }

        // ---------------------------------------------------------------------
        // [저장 버튼] 클릭 시 실행 (WPF SaveButton_Click 이식)
        // ---------------------------------------------------------------------
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // WPF의 'if (DataContext is WorkOrderEditViewModel vm)' 로직과 동일
            // 1. 유효성 검사 실행
            if (_viewModel.Validate())
            {
                // [통과 시]
                // 뷰모델에 저장 상태를 알리고 변경사항을 확정(Commit)합니다.
                _viewModel.IsSaved = true;
                
                // 부모 창에 성공 알림 (WPF의 DialogResult = true)
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // [탈락 시]
                // 뷰모델의 에러 메시지를 사용자에게 경고창으로 보여줍니다.
                MessageBox.Show(_viewModel.ValidationMessage, "입력 오류",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------------------------------------------------------------------
        // [취소 버튼] 클릭 시 실행 (WPF CancelButton_Click 이식)
        // ---------------------------------------------------------------------
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // 저장하지 않음을 명시
            _viewModel.IsSaved = false;

            // 부모 창에 취소 알림 (WPF의 DialogResult = false)
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// (옵션) ESC 키를 누르면 취소되도록 처리
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
