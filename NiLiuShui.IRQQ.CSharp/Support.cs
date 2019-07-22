using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    static class Support
    {
        public static string God = "694109410";
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
        public static void Response(string groupqq, string qq, string text)
        {
            IRQQApi.Api_SendMsg(RobotQQ, 2, groupqq, qq, text, -1);
        }
        public static void Response(string groupqq, string qq, string id, params object[] objs)
        {
            string content = string.Format(GetText(id),objs);
            IRQQApi.Api_SendMsg(RobotQQ, 2, groupqq, qq, content, -1);
        }

        #region Localization
        public static Dictionary<string, string> localization;
        public static void InitLocalization()
        {
            string path = CachePath + "\\Localization.txt";
            localization = new Dictionary<string, string>();
            if (!File.Exists(path)) return;
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var kv = line.Split(new string[]{ "|-zxc-|" }, StringSplitOptions.RemoveEmptyEntries);
                localization.Add(kv[0].Trim(), kv[1].Trim());
            }
            foreach (var kv in localization)
            {

                IRQQApi.Api_OutPutLog(kv.Key + " " + kv.Value);
            }
            IRQQApi.Api_OutPutLog("加载文本" + lines.Length.ToString() + "条");
        }

        public static string GetText(string id)
        {
            if (localization.ContainsKey(id))
                return localization[id];
            return "";
        }

        public static string GetText(string id, params object[] objs)
        {
            return string.Format(GetText(id), objs);
        }
        #endregion
    }
}
