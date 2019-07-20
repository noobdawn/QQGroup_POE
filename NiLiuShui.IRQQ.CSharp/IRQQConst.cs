
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 常量命名全部为一键转换,将中文解析成了拼音首字母
    /// 
    /// </summary>
    public class IRQQConst
    {
        public const String Log_Type_DEBUG = "[调试信息]";

        public const String Log_Type_Plugin = "["+ IRQQMain.pluginName +"]";

        /// <summary>
        /// 对象昵称：应确保发送消息中包含IRC_触发对象_主动
        /// </summary>
        public const string IRQQ_BL_ObjName = "[ObjName]";
        /// <summary>
        /// 对象QQ：对象QQ号码
        /// </summary>
        public const string IRQQ_BL_ObjQQ = "[ObjQQ]";
        /// <summary>
        /// 对象头像： 触发对象的头像
        /// </summary>
        public const string IRQQ_BL_DisPic = "[DisPic]";
        /// <summary>
        /// 时间：表示当前时间，例：2017年1月1日18时00分00秒
        /// </summary>
        public const string IRQQ_BL_Time = "[Time]";
        /// <summary>
        /// 数字时间：取时分两位数字时间，例：18:00
        /// </summary>
        public const string IRQQ_BL_NumTime = "[NumTime]";
        /// <summary>
        /// 时间段：例：凌晨、黎明、早上、中午、下午、傍晚、晚上
        /// </summary>
        public const string IRQQ_BL_TimePer = "[TimePer]";
        /// <summary>
        /// 换行符：换行
        /// </summary>
        public const string IRQQ_BL_NewLine = "[\n]";
        /// <summary>
        /// 星期：表示当前日期星期几
        /// </summary>
        public const string IRQQ_BL_Week = "[Week]";
        /// <summary>
        /// 群名：当前消息来源群名
        /// </summary>
        public const string IRQQ_BL_GName = "[GName]";
        /// <summary>
        /// 群号：当前消息来源群号
        /// </summary>
        public const string IRQQ_BL_GNum = "[GNum]";
        /// <summary>
        /// 机器人昵称：机器人的昵称
        /// </summary>
        public const string IRQQ_BL_RName = "[RName]";
        /// <summary>
        /// 机器人QQ：机器人号码
        /// </summary>
        public const string IRQQ_BL_RQQ = "[RQQ]";
        /// <summary>
        /// 农历：例如：五月初五
        /// </summary>
        public const string IRQQ_BL_LunC = "[LunC]";
        /// <summary>
        /// 随机Face表情：QQ小黄豆表情
        /// </summary>
        public const string IRQQ_BL_RFace = "[RFace]";
        /// <summary>
        /// 信息分段发送：将一条消息分作两次发送
        /// </summary>
        public const string IRQQ_BL_Next = "[Next]";
        /// <summary>
        /// 艾特码：艾特全体成员，当然必须是管理员
        /// </summary>
        public const string IRQQ_BL_AtAll1 = "[IR:at=all]";
        /// <summary>
        /// 艾特码：艾特全体成员，当然必须是管理员
        /// </summary>
        public const string IRQQ_BL_AtAll2 = "[IR:at=全体人员]";

        /// <summary>
        /// 随机数 [1,300]
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns></returns>
        public static string atQQ(int min,int max)
        {
            return "["+min+","+max+"]";
        }

        /// <summary>
        /// 艾特个人 [IR:at=123456]
        /// </summary>
        /// <param name="QQ">123456</param>
        /// <returns></returns>
        public static string atQQ(String QQ)
        {
            return "[IR:at="+QQ+"]";
        }

        /// <summary>
        /// 表情码：将X替换为1-213   如：[Face1.gif]
        /// </summary>
        /// <param name="X">范围1-213</param>
        /// <returns></returns>
        public static string getFace(String X)
        {
            return "[Face"+X+".gif]";
        }

        /// <summary>
        /// 颜表情：具体代码请发送颜表情给机器人查看
        /// </summary>
        /// <param name="bqdm">表情代码 具体代码请发送颜表情给机器人查看</param>
        /// <returns></returns>
        public static string getEmoji(String bqdm)
        {
            return "[emoji="+bqdm+"]";
        }

        /// <summary>
        /// 图片发送：图片路径或地址 或者 直接发送框架收到的图片GUID
        /// </summary>
        /// <param name="src">图片路径或地址 或者 直接发送框架收到的图片GUID</param>
        /// <returns></returns>
        public static string getPic(String pic)
        {
            return "[IR:pic=" + pic + "]";
        }

        /// <summary>
        /// 秀图发送：图片路径或地址（Pro)
        /// </summary>
        /// <param name="ShowPic">图片路径或地址 或者 直接发送框架收到的图片GUID</param>
        /// <param name="type">秀图特效可参考常量，或 0 1 2 3 4 5 代替 </param>
        /// <returns></returns>
        public static string getPic(String ShowPic,String type)
        {
            return "[IR:ShowPic=" + ShowPic + ",type="+type+"]";
        }

        /// <summary>
        /// 秀图发送2：框架收到的图片GUID（Pro)
        /// </summary>
        /// <param name="ShowPic">框架收到的图片GUID</param>
        /// <param name="type">秀图特效可参考常量，或 0 1 2 3 4 5 代替 </param>
        /// <returns></returns>
        public static string getPic2(String ShowPic, String type)
        {
            return "[IR:ShowPic={" + ShowPic + "}.jpg,type=" + type + "]";
        }

        /// <summary>
        /// 闪照发送：图片路径或地址（Pro)
        /// </summary>
        /// <param name="ShowPic">图片路径或地址</param>
        /// <param name="type">秀图特效可参考常量，或 0 1 2 3 4 5 代替 </param>
        /// <returns></returns>
        public static string getFlashPic(String FlashPic)
        {
            return "[IR:FlashPic=" + FlashPic + "]";
        }

        /// <summary>
        /// 闪照发送2：框架收到的闪照GUID（Pro)
        /// </summary>
        /// <param name="ShowPic">框架收到的图片GUID</param>
        /// <param name="type">秀图特效可参考常量，或 0 1 2 3 4 5 代替 </param>
        /// <returns></returns>
        public static string getFlashPic2(String FlashPic)
        {
            return "[IR:FlashPic={" + FlashPic + "}.jpg]";
        }

        /// <summary>
        /// 语音发送
        /// </summary>
        /// <param name="Voi">文件路径</param>
        /// <returns></returns>
        public static string getVoi(String Voi)
        {
            return "[IR:Voi={" + Voi + "}.amr]";
        }


        ///<summary>
        /// IRC_未定义
        ///</summary>
        public const int IRC_WDY = -1;

        ///<summary>
        /// IRC_在线状态临时会话   （Pro版可用）
        ///</summary>
        public const int IRC_ZXZTLSHH = 0;

        ///<summary>
        /// IRC_好友
        ///</summary>
        public const int IRC_HY = 1;

        ///<summary>
        /// IRC_群
        ///</summary>
        public const int IRC_Q = 2;

        ///<summary>
        /// IRC_讨论组
        ///</summary>
        public const int IRC_TLZ = 3;

        ///<summary>
        /// IRC_群临时会话
        ///</summary>
        public const int IRC_QLSHH = 4;

        ///<summary>
        /// IRC_讨论组临时会话
        ///</summary>
        public const int IRC_TLZLSHH = 5;

        ///<summary> 
        /// IRC_好友验证回复会话       （Pro版可用）
        ///</summary>
        public const int IRC_HYYZHFHH = 7;

        ///<summary>
        /// IRC_收到财付通转账
        ///</summary>
        public const int IRC_SDCFTZZ = 6;
        /// <summary>
        /// IRC_机器人发出讨论组消息
        /// </summary>
        public const int IRC_JQRFCQXX = 2000;
        /// <summary>
        /// IRC_机器人发出群消息
        /// </summary>
        public const int IRC_JQRFCTLZXX = 3000;

        ///<summary>
        /// IRC_请求处理_同意
        ///</summary>
        public const int IRC_QQCL_TY = 10;

        ///<summary>
        /// IRC_请求处理_拒绝
        ///</summary>
        public const int IRC_QQCL_JJ = 20;

        ///<summary>
        /// IRC_请求处理_忽略
        ///</summary>
        public const int IRC_QQCL_HL = 30;

        ///<summary>
        /// IRC_被单项添加为好友
        ///</summary>
        public const int IRC_BDXTJWHY = 100;

        ///<summary>
        /// IRC_某人请求加为好友
        ///</summary>
        public const int IRC_MRQQJWHY = 101;

        ///<summary>
        /// IRC_被同意加为好友
        ///</summary>
        public const int IRC_BTYJWHY = 102;

        ///<summary>
        /// IRC_被拒绝加为好友
        ///</summary>
        public const int IRC_BJJJWHY = 103;

        ///<summary>
        /// IRC_被删除好友
        ///</summary>
        public const int IRC_BSCHY = 104;

        ///<summary>
        /// IRC_好友签名变更
        ///</summary>
        public const int IRC_HYQMBG = 106;

        ///<summary>
        /// IRC_说说被某人评论
        ///</summary>
        public const int IRC_SSBMRPL = 107;

        ///<summary>
        /// IRC_好友离线文件接收   （Pro版可用）
        ///</summary>
        public const int IRC_HYLXWJJS = 105;

        ///<summary>
        /// IRC_群文件接收
        ///</summary>
        public const int IRC_QWJJS = 218;

        ///<summary>
        /// IRC_某人请求入群
        ///</summary>
        public const int IRC_MRQQRQ = 213;

        ///<summary>
        /// IRC_被邀请加入群
        ///</summary>
        public const int IRC_BYQJRQ = 214;

        ///<summary>
        /// IRC_被批准入群
        ///</summary>
        public const int IRC_BPZRQ = 220;

        ///<summary>
        /// IRC_被拒绝入群
        ///</summary>
        public const int IRC_BJJRQ = 221;

        ///<summary>
        /// IRC_某人被邀请加入群
        ///</summary>
        public const int IRC_MRBYQJRQ = 215;

        ///<summary>
        /// IRC_某人已被邀请入群     （群主或管理员邀请成员加群或开启了群成员100以内无需审核或无需审核直接进群，被邀请人同意进群后才会触发）
        ///</summary>
        public const int IRC_MRYBYQRQ = 219;

        ///<summary>
        /// IRC_某人被批准加入了群
        ///</summary>
        public const int IRC_MRBPZJRLQ = 212;

        ///<summary>
        /// IRC_某人退出群
        ///</summary>
        public const int IRC_MRTCQ = 201;

        ///<summary>
        /// IRC_某人被管理移除群
        ///</summary>
        public const int IRC_MRBGLYCQ = 202;

        ///<summary>
        /// IRC_某群被解散
        ///</summary>
        public const int IRC_MQBJS = 216;

        ///<summary>
        /// IRC_某人成为管理
        ///</summary>
        public const int IRC_MRCWGL = 210;

        ///<summary>
        /// IRC_某人被取消管理
        ///</summary>
        public const int IRC_MRBQXGL = 211;

        ///<summary>
        /// IRC_对象被禁言
        ///</summary>
        public const int IRC_DXBJY = 203;

        ///<summary>
        /// IRC_对象被解除禁言
        ///</summary>
        public const int IRC_DXBJCJY = 204;

        ///<summary>
        /// IRC_开启全群禁言
        ///</summary>
        public const int IRC_KQQQJY = 205;

        ///<summary>
        /// IRC_关闭全群禁言
        ///</summary>
        public const int IRC_GBQQJY = 206;

        ///<summary>
        /// IRC_开启匿名聊天
        ///</summary>
        public const int IRC_KQNMLT = 207;

        ///<summary>
        /// IRC_关闭匿名聊天
        ///</summary>
        public const int IRC_GBNMLT = 208;

        ///<summary>
        /// IRC_群公告变动
        ///</summary>
        public const int IRC_QGGBD = 209;

        ///<summary>
        /// IRC_群名片变动
        ///</summary>
        public const int IRC_QMPBD = 217;

        ///<summary>
        /// IRC_列表添加了新帐号（Pro可用）
        ///</summary>
        public const int IRC_LBTJLXZH = 1100;

        ///<summary>
        /// IRC_QQ登录完成
        ///</summary>
        public const int IRC_QQDLWC = 1101;

        ///<summary>
        /// IRC_QQ被手动离线（Pro可用）
        ///</summary>
        public const int IRC_QQBSDLX = 1102;

        ///<summary>
        /// IRC_QQ被强制离线（Pro可用）
        ///</summary>
        public const int IRC_QQBQZLX = 1103;

        ///<summary>
        /// IRC_QQ掉线（Pro可用）
        ///</summary>
        public const int IRC_QQDX = 1104;

        ///<summary>
        /// IRC_秀图无特效（Pro可用）
        ///</summary>
        public const int IRC_XTWTX = 0;

        ///<summary>
        /// IRC_秀图抖动特效（Pro可用）
        ///</summary>
        public const int IRC_XTDDTX = 1;

        ///<summary>
        /// IRC_秀图幻影特效（Pro可用）
        ///</summary>
        public const int IRC_XTHYTX = 2;

        ///<summary>
        /// IRC_秀图生日特效 （Pro可用）
        ///</summary>
        public const int IRC_XTSRTX = 3;

        ///<summary>
        /// IRC_秀图爱你特效 （Pro可用）
        ///</summary>
        public const int IRC_XTANTX = 4;

        ///<summary>
        /// IRC_秀图征友特效
        ///</summary>
        public const int IRC_XTZYTX = 5;

        ///<summary>
        /// IRC_忽略
        ///</summary>
        public const int IRC_HL = 0;

        ///<summary>
        /// IRC_继续
        ///</summary>
        public const int IRC_JX = 1;

        ///<summary>
        /// IRC_拦截
        ///</summary>
        public const int IRC_LJ = 2;

        ///<summary>
        /// IRC_本插件载入      返回20可拒绝加载 其他返回值均视为允许
        ///</summary>
        public const int IRC_BCJZR = 12000;

        ///<summary>
        /// IRC_用户启用本插件   返回20可拒绝启用 其他返回值均视为允许启用
        ///</summary>
        public const int IRC_YHQYBCJ = 12001;

        ///<summary>
        /// IRC_用户禁用本插件   无权拒绝
        ///</summary>
        public const int IRC_YHJYBCJ = 12002;

        ///<summary>
        /// IRC_插件被点击   点击方式参考子类型.  1=左键单击 2=右键单击
        ///</summary>
        public const int IRC_CJBDJ = 12003;

        ///<summary>
        /// IRC_框架启动完成
        ///</summary>
        public const int IRC_KJQDWC = 10000;

        ///<summary>
        /// IRC_框架即将重启
        ///</summary>
        public const int IRC_KJJJZQ = 10001;

        ///<summary>
        /// 在线状态_我在线上
        ///</summary>
        public const int ZXZT_WZXS = 1;

        ///<summary>
        /// 在线状态_Q我吧
        ///</summary>
        public const int ZXZT_QWB = 2;

        ///<summary>
        /// 在线状态_离开
        ///</summary>
        public const int ZXZT_LK = 3;

        ///<summary>
        /// 在线状态_忙碌
        ///</summary>
        public const int ZXZT_ML = 4;

        ///<summary>
        /// 在线状态_请勿打扰
        ///</summary>
        public const int ZXZT_QWDR = 5;

        ///<summary>
        /// 在线状态_隐身
        ///</summary>
        public const int ZXZT_YS = 6;
    }
}
