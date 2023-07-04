using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class UniTaskController : MonoBehaviour
{
    /// <summary>
    /// UniTask�̃L�����Z�����@
    /// </summary>
    protected enum UniTaskCancellMode
    {
        Auto,         //����
        Manual        //�蓮
    }

    protected UnityEvent UniTaskExecute;
    protected CancellationTokenSource cts;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    protected void Awake()
    {
        UniTaskExecute = new UnityEvent();
        cts = new CancellationTokenSource();
    }

    /// <summary>
    /// UniTask��p���ēƎ��̃t���[�����[�N�����֐��i�}���`�X���b�h�ł̎��s���\�j
    /// </summary>
    /// <param name="start">���̊֐����Ă΂ꂽ�u�ԌĂяo���֐�</param>
    /// <param name="update">���t���[���Ăяo���֐�</param>
    /// <param name="unlockUpdate">���̃��[�v�𔲂��o���o������</param>
    /// <param name="token">�L�����Z���g�[�N��</param>
    /// <param name="mode">UniTask�̃L�����Z�����@</param>
    /// <returns></returns>
    protected async UniTask UniTaskUpdate(UnityAction start, UnityAction update, Func<bool> unlockFunc, CancellationToken token, UniTaskCancellMode mode)
    {
        //�L�����Z���g�[�N�����Z�b�g
        SetCancellToken(ref token, mode);

        //start�Ŏ󂯓n���ꂽ�֐������s
        UniTaskExecute?.AddListener(start);
        UniTaskExecute?.Invoke();
        //�֐������Z�b�g
        UniTaskExecute?.RemoveAllListeners();
        //�����œn���ꂽupdate�����s
        UniTaskExecute?.AddListener(update);

        while (true)
        {
            //UniTaskExecute��null�o�Ȃ���Ύ��s����
            UniTaskExecute?.Invoke();

            if(unlockFunc.Invoke() && unlockFunc != null)
            {
                Debug.Log("�����o��");
                //�֐������Z�b�g
                UniTaskExecute?.RemoveAllListeners();
                //Unitask�������I�ɒ��~
                CancelUniTask();
                break;
            }

            //�P�t���[���҂�
            await UniTask.DelayFrame(1, PlayerLoopTiming.Update);
        }
    }

    /// <summary>
    /// �L�����Z���g�[�N���ɃC���X�^���X���L�����Z�����@�ɉ����Đݒ�
    /// </summary>
    /// <param name="token"></param>
    /// <param name="mode"></param>
    private void SetCancellToken(ref CancellationToken token, UniTaskCancellMode mode)
    {
        switch(mode)
        {
            case UniTaskCancellMode.Auto:
                token = this.GetCancellationTokenOnDestroy();
                break;
            case UniTaskCancellMode.Manual:
                token = cts.Token;
                break;
        }
    }

    /// <summary>
    /// Unitask�������I�ɃL�����Z������֐�
    /// </summary>
    protected void CancelUniTask()
    {
        cts.Cancel();
    }

    /// <summary>
    /// ���z�֐�(Update)
    /// </summary>
    public virtual void UpdateUniTask(){ }

    /// <summary>
    /// ���z�֐�(Start)
    /// </summary>
    public virtual void StartUniTask() { }
}
