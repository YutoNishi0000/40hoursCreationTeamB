using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoShop : Human
{
    private GameObject target;
    private int count;

    private void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            count++;

            if(count % 2 == 0)
            {
                return;
            }

            Debug.Log("ターゲットご来店");
            other.gameObject.SetActive(false);
            StartCoroutine(nameof(CShop), other.gameObject);
        }
    }

    IEnumerator CShop(GameObject obj)
    { 
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
    }
}
