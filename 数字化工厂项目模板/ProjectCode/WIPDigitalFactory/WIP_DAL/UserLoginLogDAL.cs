﻿/*
 * CLR Version：4.0.30319.42000
 * NameSpace：WIP_SQLServerDAL
 * FileName：UserLoginLogDAL
 * CurrentYear：2019
 * CurrentTime：2019/7/24 11:30:56
 * Author：shaocx
 *
 * <更新记录>
 * ver 1.0.0.0   2019/7/24 11:30:56 新規作成 (by shaocx)
 */


using SysManager.DB.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIP_IDAL;
using WIP_Models;

namespace WIP_SQLServerDAL
{
    /// <summary>
    /// 用户登录记录数据访问类
    /// </summary>
    public class UserLoginLogDAL : IUserLoginLogDAL
    {
        /// <summary>
        /// 查询用户登录记录列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户登录记录列表</returns>
        public List<UserLoginLogEntity> GetUserLoginLogList(string userName)
        {
            //使用ORM框架的Dapper实现
            var param = new
            {
                userName = userName
            };
            return new SQLServerDapperHelper().QueryListByProc<object, UserLoginLogEntity>("uspWip_GetUserLoginLogList", param);
        }
    }
}
