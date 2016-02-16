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
using UnityEngine;

#endregion

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyModel;
    private int mRemainingEnemyNums;
    private int mWaveIndex = 1;
    private int mWaveNums = 10;
    public PathNode startPathNode;

    public  void Init()
    {
       
//        AddTestWave();
        StartCoroutine(LoadWaveEnemy());
    }

    /// <summary>
    ///     创建敌人
    /// </summary>
    /// <returns></returns>
    private EnemyNode CreateEnemy()
    {
        GameObject model = Instantiate(enemyModel);
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
    ///     死亡回调
    /// </summary>
    /// <param name="node"></param>
    private void OnDeathCallBack(EnemyNode node)
    {
        Destroy(node.gameObject);
        GameMgr.instance.enemyList.Remove(node);
        mRemainingEnemyNums--;
        if (GameMgr.instance.enemyList.Count <= 0)
        {
            mWaveIndex++;
            if (mWaveIndex > DataMgr.instance.WaveList.Count)
            {
                //TODO: 怪物死光游戏结束
            }
            else
            {
                StartCoroutine(LoadWaveEnemy());
            }
        }
    }

    /// <summary>
    ///     加载数据
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadWaveEnemy()
    {
        WaveData wave = DataMgr.instance.WaveList[mWaveIndex - 1];
        mRemainingEnemyNums = wave.nums;
        yield return new WaitForSeconds(wave.cd);
        for (int i = 0; i < wave.nums; i++)
        {
            EnemyNode enemy = CreateEnemy();
            int enemyID = wave.randomId[Random.Range(0, wave.randomId.Length - 1)];
            enemy.Hp = enemyID;
            enemy.moveSpeed = Random.Range(0.15f, 0.3f);
            yield return new WaitForSeconds(wave.delay);
        }
    }
}