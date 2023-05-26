using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    Vector3 moveDirection = Vector3.zero;
    Vector3 targetDirection;        //移動する方向のベクトル
    private CharacterController controller;
    [SerializeField] private float speed = 5f;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;
    public bool _moveLock;                           //行動を制限するかどうか

    private float jumpSpeed = 5f;
    private float gravity = 20f;

    //変数の宣言(角度の制限用)
    float minX = -40f, maxX = 40f;
    Vector3 initialCamPos;

    private float initialPlayerSpeed;   //ゲーム開始時のプレイヤーのデフォルトスピード

    private void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        initialCamPos = cam.transform.localPosition;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        initialPlayerSpeed = speed;
        Shake(3, 1);
    }

    private void Update()
    {
        if (!GameManager.Instance.StartGame)
        {
            return;
        }

        RotateControl();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.StartGame)
        {
            return;
        }

        MoveControl();
    }

    public override void MoveControl()
    {
        //キーボード入力を取得
        float v = Input.GetAxisRaw("Vertical");         //InputManagerの↑↓の入力       
        float h = Input.GetAxisRaw("Horizontal");       //InputManagerの←→の入力 

        //カメラの正面方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //カメラの右方向を取得

        //カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = h * right + v * forward;

        AnimatioControl(v, h);

        //地上にいる場合の処理
        if (controller.isGrounded)
        {
            Debug.Log("地面についています");
            //移動のベクトルを計算
            moveDirection = targetDirection * speed;

            //Jumpボタンでジャンプ処理
            if (Input.GetButton("Jump"))
            {
                Debug.Log("ジャンプ");
                moveDirection.y = jumpSpeed;
            }
        }
        else        //空中操作の処理（重力加速度等）
        {
            float tempy = moveDirection.y;
            //(↓の２文の処理があると空中でも入力方向に動けるようになる)
            //moveDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
            //moveDirection *= speed;
            moveDirection.y = tempy - gravity * Time.deltaTime;
        }

        //最終的な移動処理
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void AnimatioControl(float vertical, float horizon)
    {
        if (vertical > .1 || vertical < -.1 || horizon > .1 || horizon < -.1) //(移動入力があると)
        {
            cam.transform.localPosition = new Vector3(initialCamPos.x, initialCamPos.y + Mathf.PingPong(Time.time / 2, 0.2f), initialCamPos.z);
        }
        else
        {
            cam.transform.localPosition = initialCamPos;
        }
    }

    void RotateControl()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        //UpdateCursorLock();
    }

    //マウスカーソルの表示非表示を制御する関数
    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    #region カメラシェイク

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = cam.transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = pos;
    }

    #endregion

    #region ゲッター、セッター

    public float GetPlayerSpeed() { return speed; }

    public float GetInitialPlayerSpeed() { return initialPlayerSpeed; }

    public void SetPlayerSpeed(float playerSpeed) { speed = playerSpeed; }

    #endregion
}

public class Actor : MonoBehaviour
{
    //プレイヤーのインスタンス
    protected Player playerInstance;

    //対象のインスタンス
    protected Target targetInstance;

    private void Awake()
    {
        //ゲーム開始時プレイヤーインスタンスとターゲットインスタンスを取得
        playerInstance = FindObjectOfType<Player>();
        targetInstance = FindObjectOfType<Target>();
    }

    public virtual void MoveControl() { }
    public virtual void AnimationControl() { }
}
