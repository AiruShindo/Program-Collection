using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;

public class Enemy01bullet : MonoBehaviour
{
    //移動速度用の多重配列[X,Y]
    public float[,] IntValues = new float[,]
    {
        {0.1f,0.2f},{0.3f,0.1f},{0.2f,0.2f},{0.1f,0.1f},
        {-0.2f,0.1f},{-0.1f,0.3f},{-0.3f,0.2f},{-0.1f,0.2f}
    };
    private int IndexNum = 0;

    //移動処理に使用する変数(X,Y)
    private float posX;
    private float posY;

    //移動の幅を仮決定させる変数
    private float qx;
    private float qy;

    private bool onceFlag = false;      //１回のみの処理を行うフラグ
    private bool bFlag = false;     //反転移動用のフラグ

    // Start is called before the first frame update
    void Start()
    {
        //インスタンス化した後に行われる訳ではない為、あまり使わない事にする
    }

    // Update is called once per frame
    void Update()
    {
        //１回のみ処理を行う（void Start代わり）
        if (!onceFlag)
        {
            //初期座標設置
            posX = transform.position.x;
            posY = transform.position.y;

            //移動値の決定
            GetRandomNum();      //乱数値をIndexNumに代入
            GetMoveArray();

            //全ての処理が終わったのでフラグを上げておく
            onceFlag = true;
        }

        //移動
        Move();
        transform.localRotation = Quaternion.Euler(0, 25, 0);   //Y軸を25°ずつ回転させる

    }

    //移動関数
    public void Move()
    {
        //bFlagの切り替えによって移動方向を切り替える（反転）
        if (!bFlag) {
            posX -= qx;
            posY -= qy;
        }
        //else {
        //    posX -= qx;
        //    posY += qy;
        //}

        //座標＆回転更新（斜め移動）
        transform.position = new Vector3(posX, posY, 0);
    }

    //0～7の乱数をIndexNumに代入させる関数（ワールドY座標を決定させ、どこから降って来るか決める）
    private void GetRandomNum()
    {
        IndexNum =  UnityEngine.Random.Range(0,7);
    }

    //配列の値から移動値を決定
    private void GetMoveArray()
    {
        for (int i = 0; i < IntValues.GetLength(0); ++i)
        {
            for (int j = 0; j < IntValues.GetLength(1); ++j)
            {
                //IntValue.0列目はX座標、IntValue.1列目はY座標に格納させる
                if (IntValues[i, j] == IntValues[IndexNum,0])
                {
                    qx = IntValues[i, j];
                }
                if (IntValues[i, j] == IntValues[IndexNum, 1])
                {
                    qy = IntValues[i, j];
                }

                //どちらかの速度が0だった場合、規定速度に変更させる
                if (qx == 0)
                {
                    qx = 0.3f;
                }
                if (qy == 0)
                {
                    qy = 0.3f;
                }

                Debug.Log("移動値は" + qx + "," + qy);
            }
        }
    }

    //接触判定
    private void OnCollisionEnter(Collision collision)
    {
        //足場及び、同じ敵同士が接触を行った場合、自身の破棄を行う
        if (collision.gameObject.tag == "tatemono" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        
        //プレイヤーに当たったら移動反転切替
        if (collision.gameObject.tag == "bat")
        {
            bFlag = true;
        }
    }
}
