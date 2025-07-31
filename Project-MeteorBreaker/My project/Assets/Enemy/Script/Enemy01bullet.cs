using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
//using static UnityEditor.PlayerSettings;

public class Enemy01bullet : MonoBehaviour
{
    //移動の為の配列(X,Y)
    public float[,] IntValues = new float[,]
    {
        {0.07f,0.03f},{0.06f,0.04f},{0.06f,0.09f},{0.03f,0.02f},
        {-0.056f,0.067f},{-0.058f,0.039f},{-0.067f,0.096f},{-0.01f,0.08f}
    };
    private int IndexNum = 0;
    private float posX;
    private float posY;
    private float qx;
    private float qy;

    private bool onceFlag = false;      //一回のみの処理を行うフラグ
    private bool bFlag = false;     //反射移動のフラグ

    /*
    //オブジェクトの親子関係に必要な変数
    public GameObject Arrow;        //矢印のオブジェクト（子オブジェクト）
    protected Vector3 Apos;           //矢印の位置座標（ワールド座標）
    private Vector3 Asize = new Vector3(0.5f, 0.5f, 0.0f);          //矢印のサイズ（ワールド座標）
    */
    // Start is called before the first frame update
    void Start()
    {
        //インスタンス化した後に行われる訳ではない為、あまり使わない事にする
    }

    // Update is called once per frame
    void Update()
    {
        //一回のみ処理を行う（void Start代わり）
        if (!onceFlag)
        {
            //初期座標設置
            posX = transform.position.x;
            posY = transform.position.y;

            //移動値の決定
            RandomArray();
            GetMoveArray();

            //矢印を子オブジェクトに設定
           // SetParent();

            onceFlag = true;
        }
        //移動
        Move();
        transform.localRotation = Quaternion.Euler(0, 25, 0);

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

    void RandomArray()
    {
        IndexNum =  UnityEngine.Random.Range(0,7);
    }

    //配列の値から移動値を決定
    void GetMoveArray()
    {
        for (int i = 0; i < IntValues.GetLength(0); ++i)
        {
            for (int j = 0; j < IntValues.GetLength(1); ++j)
            {
                //０列目はX座標、１列目はY座標に格納させる
                if (IntValues[i,j] == IntValues[IndexNum,0])
                { qx = IntValues[i, j]; }
                if (IntValues[i, j] == IntValues[IndexNum, 1])
                { qy = IntValues[i, j]; }

                if (qx == 0)
                {
                    qx = 0.03f;
                }
                if (qy == 0)
                {
                    qy = 0.03f;
                }
                Debug.Log("移動値は" + qx + "," + qy);
            }
        }
    }
    /*
    //子オブジェクト（矢印）の設定を行う関数
    void SetParent()
    {
        //子オブジェクトの座標とサイズを設定
        Apos = new Vector3(-qx, -qy, 0.0f);

        //■ 子オブジェクトのインスタンス情報を変数に代入
        GameObject obj = Instantiate(Arrow);

        //■ 親子関係を作る（今回、worldPositionStaysはfalse）
        //true → ワールド座標上での位置・サイズ・回転は変化しないが、インスペクタの値は親のScaleに応じて変わる
        //false → インスペクタの値は変化しないが、ワールド座標上での位置・サイズ・回転は親のScaleに応じて変わる
        obj.transform.SetParent(this.gameObject.transform, false);

        //子のワールド座標のサイズ ＝ 親のサイズ × 子のローカルサイズ
        //子のローカルサイズ ＝ 子のワールド座標のサイズ ／ 親のサイズ
        //■ ワールド座標系 → ローカル座標系の係数を作成
        Vector3 parentScaleInverse = new Vector3(1f / this.transform.localScale.x, 1f / this.transform.localScale.y, 1f / this.transform.localScale.z);

        //■ サイズと位置を変換して設定（Vector3.Scaleは要素毎に掛け算）
        obj.transform.localScale = Vector3.Scale(Asize, parentScaleInverse);
        obj.transform.localPosition = Vector3.Scale(Apos, parentScaleInverse);
    }
   */
    //斜め移動XY軸を乱数で決定する関数
    void SetPosXY()
    {
        //■ 0.01f~0.1fの範囲内の値をそれぞれの座標に格納する
        qx = UnityEngine.Random.Range(0.01f, 0.1f);
        qy = UnityEngine.Random.Range(0.01f, 0.1f);
    }

    //接触判定
    public void OnCollisionEnter(Collision collision)
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
