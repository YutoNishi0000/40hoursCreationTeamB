using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(destroyGirl());
    }
    IEnumerator destroyGirl()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
