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
                if (data.ChaosCount < 20)
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
            return true;
        }
    }
}
