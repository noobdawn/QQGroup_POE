using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    interface IModule
    {
        void WhenParamIn(SendParam param);
    }

    /// <summary>
    /// 版权声明：此SDK是应{续写}邀请为IRQQ\CleverQQ编写，请合理使用无用于黄赌毒相关方面。
    /// 作者QQ：1276986643,铃兰
    /// 如果您对CleverQQ感兴趣，欢迎加入QQ群：763804063,476715371，进行讨论
    /// 最后修改时间为2019年6月28日 10:29:20,初夏
    /// 本SDK编译生成的插件需要有.Net4.0以上的环境才能运行
    /// </summary>
    public class IRQQMain
    {
        private static IModule[] modules;

        /// <summary>
        /// 插件名称
        /// </summary>
        public const string pluginName = "NiLiuShui";

        /// <summary>
        /// 插件版本
        /// </summary>
        public const string pluginVersion = "1.0.0";

        /// <summary>
        /// 插件作者
        /// </summary>
        public const string pluginAuthor = "情事";

        /// <summary>
        /// 插件描述
        /// </summary>
        public const string pluginDescribe = "这是一个C#语言的IRQQ插件SDK模板，请自行修改使用";

        /// <summary>
        /// 插件Skey 请勿随意改动此项
        /// </summary>
        public const string pluginSkey = "8956RTEWDFG3216598WERDF3";//请勿随意改动此项

        /// <summary>
        /// 插件Sdk版本 请勿随意改动此项
        /// </summary>
        public const string pluginSdk = "S3";//请勿随意改动此项


        [DllExport(ExportName = nameof(IR_Create), CallingConvention = CallingConvention.StdCall)]
        public static string IR_Create()
        {
            LitJson.JsonData j = new LitJson.JsonData();
            string szBuffer = "插件名称{" + pluginName + "}\n插件版本{" + pluginVersion + "}\n插件作者{" + pluginAuthor + "}\n插件说明{" + pluginDescribe + "}\n插件skey{" + pluginSkey + "}插件sdk{" + pluginSdk + "}";
            Timeslice.Open();
            DataRunTime.Load();
            Support.InitLocalization();
            modules = new IModule[]
            {
                new Bank(),
                new Arena(),
            };
            return szBuffer;
        }
        [DllExport(ExportName = nameof(IR_Message), CallingConvention = CallingConvention.StdCall)]
        public static int IR_Message(IntPtr RobotQQ, IntPtr MsgType, IntPtr Msg, IntPtr Cookies, IntPtr SessionKey, IntPtr ClientKey)
        {
            return 1;
        }

        public static bool Debug { get; set; }


        [DllExport(ExportName = nameof(IR_Event), CallingConvention = CallingConvention.StdCall)]
        public static int IR_Event(IntPtr RobotQQ, int MsgType, int MsgCType, IntPtr MsgFrom, IntPtr TigObjF, IntPtr TigObjC, IntPtr Msg, IntPtr MsgNum, IntPtr MsgID, IntPtr RawMsg, IntPtr Json, int pText)
        {
            String RobotQQStr = IRQQUtil.ToAnsiString(RobotQQ);
            String MsgFromStr = IRQQUtil.ToAnsiString(MsgFrom);
            String TigObjFStr = IRQQUtil.ToAnsiString(TigObjF);
            String TigObjCStr = IRQQUtil.ToAnsiString(TigObjC);
            String MsgStr = String.Empty;
            String MsgNumStr = String.Empty;
            String MsgIDStr = String.Empty;
            String RawMsgStr = IRQQUtil.ToAnsiString(RawMsg);
            String JsonStr = String.Empty;
            if (!(MsgType == IRQQConst.IRC_MRBPZJRLQ || MsgType == IRQQConst.IRC_MRQQRQ))
            {
                MsgStr = IRQQUtil.ToAnsiString(Msg);
                MsgNumStr = IRQQUtil.ToAnsiString(MsgNum);
                MsgIDStr = IRQQUtil.ToAnsiString(MsgID);
                JsonStr = IRQQUtil.ToAnsiString(Json);
            }
            return IR_Event(RobotQQStr, MsgType, MsgCType, MsgFromStr, TigObjFStr, TigObjCStr, MsgStr, MsgNumStr, MsgIDStr, RawMsgStr, JsonStr, pText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RobotQQ">机器人QQ         多Q版用于判定哪个QQ接收到该消息</param>
        /// <param name="MsgType">消息类型         接收到消息类型，该类型可在常量表中查询具体定义，此处仅列举： - 1 未定义事件 1 好友信息 2, 群信息 3, 讨论组信息 4, 群临时会话 5, 讨论组临时会话 6, 财付通转账</param>
        /// <param name="MsgCType">消息子类型      此参数在不同消息类型下，有不同的定义，暂定：接收财付通转账时 1为好友 2为群临时会话 3为讨论组临时会话    有人请求入群时，不良成员这里为1</param>
        /// <param name="MsgFrom">消息来源         此消息的来源，如：群号、讨论组ID、临时会话QQ、好友QQ等</param>
        /// <param name="TigObjF">触发对象_主动    主动发送这条消息的QQ，踢人时为踢人管理员QQ</param>
        /// <param name="TigObjC">触发对象_被动    被动触发的QQ，如某人被踢出群，则此参数为被踢出人QQ</param>
        /// <param name="Msg">消息内容             此参数有多重含义，常见为：对方发送的消息内容，但当IRC_消息类型为 某人申请入群，则为入群申请理由</param>
        /// <param name="MsgNum">消息序号          此参数暂定用于消息回复，消息撤回</param>
        /// <param name="MsgID">消息ID             此参数暂定用于消息撤回</param>
        /// <param name="RawMsg">原始信息          UDP收到的原始信息，特殊情况下会返回JSON结构（收到群验证事件时，这里为该事件seq）</param>
        /// <param name="Json">Json信息            JSON格式转账解析</param>
        /// <param name="pText">信息回传文本指针   此参数用于插件加载拒绝理由  用法：写到内存（“拒绝理由”，IRC_信息回传文本指针_Out）</param>
        /// <returns></returns>
        ///此子程序会分发IRC_机器人QQ接收到的所有：事件，消息；您可在此函数中自行调用所有参数
        public static int IR_Event(string RobotQQ, int MsgType, int MsgCType, string MsgFrom, string TigObjF, string TigObjC, string Msg, string MsgNum, string MsgID, string RawMsg, string Json, int pText)
        {
            //发送图片
            //String picPath = AppDomain.CurrentDomain.BaseDirectory + "1.jpg";
            //IRQQApi.Api_SendMsg(RobotQQ, MsgType, MsgFrom, TigObjF, IRQQConst.getPic(picPath), -1);
            if (MsgType == 2)
            {
                SendParam send = Filter.MakeParam(MsgFrom, TigObjF, Msg);
                if (send == null)
                    return 1;
                foreach (var module in modules)
                {
                    module.WhenParamIn(send);
                }
            }

            return 1;
        }

        [DllExport(ExportName = nameof(IR_SetUp), CallingConvention = CallingConvention.StdCall)]
        ///启动窗体
        public static void IR_SetUp()
        {
            new FormMain().Show();
        }

        
        [DllExport(ExportName = nameof(IR_DestroyPlugin), CallingConvention = CallingConvention.StdCall)]
        ///插件即将被销毁
        public static int IR_DestroyPlugin()
        {
            Timeslice.Close();
            if (modules != null)
                for (int i = 0; i < modules.Length; i++)
                {
                    modules[i] = null;
                }
            DataRunTime.Save();
            return 0;
        }
    }
}
