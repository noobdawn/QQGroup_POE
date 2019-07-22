using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 复读检测
    /// </summary>
    class Repeat : IModule
    {
        private string GQplusQQ;
        private float RepeatTimes;
        public Repeat()
        {
            Timeslice.AddSecondEvent(RepeatQuaterMin);
        }
        
        private void RepeatQuaterMin(object sender, ElapsedEventArgs e)
        {
            RepeatTimes -= 0.333f;
        }

        public bool WhenParamIn(SendParam param)
        {
            string p = param.GroupQQ + param.QQ;
            if (GQplusQQ == p)
                RepeatTimes++;
            GQplusQQ = p;
            if (RepeatTimes >= 5)
            {
                DataRunTime.ChaosChange(param.GroupQQ, param.QQ, -3);
                Support.Response(param.GroupQQ, param.QQ, "Response_to_Repeat", param.QQ, 3);
                return false;
            }
            return true;
        }
    }
}
