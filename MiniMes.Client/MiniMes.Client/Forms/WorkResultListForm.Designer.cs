using System.Windows.Forms;

namespace MiniMes.Client.Forms
{
    partial class WorkResultListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 컨트롤 선언
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblWorkOrderId;
        private System.Windows.Forms.Label lblOrderQty;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        //private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblStatus = new Label();
            lblOrderQty = new Label();
            lblWorkOrderId = new Label();
            lblItemCode = new Label();
            pnlFooter = new Panel();
            btnClose = new Button();
            btnRefresh = new Button();
            dgvResults = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(238, 238, 238);
            pnlHeader.Controls.Add(lblStatus);
            pnlHeader.Controls.Add(lblOrderQty);
            pnlHeader.Controls.Add(lblWorkOrderId);
            pnlHeader.Controls.Add(lblItemCode);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(10, 10);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(15);
            pnlHeader.Size = new Size(564, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(0, 120, 215);
            lblStatus.Location = new Point(280, 45);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(71, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "현재 상태: -";
            // 
            // lblOrderQty
            // 
            lblOrderQty.AutoSize = true;
            lblOrderQty.Location = new Point(140, 45);
            lblOrderQty.Name = "lblOrderQty";
            lblOrderQty.Size = new Size(73, 15);
            lblOrderQty.TabIndex = 1;
            lblOrderQty.Text = "지시 수량: 0";
            // 
            // lblWorkOrderId
            // 
            lblWorkOrderId.AutoSize = true;
            lblWorkOrderId.Location = new Point(17, 45);
            lblWorkOrderId.Name = "lblWorkOrderId";
            lblWorkOrderId.Size = new Size(59, 15);
            lblWorkOrderId.TabIndex = 2;
            lblWorkOrderId.Text = "지시 ID: -";
            // 
            // lblItemCode
            // 
            lblItemCode.AutoSize = true;
            lblItemCode.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            lblItemCode.Location = new Point(15, 15);
            lblItemCode.Name = "lblItemCode";
            lblItemCode.Size = new Size(89, 20);
            lblItemCode.TabIndex = 3;
            lblItemCode.Text = "품목 코드: -";
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(btnRefresh);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(10, 340);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(564, 50);
            pnlFooter.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(255, 230, 230);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(255, 150, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(447, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 35);
            btnClose.TabIndex = 0;
            btnClose.Text = "취소";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(0, 122, 204);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(351, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 35);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "새로고침";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(225, 225, 225);
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvResults.ColumnHeadersHeight = 35;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvResults.DefaultCellStyle = dataGridViewCellStyle2;
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.GridColor = Color.LightGray;
            dgvResults.Location = new Point(10, 90);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.RowTemplate.Height = 30;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(564, 250);
            dgvResults.TabIndex = 1;
            // 
            // WorkResultListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(584, 400);
            Controls.Add(dgvResults);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new Font("맑은 고딕", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorkResultListForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "작업 실적 조회";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}