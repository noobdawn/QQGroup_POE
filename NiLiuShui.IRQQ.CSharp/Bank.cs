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

        public bool WhenParamIn(SendParam param)
        {
            //长度低于6则不计算
            if (param.content.Length >= 6)
            {
                //低保
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, 1);
                //随机
                int rand = random.Next(0, 10000);
                int rdCoin = 0;
                if (rand <= 1)
                {
                    rdCoin = random.Next(1000, 3000);
                    DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                    Support.Response(param.GroupQQ, param.QQ, "GoodLuck_2", param.caster.QQ, rdCoin);
                }
                else if (rand <= 50)
                {
                    rdCoin = random.Next(50, 150);
                    DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                    Support.Response(param.GroupQQ, param.QQ, "GoodLuck_1", param.caster.QQ, rdCoin);
                }
                else if (rand <= 300)
                {
                    rdCoin = random.Next(20, 50);
                    DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                    Support.Response(param.GroupQQ, param.QQ, "GoodLuck_0", param.caster.QQ, rdCoin);
                }
            }
            //命令
            if (param.param[0] == Support.GetText("Command_to_Query"))
            {
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Query", param.caster.QQ, param.caster.ChaosCount);
            }
            if (param.param[0] == Support.GetText("Command_to_Query_Other") && param.target != null)
            {
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Query", param.target.QQ, param.target.ChaosCount);
            }
            if (param.param[0] == Support.GetText("Command_to_GMAdd") && param.QQ == Support.God)
            {
                int count = int.Parse(param.param[1]);
                DataRunTime.GMChaos(param.GroupQQ, count);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_GMAdd", count);
            }
            return true;
        }
    }
}
