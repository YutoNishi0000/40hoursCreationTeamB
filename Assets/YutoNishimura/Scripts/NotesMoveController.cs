using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotesMoveController : MonoBehaviour
{
    private GetMouseInputController _mouse;
    //[SerializeField] private Image _decision;
    [SerializeField] RectTransform TargetTransform;
    [SerializeField] Image Render;
    RectTransform Transform;

    // Start is called before the first frame update
    void Start()
    {
        _mouse = FindObjectOfType<GetMouseInputController>();
        Transform = Render.GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position += new Vector3(0, -0.1f, 0);

        if(CheckOverlap(Transform, TargetTransform) && _mouse.GetMouseAction())
        {
            Destroy(gameObject);
        }
    }

    bool CheckOverlap(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }

    private void FixedUpdate()
    {
    }
}
