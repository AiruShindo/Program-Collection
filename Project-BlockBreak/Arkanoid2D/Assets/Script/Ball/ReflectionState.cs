using UnityEngine;

public class ReflectionState : MonoBehaviour
{
    //½ΛνͺπIReflectionStrategyπξ΅ΔQΖΨΦπs€

    /// <summary>
    /// ½ΛνͺΜνή
    /// </summary>
    public enum ReflectStrategyType
    {
        Paddle,     //nhpΜ½Λ
        Surface     //nhΘOΜ½Λ
    }
    public ReflectStrategyType strategyType;

    /// <summary>
    /// ½Λνͺϋ@πζΎ·ιΦ
    /// ψ : Θ΅
    /// ίθl : IReflectionStrategy
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
