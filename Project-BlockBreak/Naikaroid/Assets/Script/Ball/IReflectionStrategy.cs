using UnityEngine;

/// <summary>
/// 反射の共通インターフェース
/// Strategyパターン(ReflectStrategyType strategyType)を使用して行う
/// </summary>
public interface IReflectionStrategy
{
    /// <summary>
    /// 反射処理する関数
    /// 引数1: ボールの位置
    /// 引数2: ボールの向き
    /// 引数3: ボールと接触したオブジェクト
    /// </summary>
    Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider);
}
