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
            string path = Support.CachePath + "\\" + "Data.txt";
            //新档案
            if (!File.Exists(path)) return null;
            var sr = File.OpenText(path);
            string[] lines = sr.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            sr.Close();
            sr.Dispose();
            if (lines.Length == 0)
                return null;
            DataSet result = new DataSet();
            List<PersonData> persons = new List<PersonData>();
            var fields = typeof(PersonData).GetFields();
            foreach (var line in lines)
            {
                var words = line.Split(new string[] { "|-zxc-|" }, StringSplitOptions.RemoveEmptyEntries);
                PersonData temp = new PersonData();
                for (int i = 0; i < Math.Min(words.Length, fields.Length); i++)
                {
                    FieldInfo field = fields[i];
                    if (field.FieldType == typeof(string))
                        field.SetValue(temp, words[i]);
                    else
                        field.SetValue(temp, float.Parse(words[i]));
                }
                persons.Add(temp);
            }
            result.persons = persons.ToArray();
            return result;

        }
        public void Save(DataSet set)
        {
            if (set == null) return;
            string path = Support.CachePath + "\\" + "Data.txt";
            //删除旧的数据文件
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder linesb = new StringBuilder();
            Type typeofperson = typeof(PersonData);
            foreach (var person in set.persons)
            {
                sb.Append(ObjectToLine(person, typeofperson, ref linesb));
                sb.AppendLine();
            }
            var sw = File.CreateText(path);
            sw.Write(sb.ToString());
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 通过反射去获取数据
        /// </summary>
        public string ObjectToLine(Object data, Type type, ref StringBuilder sb)
        {
            if (data.GetType().Equals(type) && !type.Equals(typeof(PersonData)))
                return "";
            sb.Clear();
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                sb.Append(fieldInfo.GetValue(data));
                sb.Append("|-zxc-|");
            }
            return sb.ToString();
        }

    
    }
}
