using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //これは軌道を申告する矢印の処理
    //正確には親オブジェクトの敵の斜め下に子オブジェクトとして存在しているのみである。

    //ここでは接触判定・重力の無効化を行う

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //地面もしくは建物に接触した際は、flagをオンにする
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "tatemono")
        {
            Destroy(this.gameObject);
        }
    }

}
