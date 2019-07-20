using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 过滤器，用于分解并传递参数
    /// </summary>
    static class Filter
    {
        public static SendParam MakeParam(string groupQQ, string qq, string content)
        {
            if (!Support.isGroupCorrect(groupQQ))
                return null;
            SendParam param = new SendParam();
            param.GroupQQ = groupQQ;
            param.QQ = qq;
            param.content = content;
            param.param = content.Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return param;
        }
    }

    class SendParam
    {
        public string GroupQQ;
        public string QQ;
        public string content;
        public string[] param;
    }
}
