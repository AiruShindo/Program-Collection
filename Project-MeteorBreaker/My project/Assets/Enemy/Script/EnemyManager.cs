using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Enemy
{


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        st = EnemyStutas.none;
        vx = 0.0f;
        IntervalFlame = 0;

        //コンポーネント取得
        //_rb = GetComponent<Rigidbody>();

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

}
