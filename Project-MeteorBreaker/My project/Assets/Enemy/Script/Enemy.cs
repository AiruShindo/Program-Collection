using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //これはEmemyの基本動作である

    public GameObject[] EnemyObj;         //敵プレハブの格納（拡張を考え、配列化させておく）
    public int ObjIndex;                   //配列要素数

    protected int IntervalFlame;      //時間(フレーム数)を格納する変数

    protected float vx;         //X座標の位置を格納する変数


    public GameObject gamecontroller;
    public GameController _gameController;

    //敵生成状態
    public enum EnemyStutas
    {
        none,          //何もなし
        Instance,        //生成
        stopTime,           //待機時間
    }
    public EnemyStutas st;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        st = EnemyStutas.none;
        vx = 0.0f;
        IntervalFlame = 0;

        gamecontroller = GameObject.Find("GameStartTimer");
        _gameController = gamecontroller.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.CurrentState == GameController.PlayState.Play)
        {
            switch (st)
            {
                case EnemyStutas.none: { st = EnemyStutas.Instance; } break;
                case EnemyStutas.Instance: { SetEnemy(); } break;
                case EnemyStutas.stopTime:
                    {
                        //2秒経ったら敵生成を再開させる
                        if (IntervalFlame == 60 * 2) { ResetStutas(); }
                        ++IntervalFlame;        //毎フレーム加算
                    }
                    break;
            }
        }
    }

    //敵生成を行う関数
    protected void SetEnemy()
    {
        //X座標を決定
        SetposX();
        //インスタント化
        Instantiate(EnemyObj[0], new Vector3(vx, transform.position.y, 0), Quaternion.identity);

        //待機時間に移動
        st = EnemyStutas.stopTime;
    }

    //X座標の位置を乱数によって決定・格納する関数
    protected void SetposX()
    {
        //-4.0f～4.0fの範囲内で決める
        vx = UnityEngine.Random.Range(-4.0f, 4.0f);
        Debug.Log(vx);
    }

    //状態リセット
    protected void ResetStutas()
    {
        IntervalFlame = 0;
        st = EnemyStutas.none;
    }
}
