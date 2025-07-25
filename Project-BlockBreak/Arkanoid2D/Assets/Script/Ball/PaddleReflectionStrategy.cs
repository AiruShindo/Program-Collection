using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using static UnityEngine.RuleTile.TilingRuleOutput;

//反射戦略をまとめたスクリプト

/// <summary>
/// PaddleReflection : パドルに使われる反射処理クラス
/// 実装Interface : IReflectionStrategy:GetReflectedVelocity
/// パドルの中心点からの範囲を-1～1で正規化し、その値によって反射角度を調整する（真ん中だった場合、右斜めに設定される）
/// </summary>
public class PaddleReflection : IReflectionStrategy
{
    public Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider)
    {
        //パドルの位置・サイズを取得
        UnityEngine.Transform paddleTransform = collider.transform;
        float paddleWidth = collider.bounds.size.x;

        //ボールとパドルのX座標差を取得（パドルの中心を基準に、当たった位置のX差分を計算）
        float hitPoint = ballPosition.x - paddleTransform.position.x;

        //normalizedHitPointの値を-1～+1の範囲内で正規化（パドルの左端が -1、右端が +1）
        float normalizedHitPoint = Mathf.Clamp(hitPoint / (paddleWidth / 2f), -1f, 1f);
        Debug.Log("X軸の成分：" + normalizedHitPoint);

        //normalizedHitPointの値がほぼ０（当たったのが中央部分だったら）
        if (Mathf.Abs(normalizedHitPoint) < 0.15f)
        {
            //右寄りの角度に調整（中央部分に当たったら右斜め上に跳ねる様に調整を行う）
            normalizedHitPoint = 0.6f;

            Debug.Log("Angle adjustment");
        }

        //ここでX成分の向きを決める
        float newDirX;
        if (normalizedHitPoint > 0.15f)
        {
            //パドルの右側に当たったら左向き（負のX）
            newDirX = -Mathf.Abs(normalizedHitPoint);
        }
        else
        {
            //パドルの左側に当たったら右向き（正のX）
            newDirX = Mathf.Abs(normalizedHitPoint);
        }

        float newDirY = 1f;  // Y成分は常に上向き

        Debug.Log("Contact Player");
        return new Vector2(newDirX, newDirY).normalized * ballVelocity.magnitude;        //プレイヤーとの接触反射処理はここで処理終了
    }
}

/// <summary>
/// SurfaceReflection : ハドル以外に使われる反射処理クラス
/// 実装Interface : IReflectionStrategy:GetReflectedVelocity
/// 当たったオブジェクトの表面から法線ベクトル・衝突範囲を取得。
/// 上下左右の向きによって処理は多少変わるが共通して、中心点から-1～1で正規化、その値によって反射角度を調整する
/// </summary>
public class SurfaceReflection : IReflectionStrategy
{
    public Vector2 GetReflectedVelocity(Vector2 ballPosition, Vector2 ballVelocity, Collider2D collider)
    {
        //当たったオブジェクトに設定されている表面を取得する
        SurfaceNormal surface = collider.GetComponent<SurfaceNormal>();
        if (surface == null) return ballVelocity;

        Vector2 normal = surface.normal.normalized;         //衝突面の法線ベクトル
        Bounds surfaceBounds = collider.bounds;             //衝突相手のBounds（衝突範囲）

        Vector2 reflectedVelocity;                          //反射後の速度を格納する変数

        //法線ベクトルの絶対値がxよりyの方が大きかった場合、trueを返す
        bool isVerticalSurface = Mathf.Abs(normal.x) > Mathf.Abs(normal.y);

        if (isVerticalSurface)
        {
            //左右の壁 → 衝突相手の中心Y座標と接触Y座標差で角度調整

            float offsetY = ballPosition.y - surfaceBounds.center.y;

            //正規化（高さの半分で割る）
            float normalizedOffset = Mathf.Clamp(offsetY / (surfaceBounds.size.y / 2f), -1f, 1f);
            Debug.Log("Y軸の成分：" + normalizedOffset);

            //X成分は反転（左右壁なので反射はX軸成分反転）
            float newDirX = -ballVelocity.x;

            //Y成分にオフセットを加えて角度調整（上下に反射方向をずらす）
            float newDirY = ballVelocity.y + normalizedOffset;

            //if (Mathf.Abs(normalizedOffset) < 0.5f)
            //{
            //    newDirY = Mathf.Abs(newDirY);       //必ず上向き（正の値）にする
            //}

            reflectedVelocity = new Vector2(newDirX, newDirY);

            Debug.Log("Set - Left and Right");
        }
        else
        {
            //上下の壁 → 衝突相手の中心X座標と接触X座標差で角度調整

            float offsetX = ballPosition.x - surfaceBounds.center.x;

            float normalizedOffset = Mathf.Clamp(offsetX / (surfaceBounds.size.x / 2f), -1f, 1f);
            Debug.Log("X軸の成分：" + normalizedOffset);

            //Y成分は反転（上下壁なので反射はY軸成分反転）
            float newDirY = -ballVelocity.y;

            //X成分にオフセットを加えて角度調整（左右に反射方向をずらす）
            float newDirX = ballVelocity.x + normalizedOffset;

            //if (Mathf.Abs(normalizedOffset) > 0.5f)
            //{
            //    newDirX = Mathf.Abs(newDirX);       //必ず上向き（正の値）にする
            //}

            reflectedVelocity = new Vector2(newDirX, newDirY);

            Debug.Log("Set - Up and Down");
        }
        Debug.Log("Contact Walls or Blocks or Enemys");

        return reflectedVelocity.normalized * ballVelocity.magnitude;
    }
}
