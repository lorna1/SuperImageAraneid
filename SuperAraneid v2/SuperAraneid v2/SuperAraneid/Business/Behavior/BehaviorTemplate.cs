//================================================================
// Lorna的个人博客 http://www.lorna.com.cn
// 所属项目：SuperAraneid.Business.Behavior
// 创 建 人：Lorna
// 创建日期：2017/2/23 13:53:36
// 用    途：行为模板
//================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAraneid.Business.Behavior
{
    /// <summary>
    /// BehaviorTemplate 行为模板
    /// </summary>
    public abstract class BehaviorTemplate
    {
        /// <summary>
        /// 总开关
        /// </summary>
        public bool Control { get; set; }

        /// <summary>
        /// 开始行动
        /// </summary>
        public abstract Task Action();

        /// <summary>
        /// 进度同步
        /// </summary>
        /// <param name="message">同步消息</param>
        protected void Progress(string format, params object[] args)
        {
            if (Base.Progress != null)
            {
                IProgress<string> progress = Base.Progress;
                string message = string.Format("[{0}]{1}", DateTime.Now.ToString("HH:mm:ss"), string.Format(format, args));
                if (message.Length > 130)
                {
                    message = message.Substring(0, 130);
                }
                progress.Report(message + "\r\n");
            }
        }
    }
}
