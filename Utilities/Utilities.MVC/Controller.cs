using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Utilities.MVC
{
    /// <summary>
    /// 控制器，继承于MVC本身Controller
    /// </summary>
    public class Controller : System.Web.Mvc.Controller
    {
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType"> 内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <returns></returns>
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        /// <summary>
        /// 创建 JsonResult 对象，该对象使用指定 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="behavior"> JSON 请求行为。</param>
        /// <returns></returns>
        public new System.Web.Mvc.JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return new JsonResult { Data = data, JsonRequestBehavior = behavior };
        }

        /// <summary>
        ///  创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <returns></returns>
        public new System.Web.Mvc.JsonResult Json(object data)
        {
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        ///  创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="dateFormatString">时间格式字符串</param>
        /// <returns></returns>
        public new System.Web.Mvc.JsonResult Json(object data, string dateFormatString)
        {
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet, DateFormatString = dateFormatString };
        }
    }
}
