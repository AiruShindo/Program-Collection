using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //このスクリプトはボールの移動速度管理と物理挙動の更新
    //This script manages the ball's speed and updates the physics.

    //変数宣言
    public Vector2 velocity = new Vector2(3f, 3f);  //初期速度
    public float bounceFactor = 1.0f;               //バウンドの反発係数（1に近いほど反発し、0に近いほど減衰）
    public float speedLimit = 20f;                  //最大速度制限

    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ボールの移動を手動で行う（物理エンジンに任せる場合は更新不要）
        _rb.linearVelocity = Vector2.ClampMagnitude(velocity, speedLimit);      //Vector2.ClampMagnitude : ベクトルを指定された長さ（speedLimit）以下に収める
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = Vector2.ClampMagnitude(newVelocity * bounceFactor, speedLimit);
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }

}

