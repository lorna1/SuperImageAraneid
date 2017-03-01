//================================================================
// Lorna的个人博客 http://www.lorna.com.cn
// 所属项目：SuperAraneid.Business.Behavior
// 创 建 人：Lorna
// 创建日期：2017/2/23 13:50:54
// 用    途：CrawlerBehaviorBusiness 爬虫行为逻辑
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
    using SuperAraneid.Helper;

    /// <summary>
    /// CrawlerBehaviorBusiness 爬虫行为逻辑
    /// </summary>
    public class CrawlerBehaviorBusiness : BehaviorTemplate
    {
        /// <summary>
        /// 待下载的图片列表
        /// </summary>
        private Queue WaitImageUrlQueue = new Queue();

        /// <summary>
        /// 已存在的图片列表
        /// </summary>
        private List<string> ExistList = new List<string>();

        /// <summary>
        /// 任务管理器
        /// </summary>
        private TaskSupervisor TaskSupervisor { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        private CrawlerBehaviorBusiness() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxTaskNum">最大任务数</param>
        /// <param name="imageList">待下载</param>
        public CrawlerBehaviorBusiness(int maxTaskNum, Queue imageList)
        {
            WaitImageUrlQueue = imageList;
            TaskSupervisor = new TaskSupervisor(maxTaskNum);
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        public override async Task Action()
        {
            try
            {
                DownloadHelper downloadHelper = new DownloadHelper();
                while (!Base.CancelTokenSource.IsCancellationRequested)
                {
                    TaskSupervisor.DisposeInvalidTask();

                    if (WaitImageUrlQueue.Count < 1)
                    {
                        base.Progress("[下载器休眠]图片队列下载完毕,等待分析器...", WaitImageUrlQueue.Count);
                        await Task.Delay(2000);
                        continue;
                    }

                    string imageUrl = Dequeue();
                    if (!ExistList.Contains(imageUrl))
                    {
                        base.Progress("[下载器任务]URL:{0}", imageUrl);
                        Task task = new Task(delegate { downloadHelper.DownLoadImage(imageUrl); }, Base.CancelTokenSource.Token);
                        bool result = TaskSupervisor.Add(task);
                        if (!result)
                        {
                            base.Progress("[下载器休眠]下载队列已满,等待...", WaitImageUrlQueue.Count);
                            await Task.Delay(2000);
                            WaitImageUrlQueue.Enqueue(imageUrl);
                            continue;
                        }
                        ExistList.Add(imageUrl);
                    }
                }

                TaskSupervisor.DisposeInvalidTask();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取下一个目标
        /// </summary>
        /// <returns>图片地址</returns>
        private string Dequeue()
        {
            string imageUrl = WaitImageUrlQueue.Dequeue().ToString();
            return imageUrl;
        }


    }
}
