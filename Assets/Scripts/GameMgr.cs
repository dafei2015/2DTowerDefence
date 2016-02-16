using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr:MonoBehaviour
{
    public static GameMgr instance;

    public EnemyWave enemyWave;
    public GridMap gridMap;
    public GameObject defensModel;


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
         //TODO:读取数据
//        DataMgr.instance.ReadXml();
        if (gridMap != null)
        {
            List<GridNode> gridNodeList = gridMap.CreateGrid();
            foreach (GridNode node in gridNodeList)
            {
                node.Init();
                UIEventListener listener = UIEventListener.Get(node.gameObject);
                listener.onClick = ClickGrid;
            }
        }
        if(enemyWave!=null )
        {
            enemyWave.Init();
        }
        
    }

    void ClickGrid(GameObject click)
    {
        GridNode node = click.GetComponent<GridNode>();
       
            if (node.gridType == GridNode.GridType.available)
            {
                GameObject go = Instantiate(defensModel) as GameObject;
                go.transform.parent = click.transform;
                go.transform.localEulerAngles = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                node.gridType = GridNode.GridType.disable;
            }
    }
}