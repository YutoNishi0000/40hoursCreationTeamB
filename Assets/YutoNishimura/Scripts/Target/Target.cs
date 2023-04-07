using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : Actor
{
    //public Transform[] points;
    public GameObject[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject targetCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //目標地点の間を継続的に移動
        agent.autoBraking = false;
    }
    private void OnEnable()
    {
        targetCamera = GameObject.FindWithTag("subCamera");
        points = GameObject.FindGameObjectsWithTag("dest");
    }
    // Update is called once per frame
    void Update()
    {
        targetCamera.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + 1.0f,
            this.transform.position.z);
        targetCamera.transform.eulerAngles = this.transform.eulerAngles;
        //エージェントが現目標地点に近づいたら次の目標地点を設定
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        //地点が何も設定されていない場合
        if(points.Length == 0)
        {
            return;
        }

        //エージェントが現在設定された目標地点に行くように設定
        agent.destination = points[destPoint].transform.position;

        //配列内の次の位置を目標地点に設定し、必要ならば出発地点に戻る
        destPoint = (destPoint + 1) % points.Length;
    }
}
