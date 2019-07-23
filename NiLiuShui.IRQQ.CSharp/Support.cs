using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    static class _S
    {
        #region 随机数
        private static Random random;
        public static int RandomInt(int min, int max)
        {
            if (random == null)
                random = new Random(DateTime.Now.Millisecond);
            return random.Next(min, max);
        }
        #endregion

        #region 信息处理
        public static string GetName(PersonData person)
        {
            if (person.NickName != "default")
                return IRQQApi.Api_GetGroupCard(Config.RobotQQ, person.GroupQQ, person.QQ);
            return person.NickName;
        }
        #endregion

        #region 返回
        public static void Response(string groupqq, string qq, string text)
        {
            IRQQApi.Api_SendMsg(Config.RobotQQ, 2, groupqq, qq, text, -1);
        }
        public static void Broadcast(string text)
        {
            foreach (var kv in DataRunTime.persons)
            {
                IRQQApi.Api_SendMsg(Config.RobotQQ, 2, kv.Key, "", text, -1);
            }
        }
        public static void Response(string groupqq, string qq, string id, params object[] objs)
        {
            string content = string.Format(GetText(id),objs);
            IRQQApi.Api_SendMsg(Config.RobotQQ, 2, groupqq, qq, content, -1);
        }
        public static void Broadcast(string id, params object[] objs)
        {
            string content = string.Format(GetText(id), objs);
            foreach (var kv in DataRunTime.persons)
            {
                IRQQApi.Api_SendMsg(Config.RobotQQ, 2, kv.Key, "", content, -1);
            }
        }
        #endregion

        #region Localization
        public static Dictionary<string, string> localization;
        public static void InitLocalization()
        {
            string path = Config.CachePath + "\\Localization.txt";
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

        #region Skill
        public static bool Cost(PersonData caster, double cost)
        {
            if (caster.ChaosCount < cost)
                return false;
            DataRunTime.ChaosChange(caster.GroupQQ, caster.QQ, -cost);
            return true;
        }

        public static double[] Damage(PersonData caster, PersonData target, double dmg, bool allowHealCaster, bool hurtSelf = false)
        {
            double[] result = new double[2];
            if (caster.GroupQQ != target.GroupQQ) return result;
            if (!hurtSelf && caster.QQ == target.QQ) return result;
            double realLost = DataRunTime.ChaosChange(target.GroupQQ, target.QQ, -dmg);
            int recover = (int)(Math.Abs(realLost) * _S.GetPropertyValue((int)EnumProperty.HealSelf, (int)(caster.Properties[(int)EnumProperty.HealSelf])));
            DataRunTime.ChaosChange(caster.GroupQQ, caster.QQ, recover);
            result[0] = realLost;
            result[1] = recover;
            return result;
        }
        #endregion

        #region Property
        class PropertyTable
        {
            public int[] Grade;
            public string Templet;
            public int[] Cost;
            public int[] Value;
            public int usePercent;
        }
        public const int MAX_PROPERTY_LEVEL = 8;
        private static PropertyTable[] tables;
        private static void InitTable()
        {
            tables = new PropertyTable[(int)EnumProperty.Max];
            var temp = new PropertyTable();
            temp.Templet = "+{0}%";
            temp.Cost = new int[MAX_PROPERTY_LEVEL] { 0, 5, 20, 100, 600, 4000, 20000, 100000 };
            temp.Value = new int[MAX_PROPERTY_LEVEL] { 0, 1, 2, 4, 8, 16, 32, 64 };
            temp.usePercent = 100;
            tables[(int)EnumProperty.AfkCount] = temp;
            temp = new PropertyTable();
            temp.Templet = "+{0}%";
            temp.Cost = new int[MAX_PROPERTY_LEVEL] { 0, 5, 20, 100, 600, 4000, 20000, 100000 };
            temp.Value = new int[MAX_PROPERTY_LEVEL] { 0, 5, 10, 15, 20, 25, 30, 35 };
            temp.usePercent = 100;
            tables[(int)EnumProperty.HealSelf] = temp;

        }

        public static string GetPropertyText(int property, int grade)
        {
            if (tables == null)
                InitTable();
            if (tables[property] == null)
                return grade.ToString();
            var table = tables[property];
            return string.Format(table.Templet, table.Value[grade] * table.usePercent);
        }
        public static double GetPropertyValue(int property, int grade)
        {
            if (tables == null)
                InitTable();
            if (tables[property] == null)
                return 0;
            var table = tables[property];
            return table.Value[grade];
        }

        public static int GetPropertyCost(int property, int grade)
        {
            if (tables == null)
                InitTable();
            if (tables[property] == null)
                return 0;
            var table = tables[property];
            return table.Cost[grade];
        }
        
        #endregion
    }
}
