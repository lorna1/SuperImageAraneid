//================================================================
// Lorna的个人博客 http://www.lorna.com.cn
// 所属项目：SuperAraneid.Business.TaskSupervisor
// 创 建 人：Lorna
// 创建日期：2017/2/23 14:32:18
// 用    途：TaskSupervisorBusiness 任务管理器
//================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAraneid.Business.TaskSupervisor
{
    /// <summary>
    /// TaskSupervisorBusiness 任务管理器
    /// </summary>
    public class TaskSupervisor
    {

        /// <summary>
        /// 构造
        /// </summary>
        public TaskSupervisor()
        {
            MaxTaskNum = 10;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxTaskNum">最大线程数</param>
        public TaskSupervisor(int maxTaskNum)
        {
            MaxTaskNum = maxTaskNum;
        }

        /// <summary>
        /// 最大线程数量
        /// </summary>
        public int MaxTaskNum { get; set; }

        /// <summary>
        /// 存放航班信息;
        /// </summary>
        private List<Task> TaskList = new List<Task>();

        /// <summary>
        /// 释放所有无效任务
        /// </summary>
        public void DisposeInvalidTask()
        {
            List<Task> removeTask = new List<Task>();
            foreach (Task task in TaskList)
            {
                if (task.IsCompleted || task.IsCanceled || task.IsFaulted)
                {
                    task.Dispose();
                    removeTask.Add(task);
                }
            }

            foreach (Task task in removeTask)
            {
                TaskList.Remove(task);
            }
        }

        /// <summary>
        /// 释放线程
        /// </summary>
        public void DisposeTask()
        {
            foreach (Task task in TaskList)
            {
                task.Dispose();
            }

        }

        /// <summary>
        /// 新增一个任务
        /// </summary>
        /// <param name="task">任务</param>
        public bool Add(Task task)
        {
            if (TaskList.Count < MaxTaskNum)
            {
                if (task.Status == TaskStatus.Created)
                {
                    TaskList.Add(task);
                    task.Start();
                    return true;
                }
            }

            DisposeInvalidTask();
            return false;
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns>数量</returns>
        public int Count()
        {
            return TaskList.Count;
        }

        /// <summary>
        /// 开始全部
        /// </summary>
        public void StartAll()
        {
            foreach (Task task in TaskList)
            {
                task.Start();
            }
        }
    }
}
