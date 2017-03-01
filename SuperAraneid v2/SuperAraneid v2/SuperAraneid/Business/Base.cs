//================================================================
// Copyright (c) 苏州LHY工作室. All rights reserved.
// 所属项目：SuperAraneid.Business
// 创 建 人：lorna
// 创建日期：2017/2/3 15:17:20
// 用    途：Base 基础行为
// 网    站：www.lorna.com.cn
// 版    本: 1.0.0.0
//================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAraneid.Business
{
    using System.Threading;

    /// <summary>
    /// Base 基础行为
    /// </summary>
    public class Base
    {
        /// <summary>
        /// cancelToken
        /// </summary>
        public static CancellationTokenSource CancelTokenSource = new CancellationTokenSource();

        /// <summary>
        /// 进度通知
        /// </summary>
        public static Progress<string> Progress = new Progress<string>();

    }
}
