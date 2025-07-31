using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ground : MonoBehaviour
{
    public int gNum;        //地面の種類によって判定分けを行う変数(1=建物、2=地面)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void ChangeScene()
    //{
    //    gMng.IsEnd(true);
    //}

    //接触判定
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && gNum == 1)
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy" && gNum == 2)
        {
            //ChangeScene();
            SceneManager.LoadScene("GameOver");
        }
    }
}
