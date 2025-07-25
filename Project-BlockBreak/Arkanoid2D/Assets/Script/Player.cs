using UnityEngine;

//�v���C���[�̏���
public class Player : MonoBehaviour
{
    //�ϐ��錾
    public float Speed = 0.5f;      //�ړ����x

    public float minX;       // �ړ��̍ŏ�X�ʒu
    public float maxX;        // �ړ��̍ő�X�ʒu

    private Rigidbody2D _Rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�����
        if (Input.GetMouseButton(0))
        {
            //���N���b�N�����Ă���Ԃ̓}�E�X�Ǐ]����
            MoveWithMouse();
        }
        else
        {
            //���N���b�N�������Ă��Ȃ��Ƃ��̓L�[�{�[�h����
            MoveWithKeybroad();
        }

    }

    //�L�[�{�[�h�̈ړ��֐�
    private void MoveWithKeybroad()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        else if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector2 vec = new Vector2(moveX, 0f);           //�ړ���
        Vector2 newPos = _Rb.position + vec * Speed;

        //�ړ��͈͂̐���
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        _Rb.MovePosition(newPos);
    }

    //�}�E�X�̈ړ��֐�
    private void MoveWithMouse()
    {
        //�}�E�X�ʒu(X���W�̂�)�����[���h���W�ɕϊ�
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //�ړ��͈͂̐���
        Vector2 targetPos = new Vector2(Mathf.Clamp(mouseWorldPos.x, minX, maxX), _Rb.position.y);

        _Rb.MovePosition(Vector2.MoveTowards(_Rb.position, targetPos, Speed));
    }

}
