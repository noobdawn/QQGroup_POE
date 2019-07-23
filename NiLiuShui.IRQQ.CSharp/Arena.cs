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
        public bool WhenParamIn(SendParam param)
        {
            //混沌打击
            if (param.param[0] == _S.GetText("Command_to_ChaosStrike") && param.target != null)
            {
                int cost = 20;
                if (!_S.Cost(param.caster, cost)) return true;
                int dmg = _S.RandomInt(1, 20);
                var result = _S.Damage(param.caster, param.target, dmg, true, true);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_ChaosStrike", param.caster.QQ, param.target.QQ, cost, -result[0], result[1]);
                return false;
            }

            //查询状态
            if (param.param[0] == _S.GetText("Command_to_Detect") && param.target != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(_S.GetText("Response_to_Detect", param.caster.QQ, param.target.QQ));
                for(int i = 0; i < param.target.Properties.Length; i++)
                {
                    var grade = param.target.Properties[i];
                    sb.Append(_S.GetText("Text_Property_" + i.ToString()));
                    sb.Append(_S.GetPropertyText(i, (int)grade));
                    if (i < param.target.Properties.Length - 1)
                        sb.Append("\n");
                }
                _S.Response(param.GroupQQ, param.QQ, sb.ToString());
                return false;
            }
            
            //升级技能
            if (param.param[0] == _S.GetText("Command_to_LevelUp") && param.param.Length == 1)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < param.target.Properties.Length; i++)
                {
                    var grade = param.target.Properties[i];
                    sb.Append(_S.GetText("Text_Property_" + i.ToString()));
                    sb.Append(_S.GetPropertyText(i, (int)grade));
                    //TODO
                    if (i < param.target.Properties.Length - 1)
                        sb.Append("\n");
                }
                _S.Response(param.GroupQQ, param.QQ, sb.ToString());
                return false;
            }
            int pid;
            if (param.param[0] == _S.GetText("Command_to_LevelUp") && param.param.Length == 2 && int.TryParse(param.param[1], out pid))
            {
                int cost = _S.GetPropertyCost(pid, (int)param.caster.Properties[(int)pid]);
                if (param.caster.ExCount >= cost)
                {
                    param.caster.ExCount -= cost;
                    param.caster.Properties[(int)pid]++;
                }
                else
                    //todo, print error

                return false;
            }

                //if (param.param[0] == Support.GetText("Command_to_Syndicate") && param.param.Length == 1)
                //{
                //    //int cost = 100;
                //    //if (param.caster.ChaosCount < cost)
                //    //    return true;
                //    //DataRunTime.ChaosChange(param.caster.GroupQQ, param.caster.QQ, -cost);
                //    //Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate", param.caster.QQ, cost);
                //    //var result = DataRunTime.GetTop(param.GroupQQ);
                //    //int allLost = 150;
                //    //int[] losts = new int[Math.Min(3, result.Count)];
                //    //for (int i = 0; i < losts.Length - 1; i++)
                //    //{
                //    //    losts[i] = random.Next(0, allLost);
                //    //    allLost -= losts[i];
                //    //}
                //    //losts[losts.Length - 1] = allLost;
                //    //for (int i = 0; i < losts.Length; i++)
                //    //{
                //    //    PersonData p = result[i];
                //    //    DataRunTime.ChaosChange(p.GroupQQ, p.QQ, -losts[i]);
                //    //    Support.Response(param.GroupQQ, param.QQ, "Response_to_Syndicate_Lost", p.QQ, losts[i]);
                //    //}
                //}
            return true;
        }
    }
}
