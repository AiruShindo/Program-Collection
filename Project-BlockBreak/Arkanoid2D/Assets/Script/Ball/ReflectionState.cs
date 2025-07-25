using UnityEngine;

public class ReflectionState : MonoBehaviour
{
    //反射戦略をIReflectionStrategyを介して参照切替を行う

    /// <summary>
    /// 反射戦略の種類
    /// </summary>
    public enum ReflectStrategyType
    {
        Paddle,     //ハドル用の反射
        Surface     //ハドル以外の反射
    }
    public ReflectStrategyType strategyType;

    /// <summary>
    /// 反射戦略方法を取得する関数
    /// 引数 : なし
    /// 戻り値 : IReflectionStrategy
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
