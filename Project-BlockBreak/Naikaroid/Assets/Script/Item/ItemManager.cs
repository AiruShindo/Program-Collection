using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public GameObject[] itemPrefabs;        //複数のアイテムプレハブにも対応可

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// 複数のアイテムをランダムにスポーンさせる関数
    /// </summary>
    public void SpawnRandomItem(Vector3 position)
    {
        if (itemPrefabs.Length == 0) return;

        int index = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[index], position, Quaternion.identity);
    }

}
