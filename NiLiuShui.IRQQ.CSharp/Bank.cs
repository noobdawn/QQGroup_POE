using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 银行，主要控制金币
    /// </summary>
    class Bank : IModule
    {
        private Random random;
        public Bank()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public void WhenParamIn(SendParam param)
        {
            //低保
            DataRunTime.ChaosChange(param.GroupQQ, param.QQ, 1);
            PersonData data;
            if (!DataRunTime.TryGetPerson(param.GroupQQ, param.QQ, out data)) return;
            //随机
            int rand = random.Next(0, 10000);
            int rdCoin = 0;
            if (rand <= 1)
            {
                rdCoin = random.Next(1000, 3000);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                Support.Response(param.GroupQQ, param.QQ, "GoodLuck_2", data.QQ, rdCoin);
            }
            else if (rand <= 25)
            {
                rdCoin = random.Next(50, 150);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, 1);
                Support.Response(param.GroupQQ, param.QQ, "GoodLuck_1", data.QQ, rdCoin);
            }
            else if (rand <= 100)
            {
                rdCoin = random.Next(20, 50);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, 1);
                Support.Response(param.GroupQQ, param.QQ, "GoodLuck_0", data.QQ, rdCoin);
            }

            //命令
            if (param.param[0] == Support.GetText("Command_to_Query"))
            {
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Query", data.QQ, data.ChaosCount);
            }
            if (param.param[0] == Support.GetText("Command_to_Query_Other") && param.param.Length == 2)
            {
                if (param.param[1].Length < 10)
                    return;
                string otherqq = param.param[1].Substring(7);
                otherqq = otherqq.Substring(0, otherqq.Length - 1);
                long t;
                if (!long.TryParse(otherqq, out t)) return;
                PersonData other = DataRunTime.GetPerson_Add(param.GroupQQ, otherqq);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Query", other.QQ, other.ChaosCount);
            }
        }
    }
}
