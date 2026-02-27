namespace MiniMes.Client
{
    partial class MainForm
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
            pnlSidebar = new Panel();
            pnlUser = new Panel();
            btnLogout = new Button();
            txtUserName = new Label();
            txtUserRole = new Label();
            pnlSeparator = new Panel();
            btnInventory = new Button();
            btnWorkOrder = new Button();
            btnDashboard = new Button();
            pnlLogo = new Panel();
            lblLogo = new Label();
            pnlMainContent = new Panel();
            pnlTopBar = new Panel();
            lblTitle = new Label();
            pnlSidebar.SuspendLayout();
            pnlUser.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlMainContent.SuspendLayout();
            pnlTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(33, 37, 41);
            pnlSidebar.Controls.Add(pnlUser);
            pnlSidebar.Controls.Add(btnInventory);
            pnlSidebar.Controls.Add(btnWorkOrder);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(230, 611);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlUser
            // 
            pnlUser.Controls.Add(btnLogout);
            pnlUser.Controls.Add(txtUserName);
            pnlUser.Controls.Add(txtUserRole);
            pnlUser.Controls.Add(pnlSeparator);
            pnlUser.Dock = DockStyle.Bottom;
            pnlUser.Location = new Point(0, 501);
            pnlUser.Name = "pnlUser";
            pnlUser.Size = new Size(230, 110);
            pnlUser.TabIndex = 4;
            // 
            // btnLogout
            // 
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("맑은 고딕", 8F);
            btnLogout.ForeColor = Color.Silver;
            btnLogout.Location = new Point(20, 65);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(190, 25);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "LOGOUT";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += OnLogoutClick;
            // 
            // txtUserName
            // 
            txtUserName.AutoSize = true;
            txtUserName.Font = new Font("맑은 고딕", 10F);
            txtUserName.ForeColor = Color.White;
            txtUserName.Location = new Point(20, 35);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(81, 19);
            txtUserName.TabIndex = 2;
            txtUserName.Text = "USER NAME";
            // 
            // txtUserRole
            // 
            txtUserRole.AutoSize = true;
            txtUserRole.Font = new Font("맑은 고딕", 8.5F, FontStyle.Bold);
            txtUserRole.ForeColor = Color.LimeGreen;
            txtUserRole.Location = new Point(20, 15);
            txtUserRole.Name = "txtUserRole";
            txtUserRole.Size = new Size(67, 15);
            txtUserRole.TabIndex = 1;
            txtUserRole.Text = "ROLE INFO";
            // 
            // pnlSeparator
            // 
            pnlSeparator.BackColor = Color.FromArgb(70, 70, 70);
            pnlSeparator.Location = new Point(15, 0);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.Size = new Size(200, 1);
            pnlSeparator.TabIndex = 0;
            // 
            // btnInventory
            // 
            btnInventory.Dock = DockStyle.Top;
            btnInventory.FlatAppearance.BorderSize = 0;
            btnInventory.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.Font = new Font("맑은 고딕", 10F);
            btnInventory.ForeColor = Color.White;
            btnInventory.Location = new Point(0, 180);
            btnInventory.Name = "btnInventory";
            btnInventory.Padding = new Padding(20, 0, 0, 0);
            btnInventory.Size = new Size(230, 50);
            btnInventory.TabIndex = 3;
            btnInventory.Text = "  📦  재고 관리 (미구현)";
            btnInventory.TextAlign = ContentAlignment.MiddleLeft;
            btnInventory.UseVisualStyleBackColor = true;
            // 
            // btnWorkOrder
            // 
            btnWorkOrder.Dock = DockStyle.Top;
            btnWorkOrder.FlatAppearance.BorderSize = 0;
            btnWorkOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            btnWorkOrder.FlatStyle = FlatStyle.Flat;
            btnWorkOrder.Font = new Font("맑은 고딕", 10F);
            btnWorkOrder.ForeColor = Color.White;
            btnWorkOrder.Location = new Point(0, 130);
            btnWorkOrder.Name = "btnWorkOrder";
            btnWorkOrder.Padding = new Padding(20, 0, 0, 0);
            btnWorkOrder.Size = new Size(230, 50);
            btnWorkOrder.TabIndex = 2;
            btnWorkOrder.Text = "  📋  작업 지시 관리";
            btnWorkOrder.TextAlign = ContentAlignment.MiddleLeft;
            btnWorkOrder.UseVisualStyleBackColor = true;
            btnWorkOrder.Click += OnOpenWorkOrderClick;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("맑은 고딕", 10F);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 80);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(20, 0, 0, 0);
            btnDashboard.Size = new Size(230, 50);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "  📊  실시간 대시보드";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += OnOpenDashboardClick;
            // 
            // pnlLogo
            // 
            pnlLogo.Controls.Add(lblLogo);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(230, 80);
            pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Fill;
            lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(230, 80);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "Mini MES";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.FromArgb(244, 247, 252);
            pnlMainContent.Controls.Add(pnlTopBar);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(230, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(854, 611);
            pnlMainContent.TabIndex = 1;
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(lblTitle);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(854, 60);
            pnlTopBar.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(134, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "시스템 대시보드";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 800);
            Controls.Add(pnlMainContent);
            Controls.Add(pnlSidebar);
            MinimumSize = new Size(1280, 800);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mini MES System";
            pnlSidebar.ResumeLayout(false);
            pnlUser.ResumeLayout(false);
            pnlUser.PerformLayout();
            pnlLogo.ResumeLayout(false);
            pnlMainContent.ResumeLayout(false);
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion 

        private Panel pnlSidebar;
        private Panel pnlMainContent;
        private Button btnInventory;
        private Button btnWorkOrder;
        private Button btnDashboard;
        private Panel pnlLogo;
        private Label lblLogo;
        private Panel pnlTopBar;
        private Label lblTitle;
        private Panel pnlUser;
        private Label txtUserRole;
        private Label txtUserName;
        private Button btnLogout;
        private Panel pnlSeparator;
    }
}