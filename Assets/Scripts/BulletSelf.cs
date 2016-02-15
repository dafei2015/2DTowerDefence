#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			BulletSelf.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-15 13:49:03Z
//
// 描述(Description):			BulletSelf  子弹脚本				
//
// **********************************************************************

#endregion

using UnityEngine;

public class BulletSelf:MonoBehaviour
{
    private EnemyNode enemy;

    public void InitData(EnemyNode enemy)
    {
        this.enemy = enemy;
        TweenMove move = gameObject.AddComponent<TweenMove>();
        move.from = transform.position;
        move.to = enemy.transform;
        move.duration = 0.5f;
        move.PlayForward();

        mSprite = GetComponent<UISprite>();
    }

    #region 爆炸效果

    /// <summary>
    /// 播放帧数
    /// </summary>
    public int playFPS = 10;
    /// <summary>
    /// 当前播放时间
    /// </summary>
    private float mTime = 0;
    /// <summary>
    /// 当前播放位置索引
    /// </summary>
    private int mPlayIndex = 0;

    private string[] sprites = { "TBottle-hd.pvr_10", "TBottle-hd.pvr_20" };
    private UISprite mSprite;

    private bool isPlayBoom = false;

    /// <summary>
    /// 播放爆炸
    /// </summary>
    void PlayBoom()
    {
        if (!isPlayBoom) return;
        mTime += Time.deltaTime;
        if (mTime > 1.0f / playFPS)
        {
            mPlayIndex++;
            mTime = 0;
            if (mPlayIndex >= sprites.Length)
            {
                mPlayIndex = 0;
                enemy.OnBeginAttacked(30);
                Destroy(gameObject);
            }

            mSprite.spriteName = sprites[mPlayIndex];
            mSprite.MakePixelPerfect();
        }
    }

    void Update()
    {
        PlayBoom();
    }


    #endregion

    public void OnTriggerEnter(Collider other)
    {
        isPlayBoom = true;
    }
}