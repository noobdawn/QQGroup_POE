using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace NiLiuShui.IRQQ.CSharp
{
    class _IO
    {
        public DataSet Load()
        {
            string path = Config.CachePath + "\\" + "Data.txt";
            //新档案
            if (!File.Exists(path)) return null;
            var sr = File.ReadAllText(path);
            DataSet result = LitJson.JsonMapper.ToObject<DataSet>(sr);
            return result;
        }

        public void Save(DataSet set)
        {
            if (set == null) return;
            string path = Config.CachePath + "\\" + "Data.txt";
            //删除旧的数据文件
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            var sw = File.CreateText(path);
            sw.Write(LitJson.JsonMapper.ToJson(set));
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 通过反射去获取数据
        /// </summary>
        //public string ObjectToLine(Object data, Type type, ref StringBuilder sb)
        //{
        //    if (data.GetType().Equals(type) && !type.Equals(typeof(PersonData)))
        //        return "";
        //    sb.Clear();
        //    FieldInfo[] fieldInfos = type.GetFields();
        //    foreach (var fieldInfo in fieldInfos)
        //    {
        //        sb.Append(fieldInfo.GetValue(data));
        //        sb.Append("|-zxc-|");
        //    }
        //    return sb.ToString();
        //}    
    }
}
