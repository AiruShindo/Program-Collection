using NUnit.Framework;
using UnityEngine;
using static ReflectionState;
using System.Collections.Generic;

public class BallCollisionHandler : MonoBehaviour
{
    //このスクリプトはボールの衝突判定、反射、ダメージ処理をする。
    //This script handles ball collision detection, reflection, and damage handling.

    public AudioClip clip;
    private AudioSource audioSource;

    private BallMovement movement;
    //private bool handle = false;

    private List<Collider2D> onHitColliders = new List<Collider2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<BallMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {

        foreach (Collider2D other in onHitColliders)
        {
            ReflectionState strategyHandler = other.GetComponent<ReflectionState>();

            if (strategyHandler != null)
            {
                IReflectionStrategy strategy = strategyHandler.GetStrategy();
                if (strategy != null)
                {
                    //①自身の位置情報、②移動速度、③接触したオブジェクト　を情報提供する
                    Vector2 reflected = strategy.GetReflectedVelocity(transform.position, movement.velocity, other);
                    Debug.Log("Ball is TransformRocation : " + transform.rotation);

                    //反射ベクトルにバウンス係数を掛ける（必要なら）
                    reflected *= movement.bounceFactor;

                    //BallMovement（移動処理）に値を渡す
                    movement.SetVelocity(reflected);
                    //handle = true;

                    Debug.Log("Collision with " + other.name + " tag:" + other.tag);
                }
            }

            //ブロックのみダメージ処理を実行
            if (other.CompareTag("Block"))
            {
                BlockBase block = other.GetComponent<BlockBase>();
                if (block != null) block.TakeDamage(1);
            }
        }


        if (onHitColliders.Count == 2)
        {
            for (int i = 0; i < onHitColliders.Count; ++i)
            {
                var sn = onHitColliders[i].GetComponent<SurfaceNormal>();

                if (sn)
                {
                    if (sn.normal.x != 0.0f && (Mathf.Sign(movement.velocity.x) != Mathf.Sign(sn.normal.x)))
                    {
                        movement.velocity.x *= -1.0f;
                    }
                    else if (sn.normal.y != 0.0f && (Mathf.Sign(movement.velocity.y) != Mathf.Sign(sn.normal.y)))
                    {
                        movement.velocity.y *= -1.0f;
                    }
                }
            }
        }

        if (onHitColliders.Count > 0)
        {
            //効果音を鳴らす
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        //衝突情報はフレームごとにクリア
        onHitColliders.Clear();
    }

    //接触判定
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            //とりあえずこのフレームで衝突したコライダーだけを覚えておこう
            onHitColliders.Add(other);
        }
    }

}
