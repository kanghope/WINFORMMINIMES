namespace MiniMes.Client.Forms
{
    partial class LoginForm
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
        private void InitializeComponent()
        {
            txtUserId = new TextBox();
            txtPassword = new TextBox();
            lblMessage = new Label();
            btnLogin = new Button();
            panel1 = new Panel();
            lblTitle = new Label();
            btnClose = new Button();
            lblUserID = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(236, 150);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(199, 23);
            txtUserId.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(236, 192);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(199, 23);
            txtPassword.TabIndex = 2;
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(0, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(100, 23);
            lblMessage.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(41, 128, 185);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("맑은 고딕", 10F);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(185, 280);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(250, 40);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "LOGIN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(41, 128, 185);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 361);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 18F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(180, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(202, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Mini MES System";
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.Silver;
            btnClose.Location = new Point(185, 330);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(250, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "시스템 종료";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblUserID
            // 
            lblUserID.AutoSize = true;
            lblUserID.ForeColor = SystemColors.Highlight;
            lblUserID.Location = new Point(180, 153);
            lblUserID.Name = "lblUserID";
            lblUserID.Size = new Size(43, 15);
            lblUserID.TabIndex = 0;
            lblUserID.Text = "아이디";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(180, 195);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 7;
            label1.Text = "비밀번호";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(484, 361);
            Controls.Add(label1);
            Controls.Add(lblUserID);
            Controls.Add(btnClose);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUserId);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox txtUserId;
        private TextBox txtPassword;
        private Label lblMessage;
        private Button btnLogin;
        private Panel panel1;
        private Label lblTitle;
        private Button btnClose;
        private Label lblUserID;
        private Label label1;
    }
}