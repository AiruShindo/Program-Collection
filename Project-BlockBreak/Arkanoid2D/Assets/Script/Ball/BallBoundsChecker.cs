using UnityEngine;

public class BallBoundsChecker : MonoBehaviour
{
    //���̃X�N���v�g�̓{�[���̉�ʊO����Ɣj��������B
    //This script handles off-screen detection and discarding of the ball.

    //�{�[����ActiveArea�O�ł̏���
    private void OnTriggerExit2D(Collider2D other)
    {
        //�j�����s��
        if (other.CompareTag("ActiveArea"))
        {
            DestroyBall();
        }
    }

    //�{�[���̔j�����ɍs������
    private void DestroyBall()
    {
        //�Q�[���}�l�[�W���[���ɑ��݂��郊�X�g����폜
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBallDestroyed(gameObject);
        }

        Destroy(gameObject);
    }
}
