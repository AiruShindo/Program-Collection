using UnityEngine;

public class SurfaceNormal : MonoBehaviour
{
    //�I�u�W�F�N�g�̖@�������̎��
    //���˂��������ꍇ�ɂ͔��˂������������ɐݒ���s��
    //�i�E�ǂ̏ꍇ�F���ɔ��˂������̂�"Left"�֐ݒ���s���j
    public enum DirectionType
    {
        Up,            //��
        Down,       //��
        Left,           //��
        Right,         //�E
    }
    [SerializeField] private DirectionType direction = DirectionType.Up;

    // �@���̕�����ݒ肷��֐�
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
