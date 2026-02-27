namespace MiniMes.Client.Forms
{
    partial class WorkResultRegisterForm
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
            txtOrderQuant = new TextBox();
            txtBadQuantity = new TextBox();
            lblItemCode = new Label();
            txtItemCode = new TextBox();
            lblGoodQuintity = new Label();
            txtGoodQuantity = new TextBox();
            lblBadQuantity = new Label();
            lblQuantity = new Label();
            flpButtons = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            tlpMain.SuspendLayout();
            flpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(txtOrderQuant, 1, 1);
            tlpMain.Controls.Add(txtBadQuantity, 1, 3);
            tlpMain.Controls.Add(lblItemCode, 0, 0);
            tlpMain.Controls.Add(txtItemCode, 1, 0);
            tlpMain.Controls.Add(lblGoodQuintity, 0, 2);
            tlpMain.Controls.Add(txtGoodQuantity, 1, 2);
            tlpMain.Controls.Add(lblBadQuantity, 0, 3);
            tlpMain.Controls.Add(lblQuantity, 0, 1);
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(20);
            tlpMain.RowCount = 4;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpMain.Size = new Size(486, 228);
            tlpMain.TabIndex = 0;
            // 
            // txtOrderQuant
            // 
            txtOrderQuant.Dock = DockStyle.Fill;
            txtOrderQuant.Location = new Point(120, 65);
            txtOrderQuant.Margin = new Padding(0, 5, 0, 5);
            txtOrderQuant.Name = "txtOrderQuant";
            txtOrderQuant.ReadOnly = true;
            txtOrderQuant.Size = new Size(346, 23);
            txtOrderQuant.TabIndex = 8;
            // 
            // txtBadQuantity
            // 
            txtBadQuantity.BackColor = Color.White;
            txtBadQuantity.Dock = DockStyle.Fill;
            txtBadQuantity.Location = new Point(120, 145);
            txtBadQuantity.Margin = new Padding(0, 5, 0, 5);
            txtBadQuantity.Name = "txtBadQuantity";
            txtBadQuantity.Size = new Size(346, 23);
            txtBadQuantity.TabIndex = 7;
            // 
            // lblItemCode
            // 
            lblItemCode.Location = new Point(23, 20);
            lblItemCode.Name = "lblItemCode";
            lblItemCode.Size = new Size(94, 35);
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
            txtItemCode.ReadOnly = true;
            txtItemCode.Size = new Size(346, 23);
            txtItemCode.TabIndex = 1;
            // 
            // lblGoodQuintity
            // 
            lblGoodQuintity.Location = new Point(23, 100);
            lblGoodQuintity.Name = "lblGoodQuintity";
            lblGoodQuintity.Size = new Size(94, 25);
            lblGoodQuintity.TabIndex = 4;
            lblGoodQuintity.Text = "양품수량(Good)";
            lblGoodQuintity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGoodQuantity
            // 
            txtGoodQuantity.BackColor = Color.White;
            txtGoodQuantity.Dock = DockStyle.Fill;
            txtGoodQuantity.Location = new Point(120, 105);
            txtGoodQuantity.Margin = new Padding(0, 5, 0, 5);
            txtGoodQuantity.Name = "txtGoodQuantity";
            txtGoodQuantity.Size = new Size(346, 23);
            txtGoodQuantity.TabIndex = 5;
            // 
            // lblBadQuantity
            // 
            lblBadQuantity.Location = new Point(23, 140);
            lblBadQuantity.Name = "lblBadQuantity";
            lblBadQuantity.Size = new Size(94, 25);
            lblBadQuantity.TabIndex = 6;
            lblBadQuantity.Text = "불량 수량(Bad)";
            lblBadQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQuantity
            // 
            lblQuantity.Location = new Point(23, 60);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(94, 28);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "지시 수량";
            lblQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnCancel);
            flpButtons.Controls.Add(btnSave);
            flpButtons.Dock = DockStyle.Bottom;
            flpButtons.FlowDirection = FlowDirection.RightToLeft;
            flpButtons.Location = new Point(0, 228);
            flpButtons.Name = "flpButtons";
            flpButtons.Padding = new Padding(0, 10, 20, 0);
            flpButtons.Size = new Size(486, 60);
            flpButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(255, 230, 230);
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(255, 150, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(373, 13);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 122, 204);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(247, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 1;
            btnSave.Text = "실적 저장 및 완료";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // WorkResultRegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 288);
            Controls.Add(tlpMain);
            Controls.Add(flpButtons);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorkResultRegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "작업 실적 등록";
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblGoodQuintity;
        private System.Windows.Forms.TextBox txtGoodQuantity;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private Label lblBadQuantity;
        private TextBox txtBadQuantity;
        private TextBox txtOrderQuant;
    }
}