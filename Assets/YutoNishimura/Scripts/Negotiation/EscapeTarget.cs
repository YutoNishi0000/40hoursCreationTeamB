using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeTarget : MonoBehaviour
{
    private FailedNegotiationEvent _fail;
    private NavMeshAgent _navmesh;
    public GameObject _escapePoint;

    // Start is called before the first frame update
    void Start()
    {
        _navmesh = GetComponent<NavMeshAgent>();
        _fail = GameObject.Find("NegotiationEvent").GetComponent<FailedNegotiationEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FailedNegotiationEvent._escaprFromPlayer)
        {
            //�����ɓ����鏈����ǉ�
            GameManager.Instance.GameOver();
            Debug.Log("�^�[�Q�b�g���~�Q���͂�");
        }
    }
}
