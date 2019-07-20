using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NiLiuShui.IRQQ.CSharp.Tester
{
    public class Program
    {

        static void Main(string[] args)
        {
            //在本项目运行插件内的方法时,涉及到调用IRQQApi的请自行提供数据,否则调用任何API都会返回空内容!!!
            IRQQMain.Debug = true;//调试模式
            Console.WriteLine(IRQQMain.IR_Create());
            int MsgType = 1;
            int MsgCType = 1;
            int pText = 1;
            //IRQQMain.IR_Event("RobotQQ", MsgType, MsgCType, "MsgFrom", "TigObjF", "TigObjFC", "设置头衔 1612121214 测试", "MsgNum", "MsgID", "RawMsg", "Json", pText);

            IRQQApi.Api_OutPutLog("cnm");
            Application.Run(new FormMain());//运行插件设置窗口
        }
    }
}
