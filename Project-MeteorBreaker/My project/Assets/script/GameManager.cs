using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ゲーム終了を知らせるフラグ（初期はfalse）
    public bool bFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ゲームオーバー画面移行関数
    public void IsEnd(bool bFlag)
    {
        if (bFlag == true)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

}
