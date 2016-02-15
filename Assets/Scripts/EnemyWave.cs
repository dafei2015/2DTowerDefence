#region 描述

// **********************************************************************
// 
// 文件名(File Name)：			EnemyWave.cs
// 
// 作者(Author)：				da_fei
// 
// 创建时间(CreateTime):			2016-02-15 16:42:53Z
//
// 描述(Description):			EnemyWave 怪物波数				
//
// **********************************************************************

#endregion

#region

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyModel;
    public PathNode startPathNode;

    private int mWaveIndex = 1;
    private int mWaveNums = 10;
    private List<WaveData> mWaveList;

    private int mRemainingEnemyNums = 0;
    void Start()
    {
        AddTestWave();
        StartCoroutine(LoadWaveEnemy());
    }

    EnemyNode CreateEnemy()
    {
        GameObject model = Instantiate(enemyModel) as GameObject;
        model.transform.parent = transform;
        model.transform.localEulerAngles = Vector3.zero;
        model.transform.localScale = Vector3.one;
        model.transform.position = startPathNode.transform.position; //使用世界坐标直接指定位置

        EnemyNode enemy = model.GetComponent<EnemyNode>();
        enemy.startNode = startPathNode;
        enemy.OnDeathCallBack = OnDeathCallBack;
        GameMgr.instance.enemyList.Add(enemy);
        return enemy;
    }

    /// <summary>
    /// 死亡回调
    /// </summary>
    /// <param name="node"></param>
    private void OnDeathCallBack(EnemyNode node)
    {
        Destroy(node.gameObject);
        GameMgr.instance.enemyList.Remove(node);
        mRemainingEnemyNums--;
        if(GameMgr.instance.enemyList.Count<=0)
        {
            mWaveIndex++;
            if(mWaveIndex>mWaveList.Count)
            {
                //TODO: 怪物死光游戏结束
            }
            else
            {
                StartCoroutine(LoadWaveEnemy());                
            }
        }
    }

    IEnumerator LoadWaveEnemy()
    {
        WaveData wave = mWaveList[mWaveIndex - 1];
        mRemainingEnemyNums = wave.nums;
        yield return new WaitForSeconds(wave.cd);
        for (int i = 0; i < wave.nums; i++)
        {
            EnemyNode enemy = CreateEnemy();
            int enemyID = wave.randomId[Random.Range(0,wave.randomId.Length-1)];
            enemy.Hp = enemyID;
            enemy.moveSpeed = Random.Range(0.15f, 0.3f);
            yield return new WaitForSeconds(wave.delay);
        }
    }

    void AddTestWave()
    {
        mWaveList = new List<WaveData>();
        for (int i = 0; i < mWaveNums; i++)
        {
            WaveData wave = new WaveData(Random.Range(2, 5), Random.Range(0.5f, 1.5f), Random.Range(1, 5));
            mWaveList.Add(wave);
        }
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