using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    //これはEmemyの生成用csファイルである

    public GameObject[] EnemyObj;               //敵プレハブの格納（拡張を考え、配列化させておく）
    public int ObjIndex;                        //配列要素数

    protected int IntervalFlame;                //時間(フレーム数)を格納する変数

    protected float vx;                         //X座標の位置を格納する変数

    //敵生成状態
    public enum EnemyStutas
    {
        none,               //何もなし
        Instance,           //生成
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
    }

    // Update is called once per frame
    void Update()
    {
        //生成の状態管理
        switch (st)
        {
            case EnemyStutas.none: { st = EnemyStutas.Instance; } break;
            case EnemyStutas.Instance: { SetEnemy(); } break;
            case EnemyStutas.stopTime:
                {
                    //2秒経ったら敵生成を再開させる(60fps換算で計算を行う)
                    if (IntervalFlame == 60 * 2) { ResetStutas(); }
                    ++IntervalFlame;        //毎フレーム加算
                } break;
        }
    }

    //敵生成を行う関数
    private void SetEnemy()
    {
        //X座標を決定
        SetposX();
        //敵をインスタント化
        Instantiate(EnemyObj[0], new Vector3(vx, transform.position.y, 0), Quaternion.identity);

        //待機時間に移動
        st = EnemyStutas.stopTime;
    }

    //X座標の位置を乱数によって決定・格納する関数
    private void SetposX()
    {
        //-4.0f～4.0fの範囲内で決める
        vx = UnityEngine.Random.Range(-6, 6);
        Debug.Log(vx);
    }

    //状態リセット
    private void ResetStutas()
    {
        IntervalFlame = 0;
        st = EnemyStutas.none;
    }
}
