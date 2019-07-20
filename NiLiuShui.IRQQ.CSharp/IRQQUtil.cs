using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 常用工具类  如VisualStudio低于2017,请删除报错的方法!
    /// </summary>
    public static class IRQQUtil
    {
        /// <summary>
        /// 写文件,如果文件路径无文件夹会自动创建
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(String filePath, String content)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// 写出插件配置文件,路径为:\Clever根目录\config\插件名称\
        /// </summary>
        /// <param name="filePath">在默认路径后追加路径或文件名</param>
        /// <param name="content">文件内容</param>
        public static void WritePluginConfigFile(String filePath,String content)
        {
            String newFilePath = AppDomain.CurrentDomain.BaseDirectory + "config\\" + IRQQMain.pluginName + "\\" + filePath;
            FileInfo fileInfo = new FileInfo(newFilePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);
            File.WriteAllText(newFilePath, content);
        }

        /// <summary>
        /// 写出插件日志文件,路径为:\Clever根目录\config\插件名称\
        /// </summary>
        /// <param name="filePath">在默认路径后追加路径或文件名</param>
        /// <param name="content">文件内容</param>
        public static void WritePluginLogFile(String filePath, String content)
        {
            String newFilePath = AppDomain.CurrentDomain.BaseDirectory + "log\\" + IRQQMain.pluginName + "\\" + filePath;
            FileInfo fileInfo = new FileInfo(newFilePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);
            WriteFile(newFilePath, content);
        }

        //private static JavaScriptSerializer js;

        ///// <summary>
        ///// 反序列化JSON字符串为对象,如果用Object装的话就是 Dictionary<String,Object>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static T Deserialize<T>(this String str)
        //{
        //    if (js == null)
        //        js = new JavaScriptSerializer();
        //    return js.Deserialize<T>(str);
        //}

        /// <summary>
        /// 替换失效图片
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="RobotQQ"></param>
        /// <param name="MsgType">私聊为1 群为2</param>
        /// <returns></returns>
        public static String RegexRepleInvalidPic(String Msg, String RobotQQ, int MsgType)
        {
            foreach (Match item in Regex.Matches(Msg, "\\[IR:pic={.*}\\.jpg\\]"))
            {
                Msg = Msg.Replace(item.Value, "[IR:pic=" + IRQQApi.Api_GetPicLink(RobotQQ, MsgType, null, item.Value) + "]");
            }
            return Msg;
        }

        //public static String Serialize(this object obj)
        //{
        //    if (js == null)
        //    {
        //        js = new JavaScriptSerializer();
        //    }
        //    return js.Serialize(obj);
        //}

        ///// <summary>
        ///// 对象转换为字符串是否为空
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static bool IsNullOrWhiteSpace(this Object obj)
        //{
        //    if (obj == null)
        //        return true;
        //    else
        //        return obj.ToString().IsNullOrWhiteSpace();
        //}

        ///// <summary>
        ///// 字符串是否为空
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static bool IsNullOrWhiteSpace(this String str)
        //{
        //    return String.IsNullOrWhiteSpace(str);
        //}

        ///// <summary>
        ///// 转换为Int类型，转换失败则为0
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static int ParseInt(this object obj)
        //{
        //    try
        //    {
        //        if (obj.IsNullOrWhiteSpace())
        //            return 0;
        //        int.TryParse(obj.ToString(), out int result);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// 转换为DateTime类型，转换失败则返回DateTime.MinValue
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static DateTime ParseDateTime(this object obj)
        //{
        //    try
        //    {
        //        if (obj.IsNullOrWhiteSpace())
        //            return DateTime.MinValue;
        //        DateTime.TryParse(obj.ToString(), out DateTime result);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return DateTime.MinValue;
        //    }
        //}

        ///// <summary>
        ///// 转换为decimal类型，转换失败则返回0
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static decimal ParseDecimal(this object obj)
        //{
        //    try
        //    {
        //        if (obj.IsNullOrWhiteSpace())
        //            return 0;
        //        decimal.TryParse(obj.ToString(), out decimal result);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        public static string ToAnsiString(IntPtr intPtr)
        {
            return Marshal.PtrToStringAnsi(intPtr);
        }

        //public static string ToAnsiString(this IntPtr intPtr)
        //{
        //    return Marshal.PtrToStringAnsi(intPtr);
        //}
    }
}
