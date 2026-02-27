namespace MiniMes.Client.Forms
{
    partial class DashboardForm
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();

            // 4개의 대시보드 카드 생성
            this.pnlGood = new System.Windows.Forms.Panel();
            this.lblGoodTitle = new System.Windows.Forms.Label();
            this.lblGoodValue = new System.Windows.Forms.Label();

            this.pnlBad = new System.Windows.Forms.Panel();
            this.lblBadTitle = new System.Windows.Forms.Label();
            this.lblBadValue = new System.Windows.Forms.Label();

            this.pnlRate = new System.Windows.Forms.Panel();
            this.lblRateTitle = new System.Windows.Forms.Label();
            this.lblRateValue = new System.Windows.Forms.Label();

            this.pnlActive = new System.Windows.Forms.Panel();
            this.lblActiveTitle = new System.Windows.Forms.Label();
            this.lblActiveValue = new System.Windows.Forms.Label();

            this.tableLayoutPanel1.SuspendLayout();
            this.pnlGood.SuspendLayout();
            this.pnlBad.SuspendLayout();
            this.pnlRate.SuspendLayout();
            this.pnlActive.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "실시간 생산 현황 대시보드";

            // tableLayoutPanel1 (WPF의 UniformGrid 역할)
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 80);
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 400);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);

            // 카드 1: 오늘의 양품
            this.pnlGood.BackColor = System.Drawing.Color.FromArgb(227, 242, 253); // #E3F2FD
            this.pnlGood.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGood.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGood.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGood.Margin = new System.Windows.Forms.Padding(10);
            this.lblGoodTitle.Text = "오늘의 양품";
            this.lblGoodTitle.ForeColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.lblGoodTitle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblGoodTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGoodValue.Text = "0";
            this.lblGoodValue.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblGoodValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGoodValue.Dock = System.Windows.Forms.DockStyle.Fill;

            // 카드 2: 오늘의 불량
            this.pnlBad.BackColor = System.Drawing.Color.FromArgb(255, 235, 238); // #FFEBEE
            this.pnlBad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBad.Margin = new System.Windows.Forms.Padding(10);
            this.lblBadTitle.Text = "오늘의 불량";
            this.lblBadTitle.ForeColor = System.Drawing.Color.FromArgb(211, 47, 47);
            this.lblBadTitle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblBadTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBadValue.Text = "0";
            this.lblBadValue.ForeColor = System.Drawing.Color.FromArgb(211, 47, 47);
            this.lblBadValue.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblBadValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBadValue.Dock = System.Windows.Forms.DockStyle.Fill;

            // 카드 3: 생산 달성률
            this.pnlRate.BackColor = System.Drawing.Color.FromArgb(232, 245, 233); // #E8F5E9
            this.pnlRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRate.Margin = new System.Windows.Forms.Padding(10);
            this.lblRateTitle.Text = "생산 달성률";
            this.lblRateTitle.ForeColor = System.Drawing.Color.FromArgb(56, 142, 60);
            this.lblRateTitle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblRateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRateValue.Text = "0%";
            this.lblRateValue.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRateValue.Dock = System.Windows.Forms.DockStyle.Fill;

            // 카드 4: 진행 중인 작업
            this.pnlActive.BackColor = System.Drawing.Color.FromArgb(255, 243, 224); // #FFF3E0
            this.pnlActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActive.Margin = new System.Windows.Forms.Padding(10);
            this.lblActiveTitle.Text = "진행 중인 작업";
            this.lblActiveTitle.ForeColor = System.Drawing.Color.FromArgb(245, 124, 0);
            this.lblActiveTitle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblActiveTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActiveValue.Text = "0";
            this.lblActiveValue.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblActiveValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblActiveValue.Dock = System.Windows.Forms.DockStyle.Fill;

            // 컨트롤 조립
            this.pnlGood.Controls.Add(this.lblGoodValue);
            this.pnlGood.Controls.Add(this.lblGoodTitle);
            this.pnlBad.Controls.Add(this.lblBadValue);
            this.pnlBad.Controls.Add(this.lblBadTitle);
            this.pnlRate.Controls.Add(this.lblRateValue);
            this.pnlRate.Controls.Add(this.lblRateTitle);
            this.pnlActive.Controls.Add(this.lblActiveValue);
            this.pnlActive.Controls.Add(this.lblActiveTitle);

            this.tableLayoutPanel1.Controls.Add(this.pnlGood, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlBad, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlRate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlActive, 1, 1);

            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblTitle);

            this.Size = new System.Drawing.Size(800, 550);
            this.Text = "Dashboard";
            this.BackColor = System.Drawing.Color.White;

            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlGood.ResumeLayout(false);
            this.pnlBad.ResumeLayout(false);
            this.pnlRate.ResumeLayout(false);
            this.pnlActive.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlGood;
        private System.Windows.Forms.Label lblGoodTitle;
        private System.Windows.Forms.Label lblGoodValue;
        private System.Windows.Forms.Panel pnlBad;
        private System.Windows.Forms.Label lblBadTitle;
        private System.Windows.Forms.Label lblBadValue;
        private System.Windows.Forms.Panel pnlRate;
        private System.Windows.Forms.Label lblRateTitle;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Panel pnlActive;
        private System.Windows.Forms.Label lblActiveTitle;
        private System.Windows.Forms.Label lblActiveValue;
        #endregion
    }
}
