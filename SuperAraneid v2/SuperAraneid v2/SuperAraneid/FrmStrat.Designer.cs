namespace SuperAraneid
{
    partial class FrmStrat
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStrat));
            this.fbd_url = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_About = new System.Windows.Forms.Button();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.btn_shoudong = new System.Windows.Forms.Button();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.txt_TaskCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fbd_url
            // 
            this.fbd_url.Description = "请选择图片存放地址！如果不选文件将存在本程序默认文件夹";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "暂停";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_About
            // 
            this.btn_About.Location = new System.Drawing.Point(565, 10);
            this.btn_About.Name = "btn_About";
            this.btn_About.Size = new System.Drawing.Size(58, 23);
            this.btn_About.TabIndex = 10;
            this.btn_About.Text = "关于";
            this.btn_About.UseVisualStyleBackColor = true;
            this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(12, 12);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(153, 21);
            this.txt_url.TabIndex = 8;
            this.txt_url.Text = "http://tu.duowan.com/tu";
            // 
            // btn_shoudong
            // 
            this.btn_shoudong.Location = new System.Drawing.Point(326, 10);
            this.btn_shoudong.Name = "btn_shoudong";
            this.btn_shoudong.Size = new System.Drawing.Size(100, 23);
            this.btn_shoudong.TabIndex = 9;
            this.btn_shoudong.Text = "上吧！爬虫";
            this.btn_shoudong.UseVisualStyleBackColor = true;
            this.btn_shoudong.Click += new System.EventHandler(this.btn_shoudong_Click_1);
            // 
            // txt_Log
            // 
            this.txt_Log.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Log.Location = new System.Drawing.Point(12, 39);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(611, 309);
            this.txt_Log.TabIndex = 12;
            this.txt_Log.Text = "[Lorna]欢迎访问我的博客~\r\n特此声明：本软件仅供学习交流请勿用于非法用途\r\n源码详细讲解地址：http://www.lorna.com.cn\r\n博主码字不" +
    "容易，如果你觉得我讲的好可以在文章尾部打赏我\r\n转载请说明出处........................................\r\n";
            // 
            // txt_TaskCount
            // 
            this.txt_TaskCount.Location = new System.Drawing.Point(266, 12);
            this.txt_TaskCount.Name = "txt_TaskCount";
            this.txt_TaskCount.Size = new System.Drawing.Size(42, 21);
            this.txt_TaskCount.TabIndex = 13;
            this.txt_TaskCount.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "最大任务数量：";
            // 
            // FrmStrat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_TaskCount);
            this.Controls.Add(this.txt_Log);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_About);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.btn_shoudong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStrat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lorna图片爬虫V2.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStrat_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbd_url;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_About;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Button btn_shoudong;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.TextBox txt_TaskCount;
        private System.Windows.Forms.Label label1;
    }
}

