#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			GridMap.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-16 10:18:16Z
//
// 描述(Description):			GridMap 格子地图				
//
// **********************************************************************

#endregion

using System.Collections.Generic;
using UnityEngine;

public class GridMap:MonoBehaviour
{
    public int index_x =12;
    public int index_y = 6;
    public int size_x = 65;
    public int size_y = 80;

    /// <summary>
    /// 创建格子地图
    /// </summary>
    public List<GridNode> CreateGrid()
    {
        List<GridNode> list = new List<GridNode>();
        for (int i = 0; i < index_x; i++)
        {
            for (int j = 0; j < index_y; j++)
            {
                GameObject obj = new GameObject((80 * i + j).ToString());
                obj.transform.parent = transform;
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = new Vector3(60 + -512.0f + i * size_x, 50 + -256 + j * size_y, 0);
                GridNode node = obj.AddComponent<GridNode>();
                list.Add(node);
                
            }
        }
        return list;
    }

  
    [ContextMenu("清除格子")]
    public void ClearGrid()
    {
        int count = transform.childCount;
        
        for (int i = 0; i < transform.childCount; )
        {
            GameObject go = transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
    }
}