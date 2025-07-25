using UnityEngine;

//�A�C�e���I�u�W�F�N�g�ɃA�^�b�`�����X�N���v�g

public class Item : MonoBehaviour
{
    public float moveSpeed = 0.1f;      //�ړ����x

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0.0f, -moveSpeed));
    }

    //�ڐG����
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[�ɓ���������j�����s��
        if (other.CompareTag("Player"))
        {
            //�X�R�A���Z
            ScoreManager.Instance.AddScore(1000);

            Debug.Log("Contact to Item");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ActiveArea"))
        {
            Debug.Log("Destroy to Item");
            Destroy(this.gameObject);
        }
    }
}
