using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// CleverQQ官方提供的API，如发现本SDK内缺少部分API，可手动添加或者联系我添加,QQ:1612121215
    /// 手动添加完记得联系我,补上!
    /// 手动例子:
    ///  [DllImport(IRQQApiPath, EntryPoint = "Api_GetAge")]
    ///  public static extern int ApiGetAge([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
    ///  
    /// EntryPoint 指明API DLL内的方法名
    /// 参数个数必须对应,否则无法调用
    /// </summary>
    public static class IRQQApi
    {

        public const string IRQQApiPath = "../IRapi.dll";

        /// <summary>
        ///  修改对象群头衔（安卓协议可用）
        /// </summary>
        /// <param name="RobotQQ">响应QQ</param>
        /// <param name="GroupNum">群号</param>
        /// <param name="ObjQQ">对象QQ</param>
        /// <param name="Title">头衔名称</param>
        /// <returns></returns>
        [DllImport(IRQQApiPath, EntryPoint = "Api_SetGroupMemberTitle")]
        public static extern void Api_SetGroupMemberTitle([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Title);


        /// <summary>
        /// 消息撤回（成功返回空，失败返回腾讯给出的理由）（Pro版可用）
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="GroupNum">需撤回消息群号</param>
        /// <param name="MsgNum">需撤回消息序号</param>
        /// <param name="MsgId">需撤回消息ID</param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        [DllImport(IRQQApiPath, EntryPoint = "Api_WithdrawMsg")]
        public static extern string Api_WithdrawMsg([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string MsgNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string MsgId);

        /// <summary>
        /// 取年龄 成功返回年龄 失败返回-1
        /// </summary>
        /// <param name="RobotQQ">响应QQ</param>
        /// <param name="ObjQQ">对象QQ</param>
        /// <returns></returns>
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetAge")]
        public static extern int Api_GetAge([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);

        [DllImport(IRQQApiPath)]
        ///<summary>
        ///将好友拉入黑名单，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲拉黑的QQ</param>
        public static extern bool Api_AddBkList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_AddQQ")]
        ///<summary>
        ///向框架帐号列表增加一个登录QQ，成功返回真（CleverQQ可用）
        ///</summary>
        ///<param name="RobotQQ">帐号</param>
        ///<param name="PassWord">密码</param>
        ///<param name="Auto">自动登录</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_AddQQ([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string PassWord, bool Auto);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///管理员邀请对象入群，每次只能邀请一个对象，频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请对象QQ</param>
        ///<param name="GroupNum">欲邀请加入的群号</param>
        public static extern void Api_AdminInviteGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath, EntryPoint = "Api_CreateDisGroup")]
        ///<summary>
        ///创建一个讨论组，成功返回讨论组ID，失败返回空
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupName">讨论组名称</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_CreateDisGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string DisGroupName);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern void Api_DelBkList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///删除好友，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲删除对象QQ</param>
        public static extern bool Api_DelFriend([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///请求禁用插件自身
        ///</summary>
        public static extern void Api_DisabledPlugin();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetBkn")]
        ///<summary>
        ///取得机器人网页操作用参数Bkn或G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetBkn([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetBkn32")]
        ///<summary>
        ///取得机器人网页操作用参数长Bkn或长G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetBkn32([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetBlogPsKey")]
        ///<summary>
        ///取得腾讯微博页面操作用参数P_skey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetBlogPsKey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetClassRoomPsKey")]
        ///<summary>
        ///取得腾讯课堂页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetClassRoomPsKey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetClientkey")]
        ///<summary>
        ///取得机器人网页操作用的Clientkey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetClientkey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetCookies")]
        ///<summary>
        ///取得机器人网页操作用的Cookies
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetCookies([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetDisGroupList")]
        ///<summary>
        ///取得讨论组列表
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetDisGroupList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetEmail")]
        ///<summary>
        ///取邮箱，当对象QQ不为10000@qq.com时，可用于获取正确邮箱
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetEmail([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetFriendList")]
        ///<summary>
        ///取得好友列表，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetFriendList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///取对象性别 1男 2女 未知或失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>

        public static extern int Api_GetGender([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupAdmin")]
        ///<summary>
        ///取得群管理员，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取管理员列表群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupAdmin([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupCard")]
        ///<summary>
        ///取对象群名片
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">欲取得群名片的QQ号码</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupCard([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupList")]
        ///<summary>
        ///取得群列表，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupMemberList")]
        ///<summary>
        ///取得群成员列表，返回获取到的原始JSON格式信息，需自行解析（有群员昵称）易频繁
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取群成员列表群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupMemberList([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);


        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupMemberList_B")]
        ///<summary>
        ///取得群成员列表，返回QQ号和身份Json格式信息 失败返回空（无群员昵称）
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取群成员列表群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupMemberList_B([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);

        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupMemberList_C")]
        ///<summary>
        ///取得群成员列表，返回获取到的原始JSON格式信息，需自行解析（有群员昵称）
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取群成员列表群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupMemberList_C([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);


        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupName")]
        ///<summary>
        ///取QQ群名
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupName([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetGroupPsKey")]
        ///<summary>
        ///取得QQ群页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetGroupPsKey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetLog")]
        ///<summary>
        ///取框架日志
        ///</summary>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetLog();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetLongClientkey")]
        ///<summray>
        ///取得机器人操作网页用的长Clientkey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetLongClientkey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetLongLdw")]
        ///<summary>
        ///取得机器人操作网页用参数长Ldw
        ///</summray>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetLongLdw([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetNick")]
        ///<summary>
        ///取对象昵称
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲取得的QQ号码</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetNick([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetNotice")]
        ///<summary>
        ///取群公告，返回该群所有公告，JSON格式，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取得公告的群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetNotice([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetObjInfo")]
        ///<summary>
        ///获取对象资料，此方式为http，调用时应自行注意控制频率（成功返回JSON格式需自行解析）
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetObjInfo([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///取对象QQ等级，成功返回等级，失败返回-1
        ///</summary>
        ///<param name-="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲取得的QQ号码</param>
        public static extern int Api_GetObjLevel([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///获取对象当前赞数量，石板返回-1，成功返回赞数量（获取频繁会出现失败现象，请自行写判断处理失败问题）
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern long Api_GetObjVote([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetOffLineList")]
        ///<summary>
        ///取框架离线QQ号（多Q版可用）
        ///</summary>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetOffLineList();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetOnLineList")]
        ///<summary>
        ///取框架在线QQ号（多Q版可用）
        ///</summary>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetOnLineList();
        ///<summary>
        ///取个人说明
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetPerExp")]
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetPerExp([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetPicLink")]
        ///<summary>
        ///根据图片GUID取得图片下载链接
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="PicType">图片类型</param>
        ///<param name="ReferenceObj">参考对象</param>
        ///<param name="PicGUID">图片GUID</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetPicLink([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int PicType, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ReferenceObj, [In][MarshalAs(UnmanagedType.AnsiBStr)]string PicGUID);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///取Q龄，成功返回Q龄，失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern int Api_GetQQAge([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetQQList")]
        ///<summary>
        ///取框架所有QQ号
        ///</summary>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetQQList();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetRInf")]
        ///<summary>
        ///获取机器人状态信息，成功返回：昵称、账号、在线状态、速度、收信、发信、在线时间，失败返回空
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetRInf([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetSign")]
        ///<summary>
        ///取个性签名
        ///</summary>
        ///<param name="RobotQQ>响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetSign([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///获取当前框架内部时间戳
        ///</summary>
        public static extern long Api_GetTimeStamp();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetVer")]
        ///<summary>
        ///取框架版本号
        ///</summary>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetVer();
        [DllImport(IRQQApiPath, EntryPoint = "Api_GetZonePsKey")]
        ///<summary>
        ///取得QQ空间页面操作有用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GetZonePsKey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GIDTransGN")]
        ///<summary>
        ///群ID转群号
        ///</summary>
        ///<param name="GroupID">群ID</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GIDTransGN([In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupID);
        [DllImport(IRQQApiPath, EntryPoint = "Api_GNTransGID")]
        ///<summaray>
        ///群号转群ID
        ///</summary>
        ///<param name="GroupNum">群号</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_GNTransGID([In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);

        [DllImport(IRQQApiPath)]
        ///<summary>
        ///处理框架所有事件请求 
        ///最新版本CleverQQ已不支持该方法,请使用Api_HandleFriendEvent和Api_HandleGroupEvent
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ReQuestType">请求类型：213请求入群，214我被邀请加入某群，215某人被邀请加入群，101某人请求添加好友</param>
        ///<param name="ObjQQ">对象QQ：申请入群，被邀请人，请求添加好友人的QQ（当请求类型为214时这里请为空）</param>
        ///<param name="GroupNum">群号：收到请求的群号（好友添加时留空）</param>
        ///<param name="Handling">处理方式：10同意 20拒绝 30忽略</param>
        ///<param name="AdditionalInfo">附加信息：拒绝入群附加信息</param>
        [Obsolete("最新版本CleverQQ已不支持该方法,请使用Api_HandleFriendEvent和Api_HandleGroupEvent")]
        public static extern void Api_HandleEvent([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int ReQuestType,
          [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum,
            int Handling, [In][MarshalAs(UnmanagedType.AnsiBStr)]string AddintionalInfo);

        [DllImport(IRQQApiPath)]
        ///<summary>
        ///某人请求入群 被邀请入群 等 群验证处理 
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ReQuestType">请求类型：213请求入群，214我被邀请加入某群，215某人被邀请加入群，101某人请求添加好友</param>
        ///<param name="ObjQQ">对象QQ：申请入群，被邀请人，请求添加好友人的QQ（当请求类型为214时这里请为空）</param>
        ///<param name="GroupNum">群号：收到请求的群号（好友添加时留空）</param>
        ///<param name="seq">需要处理事件的seq ，这个参数在收到群事件时，IRC_原始信息会传递</param>
        ///<param name="Handling">处理方式：10同意 20拒绝 30忽略</param>
        ///<param name="AddintionalInfo">附加信息：拒绝入群附加信息</param>
        public static extern void Api_HandleGroupEvent([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int ReQuestType,
      [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string seq,
      int Handling, [In][MarshalAs(UnmanagedType.AnsiBStr)]string AddintionalInfo);


        [DllImport(IRQQApiPath)]
        ///<summary>
        ///某人请求添加好友验证处理
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">对象QQ：申请入群，被邀请人，请求添加好友人的QQ（当请求类型为214时这里请为空）</param>
        ///<param name="Handling">处理方式：10同意 20拒绝 30忽略</param>
        ///<param name="AddintionalInfo">附加信息：拒绝入群附加信息</param>
        public static extern void Api_HandleFriendEvent([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, int Handling, [In][MarshalAs(UnmanagedType.AnsiBStr)]string AddintionalInfo);


        [DllImport(IRQQApiPath)]
        ///<summary>
        ///是否QQ好友，好友返回真，非好友或获取失败返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="OBjQQ">对象QQ</param>
        public static extern bool Api_IfFriend([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_InviteDisGroup")]
        ///<summary>
        ///邀请对象加入讨论组，成功返回空，失败返回理由
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID>讨论组ID</param>
        ///<param name="ObjQQ">被邀请对象QQ：多个用 换行符 分割</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_InviteDisGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string DisGroupID, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///取得插件自身启用状态，启用真，禁用假
        ///</summary>
        public static extern bool Api_IsEnable();
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///查询对象或自己是否被禁言，禁言返回真，失败或未禁言返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern bool Api_IsShutUp([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///申请加群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Reason">附加理由，可留空</param>
        public static extern void Api_JoinGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNUm, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Reason);
        [DllImport(IRQQApiPath, EntryPoint = "Api_KickDisGroupMBR")]
        ///<summary>
        ///将对象移除讨论组，成功返回空，失败返回理由
        ///</summray>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID">需要执行的讨论组ID</param>
        ///<param name="ObjQQ">被执行对象</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_KickDisGroupMBR([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string DisGroupID, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///将对象移出群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">被执行对象</param>
        public static extern void Api_KickGroupMBR([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///载入插件
        ///</summary>
        public static extern void Api_LoadPlugin();
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///登录指定QQ，应确保QQ号码在列表中已经存在
        ///</summary>
        ///<param name="RobotQQ">欲登录的QQ</param>
        public static extern void Api_LoginQQ([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///非管理员邀请对象入群，每次只能邀请一个对象，频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请人QQ号码</param>
        ///<param name="GroupNum">群号</param>
        public static extern void Api_NoAdminInviteGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///令指定QQ下线，应确保QQ号码已在列表中且在线
        ///</summary>
        ///<param name="RobotQQ">欲下线的QQ</param>
        public static extern void Api_OffLineQQ([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_OutPutLog")]
        ///<summary>
        ///向IRQQ日志窗口发送一条本插件的日志，可用于调试输出需要的内容，或定位插件错误与运行状态
        ///</summary>
        ///<param name="Log">日志信息</param>
        public static extern void _Api_OutPutLog([In][MarshalAs(UnmanagedType.AnsiBStr)]string Log);


        public static void Api_OutPutLog(string Log)
        {
            if (IRQQMain.Debug)
                Console.WriteLine("["+DateTime.Now.ToString("MM-dd HH:mm")+"][Log]" + Log);
            else
                _Api_OutPutLog(Log);
        }

        [DllImport(IRQQApiPath)]
        ///<summary>
        ///发布群公告（成功返回真，失败返回假），调用此API应保证响应QQ为管理员
        ///<summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲发布公告的群号</param>
        ///<param name="Title">公告标题</param>
        ///<param name="Content">内容</param>
        public static extern bool Api_PBGroupNotic([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Title, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Content);
        [DllImport(IRQQApiPath, EntryPoint = "Api_PBHomeWork")]
        ///<summary>
        ///发布QQ群作业
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="HomeWorkName">作业名</param>
        ///<param name="HomdWorkTitle">作业标题</param>
        ///<param name="Text">作业内容</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_PBHomeWork([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string HomeWorkName, [In][MarshalAs(UnmanagedType.AnsiBStr)]string HomeWorkTitle, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Text);
        [DllImport(IRQQApiPath, EntryPoint = "Api_PBTaoTao")]
        ///<summary>
        ///发送QQ说说
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="Text">发送内容</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_PBTaoTao([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Text);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///退出讨论组
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID">需要退出的讨论组ID</param>
        public static extern void Api_QuitDisGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string DisGroupID);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///退群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲退出的群号</param>
        public static extern void Api_QuitGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///发送JSON结构消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象所属群_讨论组（消息来源），发送群、讨论组、临时会话填写、如发送对象为好友可留空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="Json">Json结构内容</param>
        public static extern void Api_SendJSON([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int SendType, int MsgType,
         [In][MarshalAs(UnmanagedType.AnsiBStr)]string MsgTo, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Json);
        [DllImport(IRQQApiPath, EntryPoint = "Api_SendMsg")]
        ///<summary>
        ///发送普通文本消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群_讨论组：发送群、讨论组、临时会话时填写</param>
        ///<param name="ObjQQ">收信QQ</param>
        ///<param name="Msg">内容</param>
        ///<param name="ABID">气泡ID</param>
        private static extern void _Api_SendMsg([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int MsgType, [In][MarshalAs(UnmanagedType.AnsiBStr)]string MsgTo,
         [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Msg, int ABID);

        public static void Api_SendMsg(string RobotQQ, int MsgType, string MsgTo, string ObjQQ, string Msg, int ABID)
        {
            if (IRQQMain.Debug)
                Console.WriteLine("RobotQQ:" + RobotQQ + ",MsgType:" + MsgType + ",MsgTo:" + MsgTo + ",ObjQQ:" + ObjQQ + ",Msg:" + Msg);
            else
                _Api_SendMsg(RobotQQ, MsgType, MsgTo, ObjQQ, Msg, ABID);
        }


        [DllImport(IRQQApiPath, EntryPoint = "Api_SendPack")]
        ///<summary>
        ///向腾讯发送原始封包（成功返回腾讯返回的包）
        ///</summary>
        ///<param name="PcakText">封包内容</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_SendPack([In][MarshalAs(UnmanagedType.AnsiBStr)]string PcakText);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///好友语音上传并发送（成功返回真，失败返回假）
        ///<summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">接收QQ</param>
        ///<param name="pAmr">语音数据的指针</param>
        public static extern bool Api_SendVoice([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, byte[] pAmr);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///发送XML消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群、讨论组：发送群、讨论组、临时时填写，如MsgType为好友可空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="ObjectMsg">XML代码</param>
        ///<param name="ObjCType">结构子类型：00基本 02点歌</param>
        public static extern void Api_SendXML([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int SendType, int MsgFrom,
          [In][MarshalAs(UnmanagedType.AnsiBStr)]string MsgTo, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjectMsg, int ObjeCType);
        [DllImport(IRQQApiPath, EntryPoint = "Api_SessionKey")]
        ///<summary>
        ///获取会话SessionKey密匙
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_SessionKey([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ);
        [DllImport(IRQQApiPath, EntryPoint = "Api_SetAdmin")]
        ///<summary>
        ///设置或取消管理员，成功返回空，失败返回理由
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        ///<param name="SetWay">操作方式，真为设置管理，假为取消管理</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_SetAdmin([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, bool SetWay);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///开关群匿名消息发送功能，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Swit">开关：真开 假关</param>
        public static extern bool Api_SetAnon([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, bool swit);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///修改对象群名片，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ：被修改名片人QQ</param>
        ///<param name="NewCard">需要修改的群名片</param>
        public static extern bool Api_SetGroupCard([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum,
           [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string NewCard);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///修改机器人在线状态，昵称，个性签名等
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="type">1 我在线上 2 Q我吧 3 离开 4 忙碌 5 请勿打扰 6 隐身 7 修改昵称 8 修改个性签名</param>
        ///<param name="ChangeText">修改内容，类型7和8时填写，其他为""</param>
        public static extern void Api_SetRInf([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int type, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ChangeText);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///屏蔽或接收某群消息
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="type">真为屏蔽接收，假为接收拼不提醒</param>
        public static extern void Api_SetShieldedGroup([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, bool type);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///向好友发起窗口抖动，调用此Api腾讯会限制频率
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">接收抖动消息的QQ</param>
        public static extern bool Api_ShakeWindow([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///禁言群内某人
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲操作的群号</param>
        ///<param name="ObjQQ">欲禁言对象，如留空且机器人QQ为管理员，将设置该群为全群禁言</param>
        ///<param name="Time">禁言时间：0解除（秒），如为全群禁言，参数为非0，解除全群禁言为0</param>
        public static extern void Api_ShutUP([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum,
         [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ, int Time);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///QQ群签到，成功返回真失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        public static extern bool Api_SignIn([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, [In][MarshalAs(UnmanagedType.AnsiBStr)]string AddressName, [In][MarshalAs(UnmanagedType.AnsiBStr)]string Content);

        [DllImport(IRQQApiPath, EntryPoint = "Api_Tea加密")]
        ///<summary>
        ///腾讯Tea加密
        ///</summary>
        ///<param name="Text">需要加密的内容</param>
        ///<param name="SessionKey">会话Key，从Api_SessionKey获得</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_Tea加密([In][MarshalAs(UnmanagedType.AnsiBStr)]string Text, [In][MarshalAs(UnmanagedType.AnsiBStr)]string SessionKey);
        [DllImport(IRQQApiPath, EntryPoint = "Api_Tea解密")]
        ///<summary>
        ///腾讯Tea解密
        ///</summary>
        ///<param name="Text">需解密的内容</param>
        ///<param name="SessionKey">会话Key，从Api_SessionKey获得</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_Tea解密([In][MarshalAs(UnmanagedType.AnsiBStr)]string Text, [In][MarshalAs(UnmanagedType.AnsiBStr)]string SessionKey);
        [DllImport(IRQQApiPath)]
        ///<summary>
        ///卸载插件自身
        ///</summary>
        public static extern void Api_UninstallPlugin();
        [DllImport(IRQQApiPath, EntryPoint = "Api_UpLoadPic")]
        ///<summary>
        ///上传图片（通过读入字节集方式），可使用网页链接或本地读入，成功返回该图片GUID,失败返回空</param>
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="UpLoadType">上传类型：1好友2群PS:好友临时用1，群讨论组用2；当填写错误时，图片GUID发送不会成功</param>
        ///<param name="UpTo">参考对象，上传该图片所属群号或QQ</param>
        ///<param name="Pic">图片字节集数据</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_UpLoadPic([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int UpLoadType,
         [In][MarshalAs(UnmanagedType.AnsiBStr)]string UpTo, byte[] hInstance);
        [DllImport(IRQQApiPath, EntryPoint = "Api_UpLoadVoice")]
        ///<summary>
        ///上传QQ语音，成功返回语音GUID，失败返回空
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="type">上传类型 2 QQ群</param>
        ///<param name="GroupNum">接收的群号</param>
        ///<param name="pAmr">语音数据指针</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_UpLoadVoice([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, int type, [In][MarshalAs(UnmanagedType.AnsiBStr)]string GroupNum, byte[] pAmr);
        [DllImport(IRQQApiPath, EntryPoint = "Api_UpVote")]
        ///<summary>
        ///调用一次点一下，成功返回空，失败返回理由如：每天最多给他点十个赞哦，调用此Api时应注意频率，每人每日10次，至多50人</param>
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被赞人QQ</param>
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string Api_UpVote([In][MarshalAs(UnmanagedType.AnsiBStr)]string RobotQQ, [In][MarshalAs(UnmanagedType.AnsiBStr)]string ObjQQ);
    }
}

