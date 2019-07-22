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
            param.caster = DataRunTime.GetPerson_Add(groupQQ, qq);
            param.target = null;
            if (param.param.Length > 1)
            {
                if (param.param[1].Length >= 10)
                {
                    string otherqq = param.param[1].Substring(7);
                    otherqq = otherqq.Substring(0, otherqq.Length - 1);
                    long t;
                    if (long.TryParse(otherqq, out t))
                    {
                        param.target = DataRunTime.GetPerson_Add(param.GroupQQ, otherqq);
                    }
                }
            }
            return param;
        }
    }

    class SendParam
    {
        public string GroupQQ;
        public string QQ;
        public string content;
        public string[] param;
        public PersonData caster;
        public PersonData target;
    }
}
