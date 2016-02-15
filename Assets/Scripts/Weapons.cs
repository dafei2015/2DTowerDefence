using System.Collections;
using UnityEngine;

public class Weapons:MonoBehaviour
{
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float attackRange = 0.6f;

    /// <summary>
    /// 当前目标敌人
    /// </summary>
    public EnemyNode mTrg;

    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float attackTime = 2.5f;

    /// <summary>
    /// 当前剩余冷却时间
    /// </summary>
    public float mTime = 0;

    /// <summary>
    /// 子弹模型
    /// </summary>
    public GameObject bullet;
    void Start()
    {
        mTime = attackTime;
    }
    void Update()
    {
        FindEnemy();
        AimedAtEnemy();
        Attack();
    }

    #region 查找对准敌人

    /// <summary>
    /// 查找敌人
    /// </summary>
    private void FindEnemy()
    {
        ArrayList list = GameMgr.instance.GetEnemyList();
        mTrg = null;
        foreach (EnemyNode enemy in list)
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = enemy.transform.position;

            float dis = Vector2.Distance(new Vector2(pos1.x, pos1.y), new Vector2(pos2.x, pos2.y));

            if (dis > attackRange)
            {
//                Debug.Log(string.Format("敌人{0} 未进入攻击范围，距离：{1}", enemy.name, dis));
            }
            else
            {
                if (enemy != mTrg) mTrg = enemy;
//                Debug.LogError(string.Format("敌人{0} 进入攻击范围，距离：{1}", enemy.name, dis));
            }
        }
    }

    /// <summary>
    /// 对准敌人
    /// </summary>
    private void AimedAtEnemy()
    {
        if (mTrg == null) return;
        Vector3 pos1 = transform.position;
        Vector3 pos2 = mTrg.transform.position;

        Vector3 dir = (pos2 - pos1).normalized;
        //通过两点之间的法向量和向上的位置设置角度
        float angle = GetAngle(Vector3.up, dir);
        //设置旋转角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// 获取欧垃角
    /// </summary>
    /// <param name="v1">第一个向量坐标</param>
    /// <param name="v2">第二个坐标</param>
    private float GetAngle(Vector3 v1, Vector3 v2)
    {
        float dot = Vector3.Dot(v1, v2);
        float mv1 = Mathf.Sqrt(v1.x*v1.x + v1.y*v1.y);
        float mv2 = Mathf.Sqrt(v2.x*v2.x + v2.y*v2.y);

        float angle = Mathf.Acos(dot/mv1*mv2)*Mathf.Rad2Deg;
        //逆时针为正，顺时针为负
        if (v2.x > v1.x) angle *= -1;
        return angle;
    }

    #endregion

    /// <summary>
    /// 攻击
    /// </summary>
    void Attack()
    {
        mTime -= Time.deltaTime;
        //冷却完毕执行攻击操作
        if(mTime<=0)
        {
            if (mTrg == null) return;
            if(bullet!=null)
            {
                GameObject item = Instantiate(bullet) as GameObject;
                BulletSelf self = item.AddComponent<BulletSelf>();
                item.transform.position = bullet.transform.position;
                item.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.localEulerAngles.z + 90);
                self.InitData(mTrg);
            }
            mTime = attackTime;
        }
    }
    //绘制攻击范围
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}