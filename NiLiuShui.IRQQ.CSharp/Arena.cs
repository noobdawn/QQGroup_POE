using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 战斗相关的类
    /// </summary>
    class Arena : IModule
    {
        private Random random;
        public Arena()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public bool WhenParamIn(SendParam param)
        {
            PersonData data;
            if (!DataRunTime.TryGetPerson(param.GroupQQ, param.QQ, out data)) return false;
            //命令
            if (param.param[0] == Support.GetText("Command_to_ChaosStrike") && param.param.Length == 2)
            {
                if (param.param[1].Length < 10)
                    return true;
                string otherqq = param.param[1].Substring(7);
                otherqq = otherqq.Substring(0, otherqq.Length - 1);
                long t;
                if (!long.TryParse(otherqq, out t)) return true;
                PersonData other = DataRunTime.GetPerson_Add(param.GroupQQ, otherqq);
                int cost = 20;
                if (data.ChaosCount < cost)
                    return true;
                DataRunTime.ChaosChange(data.GroupQQ, data.QQ, -cost);
                if (data.QQ == other.QQ)
                {
                    Support.Response(param.GroupQQ, param.QQ, "Response_to_ChaosStrike_Myself", data.QQ, cost);
                    return true;
                }
                int p = random.Next(1, 30);
                double realLost = DataRunTime.ChaosChange(other.GroupQQ, other.QQ, -p);
                DataRunTime.ChaosChange(data.GroupQQ, data.QQ, -realLost);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_ChaosStrike", data.QQ, other.QQ, cost, -realLost);
            }
            if (param.param[0] == Support.GetText("Command_to_Syndicate") && param.param.Length == 1)
            {
                int cost = 100;
                if (data.ChaosCount < cost)
                    return true;
                DataRunTime.ChaosChange(data.GroupQQ, data.QQ, -cost);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate", data.QQ, cost);
                var result = DataRunTime.GetTop(param.GroupQQ);
                int allLost = 150;
                int[] losts = new int[Math.Min(3, result.Count)];
                for (int i = 0; i < losts.Length - 1; i++)
                {
                    losts[i] = random.Next(0, allLost);
                    allLost -= losts[i];
                }
                losts[losts.Length - 1] = allLost;
                for (int i = 0; i < losts.Length; i++)
                {
                    PersonData p = result[i];
                    DataRunTime.ChaosChange(p.GroupQQ, p.QQ, -losts[i]);
                    Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate_Lost", p.QQ, losts[i]);
                }
            }
            return true;
        }
    }
}
