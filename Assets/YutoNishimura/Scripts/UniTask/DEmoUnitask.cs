using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DEmoUnitask : UniTaskController
{
    private UniTaskController unitask;
    private CancellationToken token;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        //unitask = new UniTaskController();
        //UniTaskUpdate(E, () => R(), () => { return (t >= 1); }, token,  UniTaskController.UniTaskCancellMode.Auto).Forget();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Updte‚ÅŒÄ‚Î‚ê‚Ä‚Ü‚·");
        //Debug.Log(t);
    }

    public void E()
    {
        Debug.Log("Start");
    }

    public void R()
    {
        t += Time.deltaTime;
        Debug.Log("Update");
    }
}
