using SuperAraneid.Helper;
//================================================================
// Copyright (c) 苏州LHY工作室. All rights reserved.
// 所属项目：SuperAraneid.Business
// 创 建 人：lorna
// 创建日期：2017/2/3 15:16:10
// 用    途：Crawler 爬虫
// 网    站：www.lorna.com.cn
// 版    本: 1.0.0.0
//================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSupervisor = SuperAraneid.Business.TaskSupervisor.TaskSupervisor;

namespace SuperAraneid.Business
{
    using System.Collections;
    using System.Threading;

    using SuperAraneid.Business.Behavior;

    /// <summary>
    /// Crawler 爬虫
    /// </summary>
    public class Crawler : Base
    {
        /// <summary>
        /// 分析器
        /// </summary>
        public TaskSupervisor.TaskSupervisor TaskSupervisor = new TaskSupervisor.TaskSupervisor();

        /// <summary>
        /// 功能总开关
        /// </summary>
        public bool Control { get; set; }

        /// <summary>
        /// 所有的图片队列
        /// </summary>
        public Queue ImagesQueue { get; set; }

        /// <summary>
        /// 空构造
        /// </summary>
        public Crawler()
        {
            ImagesQueue = new Queue();
        }



        /// <summary>
        /// 行动！！
        /// </summary>
        /// <param name="url">原始URL</param>
        /// <param name="maxTaskNum">最大任务数量</param>
        public void Action(string url, int maxTaskNum = 10)
        {
            Base.CancelTokenSource = new CancellationTokenSource();

            //// 初始化分析器
            AnalysisBehaviorBusiness analysisBehaviorBusiness = new AnalysisBehaviorBusiness(url, maxTaskNum, ImagesQueue);

            //// 初始化下载器
            CrawlerBehaviorBusiness crawlerBehaviorBusiness = new CrawlerBehaviorBusiness(maxTaskNum, ImagesQueue);

            TaskSupervisor.Add(Task.Factory.StartNew(() => crawlerBehaviorBusiness.Action(), Base.CancelTokenSource.Token));
            TaskSupervisor.Add(Task.Factory.StartNew(() => analysisBehaviorBusiness.Action(), Base.CancelTokenSource.Token));
        }



        /// <summary>
        /// 终止爬去
        /// </summary>
        public void End()
        {
            //// 通知关闭
            Base.CancelTokenSource.Cancel();

            TaskSupervisor.DisposeInvalidTask();
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static Crawler Instance
        {
            get
            {
                lock (ObjLock)
                {
                    if (instance == null)
                    {
                        instance = new Crawler();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        private static Crawler instance = null;

        /// <summary>
        /// 对象锁
        /// </summary>
        private static object ObjLock = new object();

    }
}
