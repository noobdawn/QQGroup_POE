using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 群内成员的数据类
    /// </summary>
    class PersonData
    {
        //群QQ
        public string GroupQQ { get; set; }
        //QQ
        public string QQ { get; set; }
        //昵称
        public string NickName { get; set; }
        //货币数量
        public double ChaosCount { get; set; }
        public PersonData()
        {
            GroupQQ = "0";
            QQ = "0";
            NickName = "default";
            ChaosCount = 0;
        }
    }

    /// <summary>
    /// 具体用于保存的数据集
    /// </summary>
    class DataSet
    {
        public PersonData[] persons { get; set; }
    }

    /// <summary>
    /// 运行中的数据集
    /// </summary>
    static class DataRunTime
    {
        public static bool IsInited = false;
        public static Dictionary<string, Dictionary<string, PersonData>> persons = new Dictionary<string, Dictionary<string, PersonData>>();

        #region 增删查改
        public static PersonData AddNewPerson(string group_qq, string qq, bool check = true)
        {
            if (check && !IsInited) return null;
            PersonData person = new PersonData();
            person.ChaosCount = 0;
            person.GroupQQ = group_qq;
            person.QQ = qq;
            person.NickName = IRQQApi.Api_GetGroupCard(Support.RobotQQ, group_qq, qq);
            AddNewPerson(person);
            return person;
        }

        public static void AddNewPerson(PersonData person, bool check = true)
        {
            if (check && !IsInited) return;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(person.GroupQQ, out persons_in_group))
            {
                persons_in_group = new Dictionary<string, PersonData>();
                persons.Add(person.GroupQQ, persons_in_group);
            }
            if (!persons_in_group.ContainsKey(person.QQ))
            {
                persons_in_group.Add(person.QQ, person);
            }

            IRQQApi.Api_OutPutLog(string.Format("新增成员{0}, QQ:{1}", person.NickName, person.QQ));
        }

        public static void RemovePerson(string group_qq, string qq, bool check = true)
        {
            if (check && !IsInited) return;
            Dictionary<string, PersonData> persons_in_group;
            if (persons.TryGetValue(group_qq, out persons_in_group))
            {
                persons_in_group = persons[group_qq];
                if (persons_in_group.ContainsKey(qq))
                {
                    persons.Remove(qq);
                    if (persons_in_group.Count == 0)
                        persons.Remove(group_qq);
                }
            }

            IRQQApi.Api_OutPutLog(string.Format("移除成员QQ:{0}", qq));
        }

        public static PersonData GetPerson(string group_qq, string qq, bool check = true) 
        {
            if (check && !IsInited) return null;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(group_qq, out persons_in_group))
                return null;
            if (!persons_in_group.ContainsKey(qq))
                return null;
            return persons_in_group[qq];
        }

        public static bool TryGetPerson(string group_qq, string qq, out PersonData data, bool check = true)
        {
            data = null;
            if (check && !IsInited) return false;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(group_qq, out persons_in_group))
                return false;
            if (!persons_in_group.ContainsKey(qq))
                return false;
            data = persons_in_group[qq];
            return true;
        }

        public static bool ChaoEnough(string group_qq, string qq, double delta, bool check = true)
        {
            if (check && !IsInited) return false;
            var person = GetPerson(group_qq, qq);
            if (person == null)
                person = AddNewPerson(group_qq, qq);
            if (person == null)
                return false;
            return person.ChaosCount >= delta;
        }

        public static double ChaosChange(string group_qq, string qq, double delta, bool check = true)
        {
            if (check && !IsInited) return 0;
            var person = GetPerson(group_qq, qq);
            if (person == null)
                person = AddNewPerson(group_qq, qq);
            if (person == null)
                return 0;
            if (person.ChaosCount + delta < 0)
            {
                var realCost = -person.ChaosCount;
                person.ChaosCount = 0;
                return realCost;
            }
            else
            {
                person.ChaosCount += delta;
                return delta;
            }
            //IRQQApi.Api_OutPutLog(string.Format("混沌石变化 QQ:{0}", qq));
        }

        public static void NickNameChange(string group_qq, string qq, string newName, bool check = true)
        {
            if (check && !IsInited) return;
            var person = GetPerson(group_qq, qq);
            if (person == null)
                person = AddNewPerson(group_qq, qq);
            if (person == null)
                return;
            person.NickName = newName;
            IRQQApi.Api_OutPutLog(string.Format("改名 QQ:{0}", qq));
        }

        public static PersonData GetPerson_Add(string group_qq, string qq, bool check = true)
        {
            if (check && !IsInited) return null;
            var person = GetPerson(group_qq, qq);
            if (person == null)
                person = AddNewPerson(group_qq, qq);
            return person;
        }

        public static List<PersonData> GetTop(string group_qq, bool check = true)
        {
            if (check && !IsInited) return null;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(group_qq, out persons_in_group)) return null;
            var array = persons_in_group.Values.ToList();
            array.Sort((x, y) =>
            {
                return y.ChaosCount.CompareTo(x.ChaosCount);
            });
            return array;
        }

        public static List<PersonData> GetBtm(string group_qq, bool check = true)
        {
            if (check && !IsInited) return null;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(group_qq, out persons_in_group)) return null;
            var array = persons_in_group.Values.ToList();
            array.Sort((x, y) =>
            {
                return x.ChaosCount.CompareTo(y.ChaosCount);
            });
            return array;
        }

        public static void GMChaos(string group_qq, int count, bool check = true)
        {
            if (check && !IsInited) return;
            Dictionary<string, PersonData> persons_in_group;
            if (!persons.TryGetValue(group_qq, out persons_in_group)) return;
            foreach (var kv in persons_in_group)
            {
                kv.Value.ChaosCount += count;
            }
        }
        #endregion

        #region 转数据集
        public static DataSet ToSet()
        {
            DataSet set = new DataSet();
            List<PersonData> personDatas = new List<PersonData>();
            foreach (var kv_0 in persons)
            {
                foreach (var kv in kv_0.Value)
                {
                    if (kv.Value == null) continue;
                    personDatas.Add(kv.Value);
                }
            }
            set.persons = personDatas.ToArray();
            return set;
        }
        public static void FromSet(DataSet set)
        {
            if (set != null)
            {
                persons.Clear();
                foreach (var person in set.persons)
                {
                    AddNewPerson(person, false);
                }
            }
            time = 0;
            IsInited = true;
        }
        #endregion

        #region 保存和加载
        public static void Save()
        {
            _IO io = new _IO();
            io.Save(ToSet());
        }

        public static void Load()
        {
            _IO io = new _IO();
            FromSet(io.Load());
            Timeslice.AddSecondEvent(AutoSave);
        }
        #endregion

        #region 自动保存
        static int time = 0;
        public static void AutoSave(Object source, ElapsedEventArgs e)
        {
            if (!IsInited) return;
            time++;
            if (time > 60)
            {
                Save();
                time -= 60;
            }
        }
        #endregion
    }
}
