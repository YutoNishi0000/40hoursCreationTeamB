using UnityEngine;

/// <summary>
/// MonoBehaviour�ɑΉ������V���O���g���N���X
/// �i��jpublic class GameManager : SingletonMonoBehaviour<GameManager>
/// </summary>
public abstract class SingletonMonoBehaviour<T> : UniTaskController where T : UniTaskController
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "���A�^�b�`���Ă���GameObject�����݂��Ȃ�");
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        // ����GameObject�ɃA�^�b�`����Ă��邩���ׂ�
        if (this != Instance)
        {
            // �A�^�b�`����Ă���ꍇ�͔j������
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
