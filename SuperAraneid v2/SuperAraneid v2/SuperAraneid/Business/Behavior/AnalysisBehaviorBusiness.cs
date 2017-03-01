//================================================================
// Lorna的个人博客 http://www.lorna.com.cn
// 所属项目：SuperAraneid.Business.Behavior
// 创 建 人：Lorna
// 创建日期：2017/2/23 13:51:38
// 用    途：AnalysisBehaviorBusiness 分析行为逻辑
//================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAraneid.Business.Behavior
{
    using System.Collections;
    using System.Threading;

    using SuperAraneid.Business.TaskSupervisor;

    /// <summary>
    /// AnalysisBehaviorBusiness 分析行为逻辑
    /// 分析url中的所有url
    /// 分析url存在的链接
    /// </summary>
    public class AnalysisBehaviorBusiness : BehaviorTemplate
    {

        /// <summary>
        /// 等待分析的地址
        /// </summary>
        private Queue WaitUrlQueue = new Queue();

        /// <summary>
        /// 待下载的图片队列
        /// </summary>
        private Queue WaitImageUrlQueue = new Queue();

        /// <summary>
        /// 已经便利过的数据
        /// </summary>
        private List<string> ExistUrlList { get; set; }

        /// <summary>
        /// 任务管理器
        /// </summary>
        private TaskSupervisor TaskSupervisor { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        private AnalysisBehaviorBusiness() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">初始的URL</param>
        /// <param name="maxTaskNum">最大任务数</param>
        /// <param name="imageQueue">图片队列</param>
        public AnalysisBehaviorBusiness(string url, int maxTaskNum, Queue imageQueue)
        {
            //// 添加第一个URL
            WaitUrlQueue.Enqueue(url);
            //// 初始化任务管理器
            TaskSupervisor = new TaskSupervisor(maxTaskNum);
            WaitImageUrlQueue = imageQueue;
            ExistUrlList = new List<string>();
        }

        /// <summary>
        /// 行动
        /// </summary>
        public override async Task Action()
        {
            while (!Base.CancelTokenSource.IsCancellationRequested)
            {
                TaskSupervisor.DisposeInvalidTask();
                if (WaitUrlQueue.Count < 1 || WaitImageUrlQueue.Count > 500)
                {
                    base.Progress("[分析器休眠]待分析URL:{0},图片待下载:{1},暂时不需要分析。", WaitUrlQueue.Count, WaitImageUrlQueue.Count);
                    await Task.Delay(2000);
                    continue;
                }
                string url = WaitUrlQueue.Dequeue().ToString();
                if (!ExistUrlList.Contains(url))
                {
                    Task task = new Task(delegate { Analysis.AnalysisUrlList(url, PushWaitUrlList, PushWaitImagesList); }, Base.CancelTokenSource.Token);
                    bool result = TaskSupervisor.Add(task);
                    if (!result)
                    {
                        base.Progress("[分析器休眠]分析队列已满,等待...", WaitImageUrlQueue.Count);
                        await Task.Delay(2000);

                        WaitUrlQueue.Enqueue(url);
                        continue;
                    }
                    ExistUrlList.Add(url);
                }
            }

            TaskSupervisor.DisposeInvalidTask();
        }

        /// <summary>
        /// 追加url至列表中
        /// </summary>
        /// <param name="list">列表</param>
        protected void PushWaitUrlList(List<string> list)
        {
            foreach (string url in list)
            {
                WaitUrlQueue.Enqueue(url);
            }
        }

        /// <summary>
        /// 追加待下载的图片至数据集中
        /// </summary>
        /// <param name="list">待下载的图片</param>
        protected void PushWaitImagesList(List<string> list)
        {
            foreach (string url in list)
            {
                WaitImageUrlQueue.Enqueue(url);
            }
        }
    }
}
