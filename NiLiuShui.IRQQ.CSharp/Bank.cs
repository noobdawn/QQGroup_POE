using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 银行，主要控制金币
    /// </summary>
    class Bank : IModule
    {
        public Bank()
        {
            time = 0;
            Timeslice.AddSecondEvent(AutoAdd);
        }

        int time = 0;
        private void AutoAdd(object sender, ElapsedEventArgs e)
        {
            time++;
            if (time > 60)
            {
                DataRunTime.ChaosStep();
                time -= 60;
            }
        }

        public bool WhenParamIn(SendParam param)
        {
            //低保
            DataRunTime.ChaosChange(param.GroupQQ, param.QQ, 1);
            //随机
            int rand = _S.RandomInt(0, 10000);
            int rdCoin = 0;
            if (rand <= 1)
            {
                rdCoin = _S.RandomInt(1000, 3000);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                _S.Response(param.GroupQQ, param.QQ, "GoodLuck_2", param.caster.QQ, rdCoin);
            }
            else if (rand <= 50)
            {
                rdCoin = _S.RandomInt(50, 150);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                _S.Response(param.GroupQQ, param.QQ, "GoodLuck_1", param.caster.QQ, rdCoin);
            }
            else if (rand <= 300)
            {
                rdCoin = _S.RandomInt(20, 50);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, rdCoin);
                _S.Response(param.GroupQQ, param.QQ, "GoodLuck_0", param.caster.QQ, rdCoin);
            }
            //命令
            if (param.param[0] == _S.GetText("Command_to_Query") && param.target == null)
            {
                _S.Response(param.GroupQQ, param.QQ, "Response_to_Query", param.caster.NickName, param.caster.ChaosCount, param.caster.ExCount);
            }
            if (param.param[0] == _S.GetText("Command_to_Query_Other") && param.target != null)
            {
                _S.Response(param.GroupQQ, param.QQ, "Response_to_Query", param.target.NickName, param.target.ChaosCount, param.target.ExCount);
            }
            //GM
            //if (param.param[0] == _S.GetText("Command_to_GMAdd") && param.QQ == Config.God)
            //{
            //    int count = int.Parse(param.param[1]);
            //    DataRunTime.GMChaos(param.GroupQQ, count);
            //    _S.Response(param.GroupQQ, param.QQ, "Response_to_GMAdd", count);
            //}
            return true;
        }
    }
}
