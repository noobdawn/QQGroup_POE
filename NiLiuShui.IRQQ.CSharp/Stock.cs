using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 股市
    /// </summary>
    class Stock : IModule
    {
        public const int MaxPrice = 200;
        public const int MinPrice = 30;

        public Stock()
        {
            time = 0;
            curPrice = _S.RandomInt(MinPrice, MaxPrice + 1);
            Timeslice.AddSecondEvent(StockStep);
        }

        int time = 0;
        int curPrice = 0;
        private void StockStep(object sender, ElapsedEventArgs e)
        {
            time++;
            if (time >= 60)
            {
                int next = _S.RandomInt(curPrice / 10 * 9 - 20, curPrice / 10 * 11 + 1 + 20);
                if (next > MaxPrice)
                    next = _S.RandomInt(curPrice / 10 * 9 - 20, MaxPrice);
                if (next < MinPrice)
                    next = _S.RandomInt(MinPrice, curPrice / 10 * 11 + 1 + 20);
                curPrice = next;
                time -= 60;
            }
        }

        public bool WhenParamIn(SendParam param)
        {
            if (param.param[0] == _S.GetText("Command_to_QueryStock") && param.param.Length == 1)
            {
                _S.Response(param.GroupQQ, param.QQ, "Response_to_QueryStock", curPrice);
                return false;
            }
            int opCnt;
            int realCost;
            if (param.param[0] == _S.GetText("Command_to_Buy") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                realCost = opCnt * curPrice;
                if (_S.Cost(param.caster, realCost))
                {
                    DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                    _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                }
                return false;
            }
            if (param.param[0] == _S.GetText("Command_to_Sell") && param.param.Length == 2 && int.TryParse(param.param[1], out opCnt))
            {
                if (opCnt <= 0) return false;
                if (opCnt > param.caster.ExCount) return false;
                opCnt = -opCnt;
                realCost = Math.Abs(opCnt * curPrice);
                DataRunTime.ExChange(param.GroupQQ, param.QQ, opCnt);
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, realCost);
                _S.Response(param.GroupQQ, param.QQ, "Response_to_StockOp", param.caster.QQ, param.caster.ChaosCount, param.caster.ExCount);
                return false;
            }
            return true;
        }

    }
}
