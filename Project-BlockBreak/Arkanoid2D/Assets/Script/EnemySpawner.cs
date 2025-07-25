using JetBrains.Annotations;
using System;
using UnityEngine;


//敵を生成するスクリプト
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;                  //敵のプレハブ
    public Transform spawnPoint;                    //スポーンする位置
    private float spawnTimer = 0.0f;                //時間を格納する変数
    private float spawnTime = 5.0f;                 //生成するまでの時間が格納された変数

    [SerializeField] private int maxEnemyCount = 4;     //最大で生成する敵の数
    [SerializeField] private int currentEnemyCount = 0; //現在生成されている敵の数

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        //一定時間超えると一定数まで敵の生成を行う
        if (spawnTimer > spawnTime)
        {
            if (currentEnemyCount < maxEnemyCount)
            {
                SpawnEnemy();
            }

            spawnTimer = 0.0f;      //タイマーリセット
        }
    }

    //敵を生成するメソッド
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        currentEnemyCount++;
    }

    // 敵が破壊されたときに呼び出すメソッド（例：敵が倒された時）
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
