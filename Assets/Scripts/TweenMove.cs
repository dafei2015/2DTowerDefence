#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			TweenMove.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-15 13:54:20Z
//
// 描述(Description):			TweenMove	移动补间动画			
//
// **********************************************************************

#endregion

#region

using System;
using UnityEngine;

#endregion

[AddComponentMenu("NGUI/Tween/Tween Move")]
public class TweenMove : UITweener
{
    public Vector3 from;
    public Transform to;

    protected override void OnUpdate(float factor, bool isFinished)
    {
        transform.position = from * (1 - factor) + to.position * factor;
    }
}