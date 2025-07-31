using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{

    public float Movespeed = 0.0f;      //移動速度
    public float JumpPower = 10.0f;     //ジャンプ力
   
    private Rigidbody rb;               //リジッドボディを使う
    private CapsuleCollider col;       //カプセルコライダーを使う

    //地面判定
    public LayerMask groundLayers;
    public float groundCheckRadius = 0.1f;

    //壁判定
    public float WallCahechRadius = 0.1f;
    public LayerMask WallLayers;

    // Start is called before the  first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //リジッドボディの取得
        col = GetComponent<CapsuleCollider>();//カプセルコライダーの取得
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    //移動の関数
    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Movespeed, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(Movespeed, 0.0f, 0.0f);
        }
        
    }
    //ジャンプの関数
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);  //上方向にジャンプする
        }
    }

    //地面の接触判定
    private bool IsGround()
    {
        Vector3 groundCheckPosition = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        return Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayers);
    }

   /* private bool IsLeftWall()
    {
        Vector3 WallCheckPosition = new Vector3(col.bounds.left.x, col.bounds.center.y, col.bounds.center.z);
        return Physics.CheckSphere(WallCheckPosition, WallCahechRadius, WallLayers);
    }*/
}
