﻿/*
 * CLR Version：4.0.30319.42000
 * NameSpace：WIP_common.AOP
 * FileName：LogsAttribute
 * CurrentYear：2019
 * CurrentTime：2019/7/22 13:35:37
 * Author：shaocx
 *
 * <更新记录>
 * ver 1.0.0.0   2019/7/22 13:35:37 新規作成 (by shaocx)
 */

using KAOP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WIP_common;
using WIP_DAL;
using WIP_Models;

namespace WIP_BLL
{

    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class LogAopAttribute : KAopClassAttribute
    {
        private static RequestRecordDAL requestRecordDAL = new WIP_DAL.RequestRecordDAL();

        public LogAopAttribute(string namespaceName, string msgSource = WipSource.WIPREST)
        {
            this.namespaceName = namespaceName;
            this.msgSource = msgSource;
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        private string namespaceName { get; set; }

        /// <summary>
        /// 信息来源
        /// </summary>
        private string msgSource { get; set; }



        public override void PreExcute(string MethodName, IDictionary<string, object> InParams)
        {
            #region 异步处理
            Task.Run(() =>
            {
                RequestRecordEntity requestRecord = new RequestRecordEntity()
                {
                    direction = 1,//  1  WIP接收   2 WIP 推送
                    happenHost = WIPCommon.GetHostName(msgSource),
                    fullFun = namespaceName + "." + MethodName,//方法名
                    param = JsonConvert.SerializeObject(InParams), //入参
                };
                LogHelper.WriteInfoLogByLog4Net(typeof(LogHelper), JsonConvert.SerializeObject(requestRecord));
                requestRecordDAL.Add(requestRecord);
            });
            //*/
            #endregion
        }

        public override void EndExcute(string MethodName, IDictionary<string, object> InParams, object[] OutParams, object ReturnValue, Exception ex)
        {

        }


    }

    //*/
}
