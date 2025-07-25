using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //���̃X�N���v�g�̓{�[���̈ړ����x�Ǘ��ƕ��������̍X�V
    //This script manages the ball's speed and updates the physics.

    //�ϐ��錾
    public Vector2 velocity = new Vector2(3f, 3f);  //�������x
    public float bounceFactor = 1.0f;               //�o�E���h�̔����W���i1�ɋ߂��قǔ������A0�ɋ߂��قǌ����j
    public float speedLimit = 20f;                  //�ő呬�x����

    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�[���̈ړ����蓮�ōs���i�����G���W���ɔC����ꍇ�͍X�V�s�v�j
        _rb.linearVelocity = Vector2.ClampMagnitude(velocity, speedLimit);      //Vector2.ClampMagnitude : �x�N�g�����w�肳�ꂽ�����ispeedLimit�j�ȉ��Ɏ��߂�
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

