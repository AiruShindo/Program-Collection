using UnityEngine;

//プレイヤーの処理
public class Player : MonoBehaviour
{
    //変数宣言
    public float Speed = 0.5f;      //移動速度

    public float minX;       // 移動の最小X位置
    public float maxX;        // 移動の最大X位置

    private Rigidbody2D _Rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //移動処理
        if (Input.GetMouseButton(0))
        {
            //左クリック押している間はマウス追従操作
            MoveWithMouse();
        }
        else
        {
            //左クリックを押していないときはキーボード操作
            MoveWithKeybroad();
        }

    }

    //キーボードの移動関数
    private void MoveWithKeybroad()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        else if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector2 vec = new Vector2(moveX, 0f);           //移動量
        Vector2 newPos = _Rb.position + vec * Speed;

        //移動範囲の制限
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        _Rb.MovePosition(newPos);
    }

    //マウスの移動関数
    private void MoveWithMouse()
    {
        //マウス位置(X座標のみ)をワールド座標に変換
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //移動範囲の制限
        Vector2 targetPos = new Vector2(Mathf.Clamp(mouseWorldPos.x, minX, maxX), _Rb.position.y);

        _Rb.MovePosition(Vector2.MoveTowards(_Rb.position, targetPos, Speed));
    }

}
