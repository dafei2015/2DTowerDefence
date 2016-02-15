using System.Collections;
using UnityEngine;

public class GameMgr:MonoBehaviour
{
    public static GameMgr instance;

    /// <summary>
    /// 怪物列表
    /// </summary>
    public ArrayList enemyList = new ArrayList();

    /// <summary>
    /// 获取敌人列表
    /// </summary>
    /// <returns></returns>
    public ArrayList GetEnemyList()
    {
        ArrayList list = new ArrayList();
        list.AddRange(enemyList);
        return list;
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
//        EnemyNode[] arr = gameObject.GetComponentsInChildren<EnemyNode>();
//        foreach (EnemyNode e in arr)
//        {
//            enemyList.Add(e);
//        }
    }
}