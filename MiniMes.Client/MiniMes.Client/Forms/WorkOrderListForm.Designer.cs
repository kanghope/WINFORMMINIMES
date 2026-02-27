namespace MiniMes.Client.Forms
{
    partial class WorkOrderListForm
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
            pnlSearch = new Panel();
            lblShow = new Label();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            cbStatus = new ComboBox();
            txtSearch = new TextBox();
            btnSearch = new Button();
            lblStats = new Label();
            dgvWorkOrders = new DataGridView();
            pnlButtonArea = new Panel();
            pgbLoading = new ProgressBar();
            btnCalculate = new Button();
            btnRefresh = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnStart = new Button();
            btnStop = new Button();
            btnRegisterResult = new Button();
            btnViewResults = new Button();
            pnlFooter = new Panel();
            grpLogs = new GroupBox();
            lbLogs = new ListBox();
            pnlStatusInfo = new Panel();
            lblStatusLamp = new Label();
            lblStatusText = new Label();
            lblLastTime = new Label();
            lblPacketCount = new Label();
            tlpMain.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorkOrders).BeginInit();
            pnlButtonArea.SuspendLayout();
            pnlFooter.SuspendLayout();
            grpLogs.SuspendLayout();
            pnlStatusInfo.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlSearch, 0, 0);
            tlpMain.Controls.Add(lblStats, 0, 1);
            tlpMain.Controls.Add(dgvWorkOrders, 0, 2);
            tlpMain.Controls.Add(pnlButtonArea, 0, 3);
            tlpMain.Controls.Add(pnlFooter, 0, 4);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Margin = new Padding(3, 4, 3, 4);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 5;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 225F));
            tlpMain.Size = new Size(984, 938);
            tlpMain.TabIndex = 0;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(lblShow);
            pnlSearch.Controls.Add(dtpStart);
            pnlSearch.Controls.Add(dtpEnd);
            pnlSearch.Controls.Add(cbStatus);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Dock = DockStyle.Fill;
            pnlSearch.Location = new Point(3, 4);
            pnlSearch.Margin = new Padding(3, 4, 3, 4);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(978, 61);
            pnlSearch.TabIndex = 0;
            // 
            // lblShow
            // 
            lblShow.AutoSize = true;
            lblShow.Location = new Point(259, 24);
            lblShow.Name = "lblShow";
            lblShow.Size = new Size(15, 15);
            lblShow.TabIndex = 5;
            lblShow.Text = "~";
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(50, 21);
            dtpStart.Margin = new Padding(3, 4, 3, 4);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 0;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(281, 21);
            dtpEnd.Margin = new Padding(3, 4, 3, 4);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 1;
            // 
            // cbStatus
            // 
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Location = new Point(499, 21);
            cbStatus.Margin = new Padding(3, 4, 3, 4);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(121, 23);
            cbStatus.TabIndex = 2;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(674, 21);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(137, 23);
            txtSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 122, 204);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(845, 12);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(90, 38);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "🔍 조회";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // lblStats
            // 
            lblStats.BackColor = Color.FromArgb(240, 240, 240);
            lblStats.Dock = DockStyle.Fill;
            lblStats.Location = new Point(3, 69);
            lblStats.Name = "lblStats";
            lblStats.Padding = new Padding(15, 0, 0, 0);
            lblStats.Size = new Size(978, 44);
            lblStats.TabIndex = 1;
            lblStats.Text = "📊 생산 현황 요약: 대기 중...";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvWorkOrders
            // 
            dgvWorkOrders.AllowUserToAddRows = false;
            dgvWorkOrders.BackgroundColor = Color.White;
            dgvWorkOrders.BorderStyle = BorderStyle.None;
            dgvWorkOrders.Dock = DockStyle.Fill;
            dgvWorkOrders.Location = new Point(3, 117);
            dgvWorkOrders.Margin = new Padding(3, 4, 3, 4);
            dgvWorkOrders.Name = "dgvWorkOrders";
            dgvWorkOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWorkOrders.Size = new Size(978, 530);
            dgvWorkOrders.TabIndex = 2;
            // 
            // pnlButtonArea
            // 
            pnlButtonArea.BackColor = Color.White;
            pnlButtonArea.Controls.Add(pgbLoading);
            pnlButtonArea.Controls.Add(btnCalculate);
            pnlButtonArea.Controls.Add(btnRefresh);
            pnlButtonArea.Controls.Add(btnAdd);
            pnlButtonArea.Controls.Add(btnEdit);
            pnlButtonArea.Controls.Add(btnDelete);
            pnlButtonArea.Controls.Add(btnStart);
            pnlButtonArea.Controls.Add(btnStop);
            pnlButtonArea.Controls.Add(btnRegisterResult);
            pnlButtonArea.Controls.Add(btnViewResults);
            pnlButtonArea.Dock = DockStyle.Fill;
            pnlButtonArea.Location = new Point(3, 655);
            pnlButtonArea.Margin = new Padding(3, 4, 3, 4);
            pnlButtonArea.Name = "pnlButtonArea";
            pnlButtonArea.Size = new Size(978, 54);
            pnlButtonArea.TabIndex = 3;
            // 
            // pgbLoading
            // 
            pgbLoading.Anchor = AnchorStyles.None;
            pgbLoading.Location = new Point(223, 10);
            pgbLoading.Name = "pgbLoading";
            pgbLoading.Size = new Size(400, 30);
            pgbLoading.Style = ProgressBarStyle.Continuous;
            pgbLoading.TabIndex = 0;
            // 
            // btnCalculate
            // 
            btnCalculate.BackColor = Color.FromArgb(70, 70, 70);
            btnCalculate.FlatStyle = FlatStyle.Flat;
            btnCalculate.ForeColor = Color.White;
            btnCalculate.Location = new Point(10, 10);
            btnCalculate.Margin = new Padding(3, 4, 3, 4);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(85, 42);
            btnCalculate.TabIndex = 0;
            btnCalculate.Text = "합계 계산";
            btnCalculate.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(70, 70, 70);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(100, 10);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(85, 42);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "새로 고침";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(0, 122, 204);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(190, 10);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(85, 42);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "지시 등록";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(0, 122, 204);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(280, 10);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(85, 42);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "지시 수정";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(0, 122, 204);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(370, 10);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(85, 42);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "지시 삭제";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(39, 174, 96);
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(470, 10);
            btnStart.Margin = new Padding(3, 4, 3, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(120, 42);
            btnStart.TabIndex = 5;
            btnStart.Text = "작업 시작(PLC)";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(192, 57, 43);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(595, 10);
            btnStop.Margin = new Padding(3, 4, 3, 4);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(120, 42);
            btnStop.TabIndex = 6;
            btnStop.Text = "작업 종료(PLC)";
            btnStop.UseVisualStyleBackColor = false;
            // 
            // btnRegisterResult
            // 
            btnRegisterResult.BackColor = Color.FromArgb(0, 122, 204);
            btnRegisterResult.FlatStyle = FlatStyle.Flat;
            btnRegisterResult.ForeColor = Color.White;
            btnRegisterResult.Location = new Point(730, 10);
            btnRegisterResult.Margin = new Padding(3, 4, 3, 4);
            btnRegisterResult.Name = "btnRegisterResult";
            btnRegisterResult.Size = new Size(110, 42);
            btnRegisterResult.TabIndex = 7;
            btnRegisterResult.Text = "실적 등록/완료";
            btnRegisterResult.UseVisualStyleBackColor = false;
            // 
            // btnViewResults
            // 
            btnViewResults.BackColor = Color.FromArgb(70, 70, 70);
            btnViewResults.FlatStyle = FlatStyle.Flat;
            btnViewResults.ForeColor = Color.White;
            btnViewResults.Location = new Point(845, 10);
            btnViewResults.Margin = new Padding(3, 4, 3, 4);
            btnViewResults.Name = "btnViewResults";
            btnViewResults.Size = new Size(85, 42);
            btnViewResults.TabIndex = 8;
            btnViewResults.Text = "실적 조회";
            btnViewResults.UseVisualStyleBackColor = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = SystemColors.Control;
            pnlFooter.Controls.Add(grpLogs);
            pnlFooter.Controls.Add(pnlStatusInfo);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Location = new Point(3, 717);
            pnlFooter.Margin = new Padding(3, 4, 3, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(978, 217);
            pnlFooter.TabIndex = 4;
            // 
            // grpLogs
            // 
            grpLogs.Controls.Add(lbLogs);
            grpLogs.Location = new Point(10, 6);
            grpLogs.Margin = new Padding(3, 4, 3, 4);
            grpLogs.Name = "grpLogs";
            grpLogs.Padding = new Padding(3, 4, 3, 4);
            grpLogs.Size = new Size(725, 200);
            grpLogs.TabIndex = 0;
            grpLogs.TabStop = false;
            grpLogs.Text = "📡 PLC 실시간 통신 로그";
            // 
            // lbLogs
            // 
            lbLogs.BackColor = Color.FromArgb(30, 30, 30);
            lbLogs.BorderStyle = BorderStyle.None;
            lbLogs.ForeColor = Color.Lime;
            lbLogs.ItemHeight = 15;
            lbLogs.Location = new Point(3, 20);
            lbLogs.Margin = new Padding(3, 4, 3, 4);
            lbLogs.Name = "lbLogs";
            lbLogs.Size = new Size(700, 165);
            lbLogs.TabIndex = 0;
            // 
            // pnlStatusInfo
            // 
            pnlStatusInfo.BackColor = Color.FromArgb(248, 249, 250);
            pnlStatusInfo.Controls.Add(lblStatusLamp);
            pnlStatusInfo.Controls.Add(lblStatusText);
            pnlStatusInfo.Controls.Add(lblLastTime);
            pnlStatusInfo.Controls.Add(lblPacketCount);
            pnlStatusInfo.Location = new Point(741, 16);
            pnlStatusInfo.Margin = new Padding(3, 4, 3, 4);
            pnlStatusInfo.Name = "pnlStatusInfo";
            pnlStatusInfo.Padding = new Padding(10);
            pnlStatusInfo.Size = new Size(230, 175);
            pnlStatusInfo.TabIndex = 1;
            // 
            // lblStatusLamp
            // 
            lblStatusLamp.Location = new Point(10, 15);
            lblStatusLamp.Name = "lblStatusLamp";
            lblStatusLamp.Size = new Size(15, 15);
            lblStatusLamp.TabIndex = 0;
            // 
            // lblStatusText
            // 
            lblStatusText.AutoSize = true;
            lblStatusText.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            lblStatusText.Location = new Point(30, 13);
            lblStatusText.Name = "lblStatusText";
            lblStatusText.Size = new Size(55, 15);
            lblStatusText.TabIndex = 1;
            lblStatusText.Text = "연결상태";
            // 
            // lblLastTime
            // 
            lblLastTime.AutoSize = true;
            lblLastTime.Location = new Point(10, 60);
            lblLastTime.Name = "lblLastTime";
            lblLastTime.Size = new Size(12, 15);
            lblLastTime.TabIndex = 2;
            lblLastTime.Text = "-";
            // 
            // lblPacketCount
            // 
            lblPacketCount.AutoSize = true;
            lblPacketCount.Location = new Point(10, 100);
            lblPacketCount.Name = "lblPacketCount";
            lblPacketCount.Size = new Size(14, 15);
            lblPacketCount.TabIndex = 3;
            lblPacketCount.Text = "0";
            // 
            // WorkOrderListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 938);
            Controls.Add(tlpMain);
            Margin = new Padding(3, 4, 3, 4);
            Name = "WorkOrderListForm";
            Text = "작업지시 관리 시스템 (PLC 연동)";
            tlpMain.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorkOrders).EndInit();
            pnlButtonArea.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            grpLogs.ResumeLayout(false);
            pnlStatusInfo.ResumeLayout(false);
            pnlStatusInfo.PerformLayout();
            ResumeLayout(false);
        }




        // 선언부
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.DataGridView dgvWorkOrders;
        private System.Windows.Forms.Panel pnlButtonArea;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.ListBox lbLogs;
        private System.Windows.Forms.Panel pnlStatusInfo;

        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRegisterResult;
        private System.Windows.Forms.Button btnViewResults;

        private System.Windows.Forms.Label lblStatusLamp;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Label lblLastTime;
        private System.Windows.Forms.Label lblPacketCount;

        // 추가된 제목 레이블 선언
        private System.Windows.Forms.Label lblTimeTitle;
        private System.Windows.Forms.Label lblPacketTitle;
        // 선언부 제일 아래쪽에 추가
        private System.Windows.Forms.ProgressBar pgbLoading;

        #endregion

        private Label lblShow;
    }
}
