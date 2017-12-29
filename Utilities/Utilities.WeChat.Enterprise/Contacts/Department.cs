using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.Contacts
{
    /// <summary>
    /// 管理部门
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="requestBody">请求参数(部门信息)</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public Result Create(string accessToken, RequestBody requestBody, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/create")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", requestBody.ToBytes());
                return bytes.ConvertTo<Result>();
            }
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="requestBody">请求参数(部门信息)</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public ResponseResult Update(string accessToken, RequestBody requestBody, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/update")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", requestBody.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门编号</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public ResponseResult Delete(string accessToken, int id, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/delete")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&id={id}");
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门编号</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public Result_List List(string accessToken, int id, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/list")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&id={id}");
                return bytes.ConvertTo<Result_List>();
            }
        }

        /// <summary>
        /// 请求实体
        /// </summary>
        public class RequestBody
        {
            /// <summary>
            /// 部门名称。长度限制为32个字（汉字或英文字母），字符不能包括\:*?"<>｜
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 父亲部门id。根部门id为1
            /// </summary>
            public int parentid { get; set; }

            /// <summary>
            /// 在父部门中的次序值。order值小的排序靠前。
            /// </summary>
            public int order { get; set; }

            /// <summary>
            /// 部门id，整型。指定时必须大于1，不指定时则自动生成
            /// </summary>
            public int id { get; set; }
        }

        /// <summary>
        /// 请求结果
        /// </summary>
        public class Result : ResponseResult
        {
            /// <summary>
            /// 操作的部门id
            /// </summary>
            public int id { get; set; }
        }

        /// <summary>
        /// 获取部门列表结果
        /// </summary>
        public class Result_List : ResponseResult
        {
            /// <summary>
            /// 部门列表数据。以部门的order字段从小到大排列
            /// </summary>
            public List<RequestBody> department { get; set; }
        }
    }
}
