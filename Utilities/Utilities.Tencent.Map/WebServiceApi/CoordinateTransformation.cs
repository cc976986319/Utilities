using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Tencent.Map.WebServiceApi
{
    /// <summary>
    /// 坐标转换
    /// </summary>
    public class CoordinateTransformation
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        public class RequestParameter
        {
            /// <summary>
            /// 预转换的坐标，支持批量转换，格式：纬度前，经度后，纬度和经度之间用","分隔，每组坐标之间使用";"分隔；批量支持坐标个数以HTTP GET方法请求上限为准
            /// </summary>
            public string Locations { get; protected set; }

            /// <summary>
            /// 输入的locations的坐标类型
            /// 可选值为[1, 6] 之间的整数，每个数字代表的类型说明： 1 GPS坐标 2 sogou经纬度 3 baidu经纬度 4 mapbar经纬度 5 [默认] 腾讯、google、高德坐标 6 sogou墨卡托
            /// </summary>
            public LocationType Type { get; set; }

            /// <summary>
            /// 开发密钥（Key）
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// 返回格式：支持JSON/JSONP，默认JSON
            /// </summary>
            public string Output { get; set; }

            /// <summary>
            /// JSONP方式回调函数
            /// </summary>
            public string CallBack { get; set; }

            /// <summary>
            /// 生成
            /// </summary>
            /// <param name="coords">坐标</param>
            public virtual void GenerateLocations(IEnumerable<Coordinate> coords)
            {
                Coordinate[] items = coords.ToArray();
                StringBuilder builder = new StringBuilder();
                if (items != null && items.Length > 0)
                {
                    int index = items.Length - 1;
                    for (int i = 0; i <= index; i++)
                    {
                        if (i == index)
                            builder.Append($"{items[i].lat},{items[i].lng}");
                        else
                            builder.Append($"{items[i].lat},{items[i].lng};");
                    }
                }
                this.Locations = builder.ToString();
            }

            /// <summary>
            /// 生成
            /// </summary>
            /// <param name="coords">坐标</param>
            public virtual void GenerateLocations(IEnumerable<string> sources)
            {
                string[] items = sources.ToArray();
                StringBuilder builder = new StringBuilder();
                if (items != null && items.Length > 0)
                {
                    int index = items.Length - 1;
                    for (int i = 0; i <= index; i++)
                    {
                        if (i == index)
                            builder.Append($"{items[i]}");
                        else
                            builder.Append($"{items[i]};");
                    }
                }
                this.Locations = builder.ToString();
            }

            /// <summary>
            /// 生成请求参数
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
#if DEBUG
                var data = $"?locations={this.Locations}&type={(int)this.Type}&key={this.Key}";
#endif
                return $"?locations={this.Locations}&type={(int)this.Type}&key={this.Key}";
            }
        }

        /// <summary>
        /// 请求结果
        /// </summary>
        public class RequestResult
        {
            /// <summary>
            /// 状态码
            /// </summary>
            public ResponseStatus status { get; set; }

            /// <summary>
            /// 对status的描述
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// 坐标转换结果，转换后的坐标顺序与输入顺序一致
            /// </summary>
            public Coordinate[] locations { get; set; }

            /// <summary>
            /// 转换
            /// </summary>
            /// <param name="jsonStr">json格式的字符串</param>
            /// <returns></returns>
            public static RequestResult Parse(string jsonStr)
            {
#if DEBUG
                RequestResult result = JsonConvert.DeserializeObject<RequestResult>(jsonStr);
                return result;
#endif
#if !DEBUG
                return JsonConvert.DeserializeObject<RequestResult>(jsonStr);
#endif
            }
        }

        /// <summary>
        /// 坐标类型
        /// </summary>
        public enum LocationType : int
        {
            /// <summary>
            /// GPS坐标
            /// </summary>
            GPS = 1,
            /// <summary>
            /// sogou经纬度
            /// </summary>
            Sogou = 2,
            /// <summary>
            /// baidu经纬度
            /// </summary>
            Baidu = 3,
            /// <summary>
            /// mapbar经纬度
            /// </summary>
            MapBar = 4,
            /// <summary>
            /// 腾讯、google、高德坐标
            /// </summary>
            WGS84 = 5,
            /// <summary>
            /// sogou墨卡托
            /// </summary>
            Sougo_Mercator = 6
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="parameter">url参数</param>
        /// <param name="url">访问地址，非参数部分</param>
        /// <returns></returns>
        public RequestResult Translate(RequestParameter parameter, string url = "http://apis.map.qq.com/ws/coord/v1/translate")
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            byte[] responseResult = webClient.DownloadData($"{url}{parameter.ToString()}");
            string result = Encoding.UTF8.GetString(responseResult);
            return JsonConvert.DeserializeObject<RequestResult>(result);
        }
    }
}
