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
            //命令
            if (param.param[0] == Support.GetText("Command_to_ChaosStrike") && param.target != null)
            {
                int cost = 20;
                if (!Support.Cost(param.caster, cost)) return true;
                int dmg = random.Next(1, 20);
                var realDmg = Support.Damage(param.caster, param.target, dmg, true, true);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_ChaosStrike", param.caster.QQ, param.target.QQ, cost, -realDmg);
            }
            if (param.param[0] == Support.GetText("Command_to_Syndicate") && param.param.Length == 1)
            {
                //int cost = 100;
                //if (param.caster.ChaosCount < cost)
                //    return true;
                //DataRunTime.ChaosChange(param.caster.GroupQQ, param.caster.QQ, -cost);
                //Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate", param.caster.QQ, cost);
                //var result = DataRunTime.GetTop(param.GroupQQ);
                //int allLost = 150;
                //int[] losts = new int[Math.Min(3, result.Count)];
                //for (int i = 0; i < losts.Length - 1; i++)
                //{
                //    losts[i] = random.Next(0, allLost);
                //    allLost -= losts[i];
                //}
                //losts[losts.Length - 1] = allLost;
                //for (int i = 0; i < losts.Length; i++)
                //{
                //    PersonData p = result[i];
                //    DataRunTime.ChaosChange(p.GroupQQ, p.QQ, -losts[i]);
                //    Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate_Lost", p.QQ, losts[i]);
                //}
            }
            return true;
        }
    }
}
