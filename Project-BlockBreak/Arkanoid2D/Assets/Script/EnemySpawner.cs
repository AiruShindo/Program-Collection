using JetBrains.Annotations;
using System;
using UnityEngine;


//�G�𐶐�����X�N���v�g
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;                  //�G�̃v���n�u
    public Transform spawnPoint;                    //�X�|�[������ʒu
    private float spawnTimer = 0.0f;                //���Ԃ��i�[����ϐ�
    private float spawnTime = 5.0f;                 //��������܂ł̎��Ԃ��i�[���ꂽ�ϐ�

    [SerializeField] private int maxEnemyCount = 4;     //�ő�Ő�������G�̐�
    [SerializeField] private int currentEnemyCount = 0; //���ݐ�������Ă���G�̐�

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        //��莞�Ԓ�����ƈ�萔�܂œG�̐������s��
        if (spawnTimer > spawnTime)
        {
            if (currentEnemyCount < maxEnemyCount)
            {
                SpawnEnemy();
            }

            spawnTimer = 0.0f;      //�^�C�}�[���Z�b�g
        }
    }

    //�G�𐶐����郁�\�b�h
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        currentEnemyCount++;
    }

    // �G���j�󂳂ꂽ�Ƃ��ɌĂяo�����\�b�h�i��F�G���|���ꂽ���j
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
