using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.Contacts
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Tags
    {
        /// <summary>
        /// 创建标签
        /// </summary>
        /// <returns></returns>
        public ResponseResult CreateTags(string accessToken, RequestBody_Tags body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/create")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<Result>();
            }
        }

        /// <summary>
        /// 修改标签名字
        /// </summary>
        /// <returns></returns>
        public ResponseResult UpdateTagsName(string accessToken, RequestBody_Tags body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/update")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <returns></returns>
        public ResponseResult DeleteTags(string accessToken, int tagid, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&tagid={tagid}");
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取标签成员
        /// </summary>
        /// <returns></returns>
        public ResponseResult FindTagsMember(string accessToken, int tagid, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/get")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&tagid={tagid}");
                return bytes.ConvertTo<Result>();
            }
        }

        /// <summary>
        /// 增加标签成员
        /// </summary>
        /// <returns></returns>
        public ResponseResult CreateTagsMember(string accessToken, RequestBody_Member body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <returns></returns>
        public ResponseResult DeleteTagsMember(string accessToken, RequestBody_Member body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult FindTagsCollection(string accessToken, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/tag/list")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}");
                return bytes.ConvertTo<Result>();
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public class Result : ResponseResult
        {
            /// <summary>
            /// 标签id，整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增。
            /// </summary>
            public int tagid { get; set; }

            /// <summary>
            /// 标签列表
            /// </summary>
            public List<Tag> taglist { get; set; }

            /// <summary>
            /// 部门列表
            /// </summary>
            public List<int> partylist { get; set; }

            /// <summary>
            /// 无效部门
            /// </summary>
            public List<int> invalidparty { get; set; }

            /// <summary>
            /// 无效列表
            /// </summary>
            public string invalidlist { get; set; }

            /// <summary>
            /// 标签
            /// </summary>
            public class Tag
            {
                /// <summary>
                /// 标签编号
                /// </summary>
                public int tagid { get; set; }

                /// <summary>
                /// 标签名称
                /// </summary>
                public string tagname { get; set; }
            }
        }

        /// <summary>
        /// 请求体(标签)
        /// </summary>
        public class RequestBody_Tags
        {
            /// <summary>
            /// 标签名称，长度限制为32个字（汉字或英文字母），标签名不可与其他标签重名。
            /// </summary>
            public string tagname { get; set; }

            /// <summary>
            /// 标签id，整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增
            /// </summary>
            public int tagid { get; set; }

            /// <summary>
            /// 转换为字节对象
            /// </summary>
            /// <returns></returns>
            public virtual byte[] ToBytes()
            {
                string json = new JavaScriptSerializer().Serialize(this);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        /// <summary>
        /// 请求体(成员)
        /// </summary>
        public class RequestBody_Member
        {
            /// <summary>
            /// 标签ID
            /// </summary>
            public int tagid { get; set; }

            /// <summary>
            /// 企业成员ID列表，注意：userlist、partylist不能同时为空
            /// </summary>
            public List<string> userlist { get; set; }

            /// <summary>
            /// 企业部门ID列表，注意：userlist、partylist不能同时为空
            /// </summary>
            public List<int> partylist { get; set; }
        }
    }
}
