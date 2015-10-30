using UnityEngine;

public class EnemyNode:MonoBehaviour
{
    /// <summary>
    /// 开始节点
    /// </summary>
    public PathNode startNode;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 0.15f;

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
}