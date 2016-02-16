#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			DataMgr.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-16 16:28:42Z
//
// 描述(Description):			DataMgr 数据管理				
//
// **********************************************************************

#endregion

using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DataMgr:MonoBehaviour
{
    public TextAsset data;

    public static DataMgr instance;

    //怪物波数控制列表
    public  List<WaveData> WaveList = new List<WaveData>();

    //禁用格子列表
    public List<int> disableList = new List<int>(); 
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void ReadXml()
    {
        XmlDocument document = new XmlDocument();
        document.LoadXml(data.text);

        //获取xml的根节点
        XmlElement root = document.DocumentElement;

        //获取xml的属性
        foreach (XmlAttribute attribute in root.Attributes)
        {

        }

        //获取xml的子节点
        XmlNodeList nodes = document.GetElementsByTagName("Monster");

        foreach (XmlNode node in nodes)
        {
            WaveData waveData = new WaveData();
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name.Equals("Monster"))
                {
                    string[] arr = attribute.Value.Split(':');
                    int[] ids = new int[arr.Length];

                    for (int i = 0; i < arr.Length; i++)
                    {
                        ids[i] = int.Parse(arr[i]);
                    }
                    waveData.randomId = ids;
                }
                else if (attribute.Name.Equals("Nums"))
                {
                    waveData.nums = int.Parse(attribute.Value);
                }
                else if (attribute.Name.Equals("CD"))
                {
                    waveData.cd = int.Parse(attribute.Value);
                }
                else if (attribute.Name.Equals("Time"))
                {
                    waveData.delay = int.Parse(attribute.Value);
                }
            }
            WaveList.Add(waveData);
        }

        XmlNodeList grids = document.GetElementsByTagName("Grid");
        foreach (XmlNode node in grids)
        {
            WaveData waveData = new WaveData();
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name.Equals("id"))
                {
                   disableList.Add(int.Parse(attribute.Value));
                }
               
            }
            
        }
    }

    /// <summary>
    /// 格子是否在禁用列表中
    /// </summary>
    public bool ContainsdDisable(int id)
    {
        return disableList.Contains(id);
    }
}

public class WaveData
{
    /// <summary>
    /// 怪物刷新CD
    /// </summary>
    public int cd = 5;

    /// <summary>
    /// 怪物间隔
    /// </summary>
    public float delay = 1.5f;

    /// <summary>
    /// 怪物数量
    /// </summary>
    public int nums = 0;

    /// <summary>
    /// 随机ID
    /// </summary>
    public int[] randomId = { 20, 30, 50, 70 };

    public WaveData(int cd, float delay, int nums)
    {
        this.cd = cd;
        this.delay = delay;
        this.nums = nums;
    }


    public WaveData()
    {
    }
}