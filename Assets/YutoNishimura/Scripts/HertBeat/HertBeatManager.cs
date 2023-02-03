using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HertBeatManager : MonoBehaviour
{
    //�傫���Ȃ�X�s�[�h
    private float largeSpeed = 0.01f;
    //�������Ȃ�X�s�[�h
    private float smallSpeed = 0.01f;
    //�~�܂鎞��
    private float stopTime = 0.01f;
    //�~�܂鎞�Ԃ̌o�ߎ���
    private float time;
    //�ŏ��̑傫��
    private Vector3 minSize = new Vector3(0.4f, 0.4f, 0.4f);
    //�ő�̑傫��
    private Vector3 maxSize = new Vector3(0.7f, 0.7f, 0.7f);
    private enum STATE
    {
        large,
        stop,
        small,
    }
    private STATE state;

    /// <summary>
    /// UI�𔏓�������
    /// </summary>
    public void BeatUpdate()
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
    /// <summary>
    /// �����𑁂�����
    /// </summary>
    public void FastBeat()
    {
        largeSpeed = 0.005f + ((RayTest.lockonTime / 2.0f) / 1000);
        smallSpeed = 0.005f + ((RayTest.lockonTime / 2.0f) / 1000);
        stopTime = 0.1f - (((RayTest.lockonTime * 5.0f) / 3.0f) / 100);
    }
}
