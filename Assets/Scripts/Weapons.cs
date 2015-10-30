using System.Collections;
using UnityEngine;

public class Weapons:MonoBehaviour
{
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float attackRange = 0.6f;

    void Update()
    {
        FindEnemy();
    }
    /// <summary>
    /// 查找敌人
    /// </summary>
    void FindEnemy()
    {
        ArrayList list = GameMgr.instance.enemyList;

        foreach (EnemyNode enemy in list)
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = enemy.transform.position;

            float dis = Vector2.Distance(new Vector2(pos1.x, pos1.y), new Vector2(pos2.x, pos2.y));

            if (dis > attackRange)
            {
                Debug.Log(string.Format("敌人{0} 未进入攻击范围，距离：{1}",enemy.name,dis));
            }
            else
            {
                Debug.LogError(string.Format("敌人{0} 未进入攻击范围，距离：{1}", enemy.name, dis));
            }
        }
    }
}