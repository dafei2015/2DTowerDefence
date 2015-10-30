using UnityEngine;

/// <summary>
/// 节点类，定义了界面上的各个节点
/// </summary>
public class PathNode : MonoBehaviour
{
    public PathNode currentNode;
    public PathNode nextNode;

    /// <summary>
    /// 设置节点
    /// </summary>
    /// <param name="node">下一个节点</param>
    public void SetNode(PathNode node)
    {
        if (nextNode != null)
        {
            nextNode.currentNode = null;
            nextNode = node;
            currentNode = this;
        }
    }

}
