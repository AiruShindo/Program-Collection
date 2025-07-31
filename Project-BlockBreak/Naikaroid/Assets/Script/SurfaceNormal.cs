using UnityEngine;

public class SurfaceNormal : MonoBehaviour
{
    //オブジェクトの法線方向の種類
    //反射させたい場合には反射させたい向きに設定を行う
    //（右壁の場合：左に反射したいので"Left"へ設定を行う）
    public enum DirectionType
    {
        Up,            //上
        Down,       //下
        Left,           //左
        Right,         //右
    }
    [SerializeField] private DirectionType direction = DirectionType.Up;

    // 法線の方向を設定する関数
    public Vector2 normal
    {
        get
        {
            switch (direction)
            {
                case DirectionType.Up: return Vector2.up;
                case DirectionType.Down: return Vector2.down;
                case DirectionType.Left: return Vector2.left;
                case DirectionType.Right: return Vector2.right;
                default: return Vector2.up;
            }
        }
    }

}
