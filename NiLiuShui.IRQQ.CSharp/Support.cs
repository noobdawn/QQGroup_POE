using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    static class Support
    {
        public static string RobotQQ = "2590863519";
        private static string[] QQ_Group = new string[] { "979136170", "96955256" };
        public static bool isGroupCorrect(string groupID)
        {
            return QQ_Group.Contains(groupID);
        }

        public static string CachePath = @"C:\CleverQQAir";

        public static string GetName(PersonData person)
        {
            if (person.NickName != "default")
                return IRQQApi.Api_GetGroupCard(RobotQQ, person.GroupQQ, person.QQ);
            return person.NickName;
        }

        public static void Response(string groupqq, string qq, ChineseID id, params object[] objs)
        {
            string content = string.Format(GetText(id),objs);
            IRQQApi.Api_SendMsg(RobotQQ, 2, groupqq, qq, content, -1);
        }
        public static string GetText(ChineseID id)
        {
            return ChineseText[(int)id];
        }

        public static string[] ChineseText = new string[]
        {
            "#仓库",
            "#查询",
            "[IR:at={0}]({0})拥有混沌石：{1}",
            "[IR:at={0}]({0})打到了一件不错的装备，卖出了{1}个混沌石的高价",
            "[IR:at={0}]({0})掉落了一个野生的崇高，获得了{1}个混沌石",
            "[IR:at={0}]({0})凑齐了所有疯医卡得到了猎首，获得了{1}个混沌石",
        };
    }

    enum ChineseID
    {
        Command_to_Query,
        Command_to_Query_Other,
        Response_to_Query,
        GoodLuck_0,
        GoodLuck_1,
        GoodLuck_2,
    }
}
