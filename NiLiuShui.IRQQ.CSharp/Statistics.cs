﻿using System;
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
            if (param.param[0] == _S.GetText("Command_to_Statistics") && param.param.Length == 1)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(_S.GetText("Response_to_Statistics"));
                var result = DataRunTime.GetTop(param.GroupQQ);
                for (int i = 0; i < Math.Min(5, result.Count); i++)
                {
                    builder.AppendLine(_S.GetText("Response_to_Query", result[i].NickName, result[i].ChaosCount, result[i].ExCount));
                }
                builder.AppendLine(_S.GetText("Response_to_Statistics_1"));
                _S.Response(param.GroupQQ, param.QQ, builder.ToString());
            }
            return true;
        }
    }
}
