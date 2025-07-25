using UnityEngine;

/// <summary>
/// ���˂̋��ʃC���^�[�t�F�[�X
/// Strategy�p�^�[��(ReflectStrategyType strategyType)���g�p���čs��
/// </summary>
public interface IReflectionStrategy
{
    /// <summary>
    /// ���ˏ�������֐�
    /// ����1: �{�[���̈ʒu
    /// ����2: �{�[���̌���
    /// ����3: �{�[���ƐڐG�����I�u�W�F�N�g
    /// </summary>
    Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider);
}
