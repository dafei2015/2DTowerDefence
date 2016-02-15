using UnityEngine;

public delegate void DeathCallBack(EnemyNode node);

public class EnemyNode:MonoBehaviour
{
    /// <summary>
    /// 怪物死亡回调
    /// </summary>
    public DeathCallBack OnDeathCallBack;
    /// <summary>
    /// 开始节点
    /// </summary>
    public PathNode startNode;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 0.15f;

    /// <summary>
    /// 怪物的当前血量
    /// </summary>
    public float Hp =100;

    public float MaxHp = 100.0f;
    /// <summary>
    /// 显示血量的Label
    /// </summary>
    public UISlider HpSlider;

  
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = startNode.transform.position;

        float dis = Vector2.Distance(new Vector2(pos1.x, pos1.y), new Vector2(pos2.x, pos2.y));
        if (dis < 0.1f)
        {
            if (startNode.nextNode == null)
            {
                Debug.Log("到达终点");
                Destroy(gameObject);
            }
            else
            {
                startNode = startNode.nextNode;
            }
        }

        //获得移动的方向向量
        Vector3 dir = new Vector3(pos2.x - pos1.x, pos2.y - pos1.y, 0).normalized;
        //移动
        transform.Translate(dir*moveSpeed*Time.deltaTime);
    }

    /// <summary>
    /// 开始受攻击
    /// </summary>
    /// <param name="hp">耗血</param>
    public void OnBeginAttacked(float hp)
    {
        if(this.Hp>0)
        {
            this.Hp -= hp;
        }
        if(this.HpSlider!= null)
        {
            this.HpSlider.value = this.Hp/MaxHp;
        }
        if(this.Hp<=0)
        {
            OnDeathCallBack(this);
        }
    }
}

