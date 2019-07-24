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
        private const string HELP_TEXT =
@"%查询：查询自身持有的财产
%查询 @目标：查询目标持有的财产
%统计：统计当前群内最富裕和最穷困的人
%侦测 @目标：查询目标的属性
%投资：查看崇高石市场价格
%买入 数量：购买一定数额的崇高石
%买入：直接购买能购买的最大数量崇高石
%卖出 数量：卖出持有的崇高石
%卖出：直接卖出手头所有的崇高石
%升级：查看可升级的属性
%升级 属性id：升级对应属性";

        public static SendParam MakeParam(string groupQQ, string qq, string content)
        {
            if (content == "%帮助")
            {
                _S.Response(groupQQ, qq, HELP_TEXT);
                return null;
            }
            //防止刷屏，滤除短长度的内容
            if (!content.StartsWith("%") && content.Length < 6)
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
