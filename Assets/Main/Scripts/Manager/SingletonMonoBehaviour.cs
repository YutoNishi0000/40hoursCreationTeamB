using UnityEngine;

/// <summary>
/// MonoBehaviourに対応したシングルトンクラス
/// （例）public class GameManager : SingletonMonoBehaviour<GameManager>
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
                    Debug.LogError(typeof(T) + "をアタッチしているGameObjectが存在しない");
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる
        if (this != Instance)
        {
            // アタッチされている場合は破棄する
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
