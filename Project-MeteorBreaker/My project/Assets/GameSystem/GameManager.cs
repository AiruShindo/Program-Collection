using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool bFlag;
    public bool clearFlag;
    public bool GameStart;

    // Start is called before the first frame update
    void Start()
    {
        bFlag = false;
        clearFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IsCrear(bool clearFlag)
    {
        if (clearFlag == true)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
    public void IsEnd(bool bFlag)
    {
        if (bFlag == true)
        {
            GetComponent<AudioSource>().Play();
            SceneManager.LoadScene("GameOver");
        }
    }

}
