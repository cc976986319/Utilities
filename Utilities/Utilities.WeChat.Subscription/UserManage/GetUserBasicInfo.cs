﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Subscription.ENUM;

namespace Utilities.WeChat.Subscription.UserManage
{
    /// <summary>
    /// 获取用户基本信息
    /// </summary>
    public class GetUserBasicInfo
    {
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public UserBasicInfo GetSingle(SingleRequestParameter parameter)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] result = client.DownloadData($"https://api.weixin.qq.com/cgi-bin/user/info?access_token={parameter.access_token}&openid={parameter.openid}&lang={Enum.GetName(typeof(Language), parameter.lang)}");
                return RequestResult.Parse<UserBasicInfo>(result);
            }
        }

        /// <summary>
        /// 批量获取
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public BatchUserBasicInfo GetBatch(string access_token, List<BaseRequestParameter> parameters)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string requestData = JsonConvert.SerializeObject(parameters);
                byte[] result = client.UploadData($"https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={access_token}", "POST", Encoding.UTF8.GetBytes(requestData));
                return RequestResult.Parse<BatchUserBasicInfo>(result);
            }
        }

        /// <summary>
        /// 单条用户信息
        /// </summary>
        public class UserBasicInfo : RequestResult
        {
            /// <summary>
            /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
            /// </summary>
            public int subscribe { get; set; }

            /// <summary>
            /// 用户的标识，对当前公众号唯一
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 用户的昵称
            /// </summary>
            public string nickname { get; set; }

            /// <summary>
            /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
            /// </summary>
            public int sex { get; set; }

            /// <summary>
            /// 用户所在城市
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 用户所在国家
            /// </summary>
            public string country { get; set; }

            /// <summary>
            /// 用户所在省份
            /// </summary>
            public string province { get; set; }

            /// <summary>
            /// 用户的语言，简体中文为zh_CN
            /// </summary>
            public string language { get; set; }

            /// <summary>
            /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
            /// </summary>
            public string headimgurl { get; set; }

            /// <summary>
            /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
            /// </summary>
            public long subscribe_time { get; set; }

            /// <summary>
            /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
            /// </summary>
            public string unionid { get; set; }

            /// <summary>
            /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 用户所在的分组ID（兼容旧的用户分组接口）
            /// </summary>
            public int groupid { get; set; }

            /// <summary>
            /// 用户被打上的标签ID列表
            /// </summary>
            public int[] tagid_list { get; set; }
        }

        /// <summary>
        /// 用户信息列表组
        /// </summary>
        public class BatchUserBasicInfo : RequestResult
        {
            /// <summary>
            /// 用户信息列表
            /// </summary>
            public UserBasicInfo[] user_info_list { get; set; }
        }
        
        /// <summary>
        /// 基础参数
        /// </summary>
        public class BaseRequestParameter
        {
            /// <summary>
            /// 用户的标识，对当前公众号唯一
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN
            /// </summary>
            public Language lang { get; set; }
        }

        /// <summary>
        /// 单一参数
        /// </summary>
        public class SingleRequestParameter : BaseRequestParameter
        {
            /// <summary>
            /// 调用接口凭证
            /// </summary>
            public string access_token { get; set; }
        }
    }
}
