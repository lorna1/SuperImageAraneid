using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperAraneid.Helper;

namespace SuperAraneid
{
    using SuperAraneid.Business;

    /// <summary>
    /// Author:Lorna
    /// WebSite:www.lorna.com.cn
    /// </summary>
    public partial class FrmStrat : Form
    {
        public FrmStrat()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            //// 设置释放按钮默认失效
            button1.Enabled = false;
            //// 控件允许多线程访问
            CheckForIllegalCrossThreadCalls = false;
            //// 同步实时进度时间
            Base.Progress.ProgressChanged += ProgressEvent;
        }

        /// <summary>
        /// 窗口释放
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void FrmStrat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Crawler.Instance.End();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btn_About_Click(object sender, EventArgs e)
        {
            FromAbout about = new FromAbout();
            about.Show();
        }

        /// <summary>
        /// 开始爬取
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btn_shoudong_Click_1(object sender, EventArgs e)
        {
           
            //// 设置保存路径
            SetSaveFloder();
            //// 启动任务
            Crawler.Instance.Action(txt_url.Text, Convert.ToInt32(txt_TaskCount.Text));

            //// 按钮状态
            btn_shoudong.Enabled = false;
            button1.Enabled = true;
            System.Diagnostics.Process.Start("http://www.lorna.com.cn");
        }

        /// <summary>
        /// 暂停释放
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            //// 暂停任务
            Crawler.Instance.End();

            btn_shoudong.Enabled = true;
            button1.Enabled = false;

            ProgressEvent(null, "--------------------------------------[任务关闭]--------------------------------------");
        }

        /// <summary>
        /// 设置保存的文件夹
        /// </summary>
        private void SetSaveFloder()
        {
            Global.WebUrl = txt_url.Text;
            fbd_url.ShowDialog();
            string path = fbd_url.SelectedPath;
            if (!string.IsNullOrEmpty(path))
            {
                Global.FloderUrl = fbd_url.SelectedPath + "\\";
            }
            else
            {
                Global.FloderUrl = Application.StartupPath + Global.FloderMoUrl;
            }

        }

        /// <summary>
        /// 进度通知
        /// </summary>
        /// <param name="text">文本</param>
        private void ProgressEvent(object obj, string text)
        {
            txt_Log.AppendText(text);
            txt_Log.ScrollToCaret();
        }

    }

}
