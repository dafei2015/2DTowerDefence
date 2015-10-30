using System.Collections;
using UnityEngine;

public class GameMgr:MonoBehaviour
{
    public static GameMgr instance;

    /// <summary>
    /// 怪物列表
    /// </summary>
    public ArrayList enemyList = new ArrayList();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EnemyNode[] arr = gameObject.GetComponentsInChildren<EnemyNode>();
        foreach (EnemyNode e in arr)
        {
            enemyList.Add(e);
        }
    }
}