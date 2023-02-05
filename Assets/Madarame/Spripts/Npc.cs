using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] Transform player;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LookAt(player.position);
        }
    }
    public void LookAt(Vector3 playerPos)
    {
        transform.LookAt(playerPos);
    }
}
