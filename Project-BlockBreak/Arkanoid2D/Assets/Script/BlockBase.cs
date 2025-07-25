using UnityEngine;

public class BlockBase : MonoBehaviour
{
    //�u���b�N��p�̃X�N���v�g

    public GameManager gameManager;

    public bool canDropItem = false;    //�A�C�e���h���b�v���邩���Ȃ������߂�t���O�X�C�b�`

    /// <summary>
    /// �u���b�N�̑ϋv�l(HP)
    /// </summary>
    [SerializeField] protected int durability;      //�ʂɐݒ�\

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager��������܂���I");
        }
    }

    /// <summary>
    /// �_���[�W�֐�
    /// </summary>
    public virtual void TakeDamage(int amount)
    {
        durability -= amount;
        Debug.Log("HP:" + durability);

        //HP��0�ȉ��ɂȂ�����
        if (durability <= 0)
        {
            if (canDropItem && ItemManager.Instance != null)
            {
                //�A�C�e���h���b�v������
                ItemManager.Instance.SpawnRandomItem(this.transform.position);
            }

            //�u���b�N�j��
            BreakBlock();

            //ScoreManager.Instance.AddScore(100);

        }
    }

    /// <summary>
    /// �u���b�N����ꂽ���ɌĂ΂��֐�
    /// </summary>
    protected virtual void BreakBlock()
    {
        //GameManager�ɒʒm����blocksPrehub���X�g����O��
        gameManager.OnBlockDestroyed(gameObject);

        //�I�u�W�F�N�g��j��
        Destroy(gameObject);
    }

}
