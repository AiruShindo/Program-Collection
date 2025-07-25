using UnityEngine;

public class BallBoundsChecker : MonoBehaviour
{
    //このスクリプトはボールの画面外判定と破棄をする。
    //This script handles off-screen detection and discarding of the ball.

    //ボールがActiveArea外での処理
    private void OnTriggerExit2D(Collider2D other)
    {
        //破棄を行う
        if (other.CompareTag("ActiveArea"))
        {
            DestroyBall();
        }
    }

    //ボールの破棄時に行う処理
    private void DestroyBall()
    {
        //ゲームマネージャー側に存在するリストから削除
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBallDestroyed(gameObject);
        }

        Destroy(gameObject);
    }
}
