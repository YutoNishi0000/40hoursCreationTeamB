using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeTarget : MonoBehaviour
{
    private FailedNegotiationEvent _fail;
    private NavMeshAgent _navmesh;
    public GameObject _escapePoint;
    private Animator _animator;
    private bool once;

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        _animator = GetComponent<Animator>();
        _navmesh = GetComponent<NavMeshAgent>();
        _fail = GameObject.Find("NegotiationEvent").GetComponent<FailedNegotiationEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FailedNegotiationEvent._escaprFromPlayer)
        {
            //ここに逃げる処理を追加
            _navmesh.destination = _escapePoint.transform.position;
            _animator.SetFloat("Speed", _navmesh.velocity.magnitude);
            Debug.Log("ターゲットがミゲルはず");

            if (!once)
            {
                Invoke(nameof(GoToGameOver), 2);
                once = true;
            }
        }
    }

    void GoToGameOver()
    {
        GameManager.Instance.GameOver();
    }
}
