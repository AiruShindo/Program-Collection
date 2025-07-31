using UnityEngine;

public class BlockBase : MonoBehaviour
{
    //ブロック専用のスクリプト

    public GameManager gameManager;

    public bool canDropItem = false;    //アイテムドロップするかしないか決めるフラグスイッチ

    /// <summary>
    /// ブロックの耐久値(HP)
    /// </summary>
    [SerializeField] protected int durability;      //個別に設定可能

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManagerが見つかりません！");
        }
    }

    /// <summary>
    /// ダメージ関数
    /// </summary>
    public virtual void TakeDamage(int amount)
    {
        durability -= amount;
        Debug.Log("HP:" + durability);

        //HPが0以下になったら
        if (durability <= 0)
        {
            if (canDropItem && ItemManager.Instance != null)
            {
                //アイテムドロップをする
                ItemManager.Instance.SpawnRandomItem(this.transform.position);
            }

            //ブロック破壊
            BreakBlock();

            //ScoreManager.Instance.AddScore(100);

        }
    }

    /// <summary>
    /// ブロックが壊れた時に呼ばれる関数
    /// </summary>
    protected virtual void BreakBlock()
    {
        //GameManagerに通知してblocksPrehubリストから外す
        gameManager.OnBlockDestroyed(gameObject);

        //オブジェクトを破壊
        Destroy(gameObject);
    }

}
