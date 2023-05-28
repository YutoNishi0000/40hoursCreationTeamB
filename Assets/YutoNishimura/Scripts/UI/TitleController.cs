using UnityEngine;


public class TitleController : UIController
{
    private void Start()
    {
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //MoveScene(HomeSceneName);
            InstantAnimation();
            PlayDecisionSE();
            HomeScene();
        }

        MoveScene(GameManager.Instance.sceneIndex);
    }
}
