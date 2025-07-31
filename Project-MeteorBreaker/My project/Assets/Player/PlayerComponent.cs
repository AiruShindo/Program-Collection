using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerComponent : MonoBehaviour
{
    #region MAIのいたずら
    //private const float GRAVITY = -20f;
    #endregion
    // end of いたずら

    public float Movespeed = 0.0f;
    public float JumpPower = 10.0f;

    public GameObject gamecontroller;
    public GameController _gameController;

    public Animator _PlayerAnimator;
   [SerializeField]  bool IsRun;
   [SerializeField]  bool IsAtk;

    //プレイヤーの向き
    [SerializeField] private bool CanRightMove;
    [SerializeField] private bool CanLeftMove;
    [SerializeField] private bool Rightdirection;
    [SerializeField] private bool Leftdirection;

    private Rigidbody rb;               //リジッドボディを使う
    private CapsuleCollider col;       //カプセルコライダーを使う


    //効果音
   /* public GameObject Audio;
    AudioSource audio;
    public AudioClip music1;
   */
    //地面判定
    public LayerMask groundLayers;
    public float groundCheckRadius = 0.1f;

    //壁判定
    public float WallCheckRadius = 0.1f;
    public LayerMask WallLayers;

    //攻撃オブジェクト
    public GameObject bat;

    // Start is called before the  first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //リジッドボディの取得
        col = GetComponent<CapsuleCollider>();//カプセルコライダーの取得
       // audio = GetComponent<AudioSource>();

        gamecontroller = GameObject.Find("GameStartTimer");
        _gameController = gamecontroller.GetComponent<GameController>();

        IsRun = false;

        CanRightMove = true;
        CanLeftMove = true;
        Rightdirection = false;
        Leftdirection = false;
}

    // Update is called once per frame
    void Update()
    {
        if (_gameController.CurrentState == GameController.PlayState.Play)
        {
            Move();
            Attack();
            Jump();
        }
    }

   /* private void FixedUpdate()
    {
        var newVelo = rb.velocity;
        newVelo.y += GRAVITY * Time.fixedDeltaTime;
        rb.velocity = newVelo;
    }*/
    //移動の関数
    void Move()
    {
       
        if (Input.GetKey(KeyCode.D) && (CanRightMove == true))
        {
            rb.MovePosition(rb.position + new Vector3(Movespeed, 0.0f, 0.0f));
            Rightdirection = true;
            Leftdirection = false;
            if(Rightdirection == true)
            {
                rb.rotation = Quaternion.AngleAxis(90.0f, Vector3.up);
            }
            IsRun = true;
        }

        if (Input.GetKey(KeyCode.A) && (CanLeftMove == true))
        {
            rb.MovePosition(rb.position - new Vector3(Movespeed, 0.0f, 0.0f));
            Leftdirection = true;
            Rightdirection = false;
            if(Leftdirection == true)
            {
                rb.rotation = Quaternion.AngleAxis(-90.0f, Vector3.up);
            }
            IsRun = true;
        }
        _PlayerAnimator.SetBool("RUN", IsRun);
        
    }

    //ジャンプの関数
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);  //上方向にジャンプする
        }
    }

    bool IsGround()
    {
        Vector3 groundCheckPosition = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        return Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayers);
    }

    void Attack()
    {
        IsAtk = false;
        if(Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(bat, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            IsAtk = true;
            _PlayerAnimator.SetBool("ATK", IsAtk);
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "RightWall")
        {
            CanRightMove = false;
            CanLeftMove = true;
        }
        if (collision.gameObject.tag == "LeftWall")
        {
            CanLeftMove = false;
            CanRightMove = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RightWall")
        {
            CanRightMove = true;
        }
        if (collision.gameObject.tag == "LeftWall")
        {
            CanLeftMove = true;
        }

    }
}
