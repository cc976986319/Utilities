using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Utilities.MVC
{
    /// <summary>
    /// Jsonp返回结果
    /// </summary>
    public class JsonpResult : JsonResult
    {
        // 标识类型
        private static readonly string JsonpCallbackName = "callback";
        // 返回类型定义
        private static readonly string CallbackApplicationType = "application/json";

        // 数据存储
        object data = null;

        /// <summary>
        /// 实例化
        /// </summary>
        public JsonpResult() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="data"></param>
        public JsonpResult(object data)
        {
            this.data = data;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context">控制器实例</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                HttpResponseBase Response = context.HttpContext.Response;

                string callbackfunction = context.HttpContext.Request["callback"];

                if (string.IsNullOrEmpty(callbackfunction))
                {
                    throw new Exception("Callback function name must be provided in the request!");
                }
                Response.ContentType = "application/x-javascript";
                if (data != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(data)));
                }
            }
        }
    }

    /// <summary>
    /// 控制器扩展
    /// </summary>
    public static class ContollerExtensions
    {
        /// <summary>
        /// 扩展Jsonp函数
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static JsonpResult Jsonp(this Controller controller, object data)
        {
            JsonpResult result = new JsonpResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }
    }
}
