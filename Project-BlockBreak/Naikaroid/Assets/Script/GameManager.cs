using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゲーム全体に関係するものを管理するマネージャースクリプト
public class GameManager : MonoBehaviour
{
    //変数宣言
    public static GameManager Instance { get; private set; }        //他スクリプトでGameManagerを取得できる用（シングルトン化）
    public Camera mainCamera;       //CameraComponent参照用

    private List<GameObject> blocksPrehub;      //ブロックリスト
    private List<GameObject> ballsPrehub;       //ボールリスト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ステージ内にあるブロック及びボールをList<>で取得
        blocksPrehub = new List<GameObject>(GameObject.FindGameObjectsWithTag("Block"));
        ballsPrehub = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ball"));
        
        //カメラの再設定
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //画面内に1つでもブロックまたはボールがある際のフラグ
        // 毎フレーム、フラグをfalseに初期化
        bool anyBlockInView = false;
        bool anyBallInView = false;

        //ブロックの画面内判定
        //foreach : コレクション（配列やリストなどの複数の要素をまとめたもの）を順番に1つずつ取り出して処理をする
        //var block -> blocksPrehub内の1つの要素
        foreach (var block in blocksPrehub)
        {
            //万が一破壊済みブロックに当たってしまったらスキップする
            if (block == null) continue;

            //ワールド座標をビューポート座標に変換（ビューポート座標は(0,0)が画面左下、(1,1)が画面右上で表される）
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(block.transform.position);
            //画面内にブロックが存在しているかどうか（viewportPos.z > 0 -> カメラの前にあるかどうか）
            if (viewportPos.x >= 0 && viewportPos.x <= 1 &&　viewportPos.y >= 0 && viewportPos.y <= 1 &&　viewportPos.z > 0)
            {
                //ブロックが1つでも存在しているのでtrueに変更
                anyBlockInView = true;
                break;
            }
        }

        //ボールの画面内判定（中身はブロックの判定と同様）
        foreach (var ball in ballsPrehub)
        {
            //万が一破壊済みボールに当たってしまったらスキップする
            if (ball == null) continue;

            Vector3 viewportPos = mainCamera.WorldToViewportPoint(ball.transform.position);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 &&　viewportPos.y >= 0 && viewportPos.y <= 1 &&　viewportPos.z > 0)
            {
                //ボールが1つでも存在しているのでtrueに変更
                anyBallInView = true;
                break;
            }
        }

        //ブロックまたはボールが1つも画面内になければ（trueでなかった場合）ゲームリセット
        if (!anyBlockInView || !anyBallInView)
        {
            ResetGame();
        }
    }

    // ブロックが壊れるスクリプト側で呼ぶ関数
    public void OnBlockDestroyed(GameObject destroyedBlock)
    {
        //スコア加算
        ScoreManager.Instance.AddScore(100);

        blocksPrehub.Remove(destroyedBlock);
    }

    //ボールをリストに動的に追加する関数
    public void AddBall(GameObject newBall)
    {
        if (!ballsPrehub.Contains(newBall))
        {
            ballsPrehub.Add(newBall);
        }
    }

    // ボールを破棄された時に呼ぶ関数
    public void OnBallDestroyed(GameObject destroyedBall)
    {
        //Ball.csでnullを発生させない為の安全策
        if (destroyedBall != null && ballsPrehub.Contains(destroyedBall))
        {
            ballsPrehub.Remove(destroyedBall);
        }
    }

    //ゲームリセット
    private void ResetGame()
    {
        //スコアリセット
        ScoreManager.Instance.ResetScore();

        // 現在のシーンを再読み込みしてリセット（これは後に別のSceneへの移行へ変更）
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
