using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Utilities.WeChatEnterprise.ResponseResults.CallBack
{
    public class xml
    {
        public xml() { }

        public xml(string agentID, string corpID, string encrypt)
        {
            this.ToUserName = $"<![CDATA[{corpID}]]>";
            this.AgentID = $"<![CDATA[{agentID}]]>";
            this.Encrypt = $"<![CDATA[{encrypt}]]>";
        }

        public string ToUserName { get; set; }

        public string AgentID { get; set; }

        public string Encrypt { get; set; }
    }
}
