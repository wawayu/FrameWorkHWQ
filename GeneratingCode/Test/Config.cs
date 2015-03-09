//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5485
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConfigData
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections;


    public class Chapter : ConfigMetaData
    {

        internal Int32 id;

        internal String name;

        internal List<Level> levelList;

        public Int32 Id
        {
            get
            {
                return this.id;
            }
        }

        public String Name
        {
            get
            {
                return this.name;
            }
        }

        public List<Level> LevelList
        {
            get
            {
                return this.levelList;
            }
        }

        internal override ConfigMetaData Clone()
        {
            return new Chapter();
        }

        internal override int CustomCode()
        {
            return 2057344;
        }

        internal override void Serialize(BinaryWriter bw)
        {

            bw.Write(id);
            bw.Write(name);

        }

        internal override void Deserialize(BinaryReader br)
        {
            id = br.ReadInt32();
            name = br.ReadString();

        }
    }

    public class Level : ConfigMetaData
    {

        internal Int32 id;

        internal String name;

        internal Chapter chapter;

        public Int32 Id
        {
            get
            {
                return this.id;
            }
        }

        public String Name
        {
            get
            {
                return this.name;
            }
        }

        public Chapter Chapter
        {
            get
            {
                return this.chapter;
            }
        }

        internal override ConfigMetaData Clone()
        {
            return new Level();
        }

        internal override int CustomCode()
        {
            return 2057576;
        }

        internal override void Serialize(BinaryWriter bw)
        {

            bw.Write(id);
            bw.Write(name);

        }

        internal override void Deserialize(BinaryReader br)
        {
            id = br.ReadInt32();
            name = br.ReadString();

        }
    }

    public class LevelCondition : ConfigMetaData
    {

        internal Int32 id;

        internal Int32 levelId;

        internal Int32 conditionType;

        internal String conditionValue;

        public Int32 Id
        {
            get
            {
                return this.id;
            }
        }

        public Int32 LevelId
        {
            get
            {
                return this.levelId;
            }
        }

        public Int32 ConditionType
        {
            get
            {
                return this.conditionType;
            }
        }

        public String ConditionValue
        {
            get
            {
                return this.conditionValue;
            }
        }

        internal override ConfigMetaData Clone()
        {
            return new LevelCondition();
        }

        internal override int CustomCode()
        {
            return 2057824;
        }

        internal override void Serialize(BinaryWriter bw)
        {

            bw.Write(id);
            bw.Write(levelId);
            bw.Write(conditionType);
            bw.Write(conditionValue);

        }

        internal override void Deserialize(BinaryReader br)
        {
            id = br.ReadInt32();
            levelId = br.ReadInt32();
            conditionType = br.ReadInt32();
            conditionValue = br.ReadString();

        }
    }

    internal class LoadType
    {

        internal static ConfigMetaData Get(int hc)
        {
            ConfigMetaData cmd = null;
            switch (hc)
            {
                case 2057344:
                    cmd = new Chapter();
                    break;
                case 2057576:
                    cmd = new Level();
                    break;
                case 2057824:
                    cmd = new LevelCondition();
                    break;
            }
            return cmd;

        }

        internal static int GetCode(string name)
        {
            int i = 0;
            switch (name)
            {
                case "Chapter":
                    i = 2057344;
                    break;
                case "Level":
                    i = 2057576;
                    break;
                case "LevelCondition":
                    i = 2057824;
                    break;
            }
            return i;

        }

        internal static void Init()
        {
            foreach (Chapter temp in ConfigManager.GetList<Chapter>())
            {
                ConfigManager.GetList<Level>().FindAll(delegate(Level obj) { return obj.id == temp.id; });
                temp.levelList = ConfigManager.GetList<Level>().FindAll(obj => { return obj.id == temp.id; });
            }
            foreach (Level temp in ConfigManager.GetList<Level>())
            {
                temp.chapter = ConfigManager.GetList<Chapter>().Find(obj => obj.id == temp.id);
            }

        }
    }


    public abstract class ConfigMetaData
    {
        internal abstract int CustomCode();
        internal abstract void Serialize(BinaryWriter bw);
        internal abstract void Deserialize(BinaryReader br);
        internal abstract ConfigMetaData Clone();
    }


    public sealed class ConfigManager
    {
        private static Dictionary<int, IList> allDataList = new Dictionary<int, IList>();


        private static void Add(IList list, int hc)
        {
            if (!allDataList.ContainsKey(hc))
            {
                allDataList.Add(hc, list);
            }
        }


        /// <summary>
        /// 从byte数组加载
        /// </summary>
        /// <param name="dataList"></param>
        public static void Load(byte[] dataList)
        {
            BinaryReader br = new BinaryReader(new MemoryStream(dataList));
            br.ReadString();
            int count = br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int hc = br.ReadInt32();
                ConfigMetaData cmd = LoadType.Get(hc);
                IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(cmd.GetType()));
                int listCount = br.ReadInt32();
                for (int j = 0; j < listCount; j++)
                {
                    cmd = cmd.Clone();
                    cmd.Deserialize(br);
                    list.Add(cmd);
                }
                if (allDataList.ContainsKey(hc))
                {
                    allDataList[hc] = list;
                }
                else
                {
                    allDataList.Add(hc, list);
                }
            }
            br.Close();
            br = null;
            dataList = null;
            LoadType.Init();
            System.GC.Collect();
        }

        /// <summary>
        /// 转成byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] ToBinary()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write("0");
            int count = 0;
            foreach (IList list in allDataList.Values)
            {
                if (list.Count > 0)
                {
                    count++;
                }
            }
            bw.Write(count);
            foreach (IList list in allDataList.Values)
            {
                if (list.Count > 0)
                {
                    bw.Write(((ConfigMetaData)list[0]).CustomCode());
                    bw.Write(list.Count);
                    foreach (ConfigMetaData cmd in list)
                    {
                        cmd.Serialize(bw);
                    }
                }
            }
            byte[] temp = ms.ToArray();
            ms.Close();
            bw.Close();
            ms = null;
            bw = null;
            return temp;
        }

        public static IList GetList(Type t)
        {
            int hc = LoadType.GetCode(t.Name);
            if (allDataList.ContainsKey(hc))
            {
                return allDataList[hc];
            }
            return null;
        }

        public static List<T> GetList<T>() where T : ConfigMetaData
        {

            int hc = LoadType.GetCode(typeof(T).Name);
            if (allDataList.ContainsKey(hc))
            {
                return allDataList[hc] as List<T>;
            }
            return null;
        }

        /// <summary>
        /// 获得集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="callBack">存在次类型集合则回调</param>
        public static void GetList<T>(System.Action<List<T>> callBack) where T : ConfigMetaData
        {
            int hc = LoadType.GetCode(typeof(T).Name);
            if (allDataList.ContainsKey(hc))
            {
                callBack(allDataList[hc] as List<T>);
            }
        }
    }
}
