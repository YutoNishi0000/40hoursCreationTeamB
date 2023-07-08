using UnityEngine;


public class TitleController : UIController
{
    private bool oneTime;
    private void Start()
    {
        oneTime = false;
    }
    void Update()
    {
        if (Input.anyKey && !oneTime)
        {
            //MoveScene(HomeSceneName);
            InstantAnimation();
            PlayDecisionSE();
            HomeScene();
            oneTime = true;
        }
        MoveScene(GameManager.Instance.sceneIndex);
    }
}
