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

    protected CancellationTokenSource cts;

    /// <summary>
    /// UniTask��p���ēƎ��̃t���[�����[�N�����֐�
    /// </summary>
    /// <param name="start">���̊֐����Ă΂ꂽ�u�ԌĂяo���֐�</param>
    /// <param name="update">���t���[���Ăяo���֐�</param>
    /// <param name="beforeBreak">���̃��[�v�𔲂��o�����O�Ɏ��s�������֐�</param>
    /// <param name="unlockFunc">���̃��[�v�𔲂��o���o������</param>
    /// <param name="token">�L�����Z���g�[�N��</param>
    /// <param name="mode">UniTask�̃L�����Z�����@</param>
    /// <returns></returns>
    protected async UniTask UniTaskUpdate(UnityAction start, UnityAction update, UnityAction beforeBreak, Func<bool> unlockFunc, CancellationToken token, UniTaskCancellMode mode)
    {
        //�L�����Z���g�[�N�����Z�b�g
        SetCancellToken(ref token, mode);

        start?.Invoke();

        while (true)
        {
            //UniTaskExecute��null�Ȃ���Ύ��s����
            update?.Invoke();

            //�������A���̃��[�v�𔲂��o�������𖞂����Ă���̂Ȃ�
            if(unlockFunc.Invoke() && unlockFunc != null)
            {
                //breforeBreak�ɓn���ꂽ�֐������s
                beforeBreak?.Invoke();
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
        //cts�ɐV�����C���X�^���X������
        cts = new CancellationTokenSource();

        switch (mode)
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
