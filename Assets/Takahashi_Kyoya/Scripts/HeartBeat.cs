using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    //�傫���Ȃ�X�s�[�h
    [SerializeField] private float largeSpeed;
    //�������Ȃ�X�s�[�h
    [SerializeField] private float smallSpeed;
    //�~�܂鎞��
    [SerializeField] private float stopTime;
    //�~�܂鎞�Ԃ̌o�ߎ���
    private float time;
    //�ŏ��̑傫��
    private Vector3 minSize = new Vector3(1.0f, 1.0f, 1.0f);
    //�ő�̑傫��
    private Vector3 maxSize = new Vector3(2.0f, 2.0f, 2.0f);
    private enum STATE
    {
        large,
        stop,
        small,
    }
    private STATE state;

    private void Update()
    {
        BeatUpdate();
    }

    /// <summary>
    /// UI�𔏓�������
    /// </summary>
    void BeatUpdate()
    {
        //�傫���Ȃ��ď����~�܂��ď������Ȃ�
        switch (state)
        {
            case STATE.large:
                LargeUpdate();
                break;
            case STATE.stop:
                StopUpdate();
                break;
            case STATE.small:
                SmallUpdate();
                break;
        }
    }

    /// <summary>
    /// �傫������
    /// </summary>
    void LargeUpdate()
    {
        this.transform.localScale += new Vector3(largeSpeed, largeSpeed, largeSpeed);
        if (this.transform.localScale.x > maxSize.x)
        {
            state = STATE.stop;
            time = Time.time;
        }
    }
    /// <summary>
    /// �T�C�Y�ύX���~�߂�
    /// </summary>
    void StopUpdate()
    {
        if (Time.time - time > stopTime)
        {
            state = STATE.small;
        }
    }
    /// <summary>
    /// ����������
    /// </summary>
    void SmallUpdate()
    {
        this.transform.localScale -= new Vector3(smallSpeed, smallSpeed, smallSpeed);
        if (this.transform.localScale.x < minSize.x)
        {
            state = STATE.large;
        }
    }

}
