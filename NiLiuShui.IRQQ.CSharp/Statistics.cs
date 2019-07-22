using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiLiuShui.IRQQ.CSharp
{
    class Statistics : IModule
    {
        public bool WhenParamIn(SendParam param)
        {
            //命令
            if (param.param[0] == Support.GetText("Command_to_Statistics"))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(Support.GetText("Response_to_Statistics"));
                var result = DataRunTime.GetTop(param.GroupQQ);
                for (int i = 0; i < Math.Min(3, result.Count); i++)
                {
                    builder.AppendLine(Support.GetText("Response_to_Query", result[i].QQ, result[i].ChaosCount));
                }
                builder.AppendLine(Support.GetText("Response_to_Statistics_1"));
                result = DataRunTime.GetBtm(param.GroupQQ);
                for (int i = 0; i < Math.Min(3, result.Count); i++)
                {
                    builder.AppendLine(Support.GetText("Response_to_Query", result[i].QQ, result[i].ChaosCount));
                }
                Support.Response(param.GroupQQ, param.QQ, builder.ToString());
            }
            return true;
        }
    }
}
