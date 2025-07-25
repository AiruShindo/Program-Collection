using UnityEngine;

public class ReflectionState : MonoBehaviour
{
    //���ː헪��IReflectionStrategy����ĎQ�Ɛؑւ��s��

    /// <summary>
    /// ���ː헪�̎��
    /// </summary>
    public enum ReflectStrategyType
    {
        Paddle,     //�n�h���p�̔���
        Surface     //�n�h���ȊO�̔���
    }
    public ReflectStrategyType strategyType;

    /// <summary>
    /// ���ː헪���@���擾����֐�
    /// ���� : �Ȃ�
    /// �߂�l : IReflectionStrategy
    /// </summary>
    public IReflectionStrategy GetStrategy()
    {
        switch (strategyType)
        {
            case ReflectStrategyType.Paddle:
                {
                    return new PaddleReflection();
                }
            case ReflectStrategyType.Surface:
                {
                    return new SurfaceReflection();
                }
            default:
                return null;
        }
    }
}
