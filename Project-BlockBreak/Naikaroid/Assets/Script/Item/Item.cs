using UnityEngine;

//アイテムオブジェクトにアタッチされるスクリプト

public class Item : MonoBehaviour
{
    public float moveSpeed = 0.1f;      //移動速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0.0f, -moveSpeed));
    }

    //接触判定
    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーに当たったら破棄を行う
        if (other.CompareTag("Player"))
        {
            //スコア加算
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
