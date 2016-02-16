#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			GridNode.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-16 10:26:37Z
//
// 描述(Description):			GridNode 格子节点				
//
// **********************************************************************

#endregion

#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class GridNode : MonoBehaviour
{
    public int id;
    /// <summary>
    /// 绘制方块,宽度是640 所以除以320 格子大小80
    /// </summary>
    public void OnDrawGizmos()
    {
        if(gridType == GridType.available)
        {
            Gizmos.color = Color.blue;
            
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(120.0f /512.0f, 80.0f / 320.0f, 0));
    }

    public GridType gridType = GridType.available;
    public enum GridType
    {
        /// <summary>
        ///     可用
        /// </summary>
        available,

        /// <summary>
        ///     禁用
        /// </summary>
        disable
    }

    public void Init()
    {
        UIWidget widget = gameObject.AddComponent<UIWidget>();//是普通物体显示在NGUI层上
        widget.depth = 20;
        widget.width = 65;
        widget.height = 80;
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(65, 80, 0);
       

        if(DataMgr.instance.ContainsdDisable(int.Parse(gameObject.name)))
        {
            gridType = GridType.disable;
        }
    }

    void Start()
    {
        id = int.Parse(gameObject.name);
    }
   
}