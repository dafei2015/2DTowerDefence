using UnityEngine;

public class PathRoot:MonoBehaviour
{
    private GameObject[] mPaths;

    [ContextMenu("显示线路")]
    public void ShowPath()
    {
        mPaths = GameObject.FindGameObjectsWithTag("PathNode");
    }

    /// <summary>
    /// 绘制图线
    /// </summary>
    public void OnDrawGizmos()
    {
        if (mPaths == null) return;

        //设置Gizmos的颜色
        Gizmos.color = Color.red;
        foreach (GameObject go in mPaths)
        {
            PathNode node = go.GetComponent<PathNode>();
            if (node == null) return;

            if (node.nextNode != null)
            {
                Gizmos.DrawLine(node.transform.position, node.nextNode.transform.position);                
            }
            Gizmos.DrawCube(node.transform.position,new Vector3(0.1f,0.1f,0.1f));
        }
    }

}