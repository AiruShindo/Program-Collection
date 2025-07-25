using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using static UnityEngine.RuleTile.TilingRuleOutput;

//���ː헪���܂Ƃ߂��X�N���v�g

/// <summary>
/// PaddleReflection : �p�h���Ɏg���锽�ˏ����N���X
/// ����Interface : IReflectionStrategy:GetReflectedVelocity
/// �p�h���̒��S�_����͈̔͂�-1�`1�Ő��K�����A���̒l�ɂ���Ĕ��ˊp�x�𒲐�����i�^�񒆂������ꍇ�A�E�΂߂ɐݒ肳���j
/// </summary>
public class PaddleReflection : IReflectionStrategy
{
    public Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider)
    {
        //�p�h���̈ʒu�E�T�C�Y���擾
        UnityEngine.Transform paddleTransform = collider.transform;
        float paddleWidth = collider.bounds.size.x;

        //�{�[���ƃp�h����X���W�����擾�i�p�h���̒��S����ɁA���������ʒu��X�������v�Z�j
        float hitPoint = ballPosition.x - paddleTransform.position.x;

        //normalizedHitPoint�̒l��-1�`+1�͈͓̔��Ő��K���i�p�h���̍��[�� -1�A�E�[�� +1�j
        float normalizedHitPoint = Mathf.Clamp(hitPoint / (paddleWidth / 2f), -1f, 1f);
        Debug.Log("X���̐����F" + normalizedHitPoint);

        //normalizedHitPoint�̒l���قڂO�i���������̂�����������������j
        if (Mathf.Abs(normalizedHitPoint) < 0.15f)
        {
            //�E���̊p�x�ɒ����i���������ɓ���������E�΂ߏ�ɒ��˂�l�ɒ������s���j
            normalizedHitPoint = 0.6f;

            Debug.Log("Angle adjustment");
        }

        //������X�����̌��������߂�
        float newDirX;
        if (normalizedHitPoint > 0.15f)
        {
            //�p�h���̉E���ɓ��������獶�����i����X�j
            newDirX = -Mathf.Abs(normalizedHitPoint);
        }
        else
        {
            //�p�h���̍����ɓ���������E�����i����X�j
            newDirX = Mathf.Abs(normalizedHitPoint);
        }

        float newDirY = 1f;  // Y�����͏�ɏ����

        Debug.Log("Contact Player");
        return new Vector2(newDirX, newDirY).normalized * ballVelocity.magnitude;        //�v���C���[�Ƃ̐ڐG���ˏ����͂����ŏ����I��
    }
}

/// <summary>
/// SurfaceReflection : �n�h���ȊO�Ɏg���锽�ˏ����N���X
/// ����Interface : IReflectionStrategy:GetReflectedVelocity
/// ���������I�u�W�F�N�g�̕\�ʂ���@���x�N�g���E�Փ˔͈͂��擾�B
/// �㉺���E�̌����ɂ���ď����͑����ς�邪���ʂ��āA���S�_����-1�`1�Ő��K���A���̒l�ɂ���Ĕ��ˊp�x�𒲐�����
/// </summary>
public class SurfaceReflection : IReflectionStrategy
{
    public Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider)
    {
        //���������I�u�W�F�N�g�ɐݒ肳��Ă���\�ʂ��擾����
        SurfaceNormal surface = collider.GetComponent<SurfaceNormal>();
        if (surface == null) return ballVelocity;

        Vector2 normal = surface.normal.normalized;         //�Փ˖ʂ̖@���x�N�g��
        Bounds surfaceBounds = collider.bounds;             //�Փˑ����Bounds�i�Փ˔͈́j

        Vector2 reflectedVelocity;                          //���ˌ�̑��x���i�[����ϐ�

        //�@���x�N�g���̐�Βl��x���y�̕����傫�������ꍇ�Atrue��Ԃ�
        bool isVerticalSurface = Mathf.Abs(normal.x) > Mathf.Abs(normal.y);

        if (isVerticalSurface)
        {
            //���E�̕� �� �Փˑ���̒��SY���W�ƐڐGY���W���Ŋp�x����

            float offsetY = ballPosition.y - surfaceBounds.center.y;

            //���K���i�����̔����Ŋ���j
            float normalizedOffset = Mathf.Clamp(offsetY / (surfaceBounds.size.y / 2f), -1f, 1f);
            Debug.Log("Y���̐����F" + normalizedOffset);

            //X�����͔��]�i���E�ǂȂ̂Ŕ��˂�X���������]�j
            float newDirX = -ballVelocity.x;

            //Y�����ɃI�t�Z�b�g�������Ċp�x�����i�㉺�ɔ��˕��������炷�j
            float newDirY = ballVelocity.y + normalizedOffset;

            //if (Mathf.Abs(normalizedOffset) < 0.5f)
            //{
            //    newDirY = Mathf.Abs(newDirY);       //�K��������i���̒l�j�ɂ���
            //}

            reflectedVelocity = new Vector2(newDirX, newDirY);

            Debug.Log("Set - Left and Right");
        }
        else
        {
            //�㉺�̕� �� �Փˑ���̒��SX���W�ƐڐGX���W���Ŋp�x����

            float offsetX = ballPosition.x - surfaceBounds.center.x;

            float normalizedOffset = Mathf.Clamp(offsetX / (surfaceBounds.size.x / 2f), -1f, 1f);
            Debug.Log("X���̐����F" + normalizedOffset);

            //Y�����͔��]�i�㉺�ǂȂ̂Ŕ��˂�Y���������]�j
            float newDirY = -ballVelocity.y;

            //X�����ɃI�t�Z�b�g�������Ċp�x�����i���E�ɔ��˕��������炷�j
            float newDirX = ballVelocity.x + normalizedOffset;

            //if (Mathf.Abs(normalizedOffset) > 0.5f)
            //{
            //    newDirX = Mathf.Abs(newDirX);       //�K��������i���̒l�j�ɂ���
            //}

            reflectedVelocity = new Vector2(newDirX, newDirY);

            Debug.Log("Set - Up and Down");
        }
        Debug.Log("Contact Walls or Blocks or Enemys");

        return reflectedVelocity.normalized * ballVelocity.magnitude;
    }
}
