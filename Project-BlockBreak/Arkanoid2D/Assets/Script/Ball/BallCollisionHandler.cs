using NUnit.Framework;
using UnityEngine;
using static ReflectionState;
using System.Collections.Generic;

public class BallCollisionHandler : MonoBehaviour
{
    //���̃X�N���v�g�̓{�[���̏Փ˔���A���ˁA�_���[�W����������B
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
                    //�@���g�̈ʒu���A�A�ړ����x�A�B�ڐG�����I�u�W�F�N�g�@�����񋟂���
                    Vector2 reflected = strategy.GetReflectedVelocity(transform.position, movement.velocity, other);
                    Debug.Log("Ball is TransformRocation : " + transform.rotation);

                    //���˃x�N�g���Ƀo�E���X�W�����|����i�K�v�Ȃ�j
                    reflected *= movement.bounceFactor;

                    //BallMovement�i�ړ������j�ɒl��n��
                    movement.SetVelocity(reflected);
                    //handle = true;

                    Debug.Log("Collision with " + other.name + " tag:" + other.tag);
                }
            }

            //�u���b�N�̂݃_���[�W���������s
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
            //���ʉ���炷
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        //�Փˏ��̓t���[�����ƂɃN���A
        onHitColliders.Clear();
    }

    //�ڐG����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            //�Ƃ肠�������̃t���[���ŏՓ˂����R���C�_�[�������o���Ă�����
            onHitColliders.Add(other);
        }
    }

}
