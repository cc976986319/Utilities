using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities.Extension.Microsoft
{
    /// <summary>
    /// Xml扩展
    /// </summary>
    public static class XmlExtension
    {
        /// <summary>
        /// 转换为Xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder ConvertXmlString<T>(this T value) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(builder, settings))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, value, namespaces);
            }
            return builder;
        }
    }
}
