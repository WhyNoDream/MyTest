using Exceptionless;
using ExceptionlessUnit.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionlessUnit.Instances
{
    public class SubmittingEvent: ISubmittingEvent
    {
        public  void OnSubmittingEvent(object sender, EventSubmittingEventArgs e)
        {
            // 只处理未处理的异常
            //if (!e.IsUnhandledError)
            //{
            //    return;
            //}

            // 忽略404错误
            if (e.Event.IsNotFound())
            {
                e.Cancel = true;
                return;
            }

            // 忽略没有错误体的错误
            var error = e.Event.GetError();
            if (error == null)
            {
                return;
            }

            // 忽略 401 (Unauthorized) 和 请求验证的错误.
            if (error.Code == "401" || error.Type == "System.Web.HttpRequestValidationException")
            {
                e.Cancel = true;
                return;
            }

            // Ignore any exceptions that were not thrown by our code.
            //var handledNamespaces = new List<string> { "Exceptionless" };
            //if (!error.StackTrace.Select(s => s.DeclaringNamespace).Distinct().Any(ns => handledNamespaces.Any(ns.Contains)))
            //{
            //    e.Cancel = true;
            //    return;
            //}

            //添加附加信息.
            e.Event.Tags.Add("");
            e.Event.MarkAsCritical();
        }
    }
}
