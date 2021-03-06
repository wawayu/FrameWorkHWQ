//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5420
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetEntityHWQ
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections;
    
    
    public class User : BaseNetHWQ
    {
        
        public String userName;
        
        public String pwd;
        
        public override List<byte> Serialize()
        {
            List<byte> tempList = new List<byte>();
		tempList.AddRange(BitConverter.GetBytes(true));
		s(tempList,userName);
		s(tempList,pwd);
		return tempList;

        }
        
        public override void Deserialize(BinaryReader br)
        {
		userName = d(br);
		pwd = d(br);

        }
        
        public override int CustomCode()
        {
            return 1915504;
        }
    }
    
    public class UserData : BaseNetHWQ
    {
        
        public Int32 userId;
        
        public List<UserBack> backList;
        
        public List<UserEquip> equipList;
        
        public override List<byte> Serialize()
        {
            List<byte> tempList = new List<byte>();
		tempList.AddRange(BitConverter.GetBytes(true));
		tempList.AddRange(BitConverter.GetBytes(userId));
		if(backList == null)
		{
		tempList.AddRange(BitConverter.GetBytes((short)0));
		}
		else
		{
		tempList.AddRange(BitConverter.GetBytes((short)backList.Count));
		foreach (UserBack temp in backList)
		{
			tempList.AddRange(temp.Serialize());
		}
		}
		if(equipList == null)
		{
		tempList.AddRange(BitConverter.GetBytes((short)0));
		}
		else
		{
		tempList.AddRange(BitConverter.GetBytes((short)equipList.Count));
		foreach (UserEquip temp in equipList)
		{
			tempList.AddRange(temp.Serialize());
		}
		}
		return tempList;

        }
        
        public override void Deserialize(BinaryReader br)
        {
		userId = br.ReadInt32();
		int backListCount = br.ReadInt16();
		backList = new List<UserBack>();
		for(int i = 0;i<backListCount;i++)
		{
		if (br.ReadBoolean())
		{
			UserBack  obj = new UserBack();
			obj.Deserialize(br);
			backList.Add(obj);
		}
		}
		int equipListCount = br.ReadInt16();
		equipList = new List<UserEquip>();
		for(int i = 0;i<equipListCount;i++)
		{
		if (br.ReadBoolean())
		{
			UserEquip  obj = new UserEquip();
			obj.Deserialize(br);
			equipList.Add(obj);
		}
		}

        }
        
        public override int CustomCode()
        {
            return 1915736;
        }
    }
    
    public class UserBack : BaseNetHWQ
    {
        
        public Int32 backId;
        
        public Int32 userId;
        
        public Int32 itemId;
        
        public Int32 gridNum;
        
        public Int32 isEquip;
        
        public List<Int32> tt;
        
        public override List<byte> Serialize()
        {
            List<byte> tempList = new List<byte>();
		tempList.AddRange(BitConverter.GetBytes(true));
		tempList.AddRange(BitConverter.GetBytes(backId));
		tempList.AddRange(BitConverter.GetBytes(userId));
		tempList.AddRange(BitConverter.GetBytes(itemId));
		tempList.AddRange(BitConverter.GetBytes(gridNum));
		tempList.AddRange(BitConverter.GetBytes(isEquip));
		if(tt == null)
		{
		tempList.AddRange(BitConverter.GetBytes((short)0));
		}
		else
		{
		tempList.AddRange(BitConverter.GetBytes((short)tt.Count));
		foreach (Int32 temp in tt)
		{
			tempList.AddRange(BitConverter.GetBytes(temp));
		}
		}
		return tempList;

        }
        
        public override void Deserialize(BinaryReader br)
        {
		backId = br.ReadInt32();
		userId = br.ReadInt32();
		itemId = 