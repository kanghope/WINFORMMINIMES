namespace MiniMes.Client.Forms
{
    partial class WorkOrderEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            tlpMain = new TableLayoutPanel();
            lblItemCode = new Label();
            txtItemCode = new TextBox();
            lblQuantity = new Label();
            nudQuantity = new NumericUpDown();
            lblId = new Label();
            txtId = new TextBox();
            flpButtons = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            flpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(lblItemCode, 0, 0);
            tlpMain.Controls.Add(txtItemCode, 1, 0);
            tlpMain.Controls.Add(lblQuantity, 0, 1);
            tlpMain.Controls.Add(nudQuantity, 1, 1);
            tlpMain.Controls.Add(lblId, 0, 2);
            tlpMain.Controls.Add(txtId, 1, 2);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(20);
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.Size = new Size(380, 180);
            tlpMain.TabIndex = 0;
            // 
            // lblItemCode
            // 
            lblItemCode.Dock = DockStyle.Fill;
            lblItemCode.Location = new Point(23, 20);
            lblItemCode.Name = "lblItemCode";
            lblItemCode.Size = new Size(94, 40);
            lblItemCode.TabIndex = 0;
            lblItemCode.Text = "품목 코드";
            lblItemCode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtItemCode
            // 
            txtItemCode.Dock = DockStyle.Fill;
            txtItemCode.Location = new Point(120, 25);
            txtItemCode.Margin = new Padding(0, 5, 0, 5);
            txtItemCode.Name = "txtItemCode";
            txtItemCode.Size = new Size(240, 23);
            txtItemCode.TabIndex = 1;
            // 
            // lblQuantity
            // 
            lblQuantity.Dock = DockStyle.Fill;
            lblQuantity.Location = new Point(23, 60);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(94, 40);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "지시 수량";
            lblQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudQuantity
            // 
            nudQuantity.Dock = DockStyle.Fill;
            nudQuantity.Location = new Point(120, 65);
            nudQuantity.Margin = new Padding(0, 5, 0, 5);
            nudQuantity.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(240, 23);
            nudQuantity.TabIndex = 3;
            // 
            // lblId
            // 
            lblId.Dock = DockStyle.Fill;
            lblId.Location = new Point(23, 100);
            lblId.Name = "lblId";
            lblId.Size = new Size(94, 60);
            lblId.TabIndex = 4;
            lblId.Text = "지시 ID";
            lblId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtId
            // 
            txtId.BackColor = Color.WhiteSmoke;
            txtId.Dock = DockStyle.Fill;
            txtId.Location = new Point(120, 105);
            txtId.Margin = new Padding(0, 5, 0, 5);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(240, 23);
            txtId.TabIndex = 5;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnCancel);
            flpButtons.Controls.Add(btnSave);
            flpButtons.Dock = DockStyle.Bottom;
            flpButtons.FlowDirection = FlowDirection.RightToLeft;
            flpButtons.Location = new Point(0, 180);
            flpButtons.Name = "flpButtons";
            flpButtons.Padding = new Padding(0, 10, 20, 0);
            flpButtons.Size = new Size(380, 60);
            flpButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(255, 230, 230);
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(255, 150, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(267, 13);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 122, 204);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(171, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 35);
            btnSave.TabIndex = 1;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // WorkOrderEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 240);
            Controls.Add(tlpMain);
            Controls.Add(flpButtons);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorkOrderEditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "작업 지시 등록/수정";
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity; // 수량은 숫자가 안전하므로 NumericUpDown 권장
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        #endregion
    }
}