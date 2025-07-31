using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public enum PlayState
    {
        None,
        Ready,
        Play,
        Finish,
    }
    public PlayState CurrentState = PlayState.None;

    //カウントダウンスタートタイム
    [SerializeField] int CountStartTime = 5;

    //それぞれのテキスト
    [SerializeField] Text CountdownText = null;
    [SerializeField] Text timerText = null;
    //カウントダウンの現在値
    float currenntCountDown = 0;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        CountdownStart();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time : 000.0s";
        //Ready
        if(CurrentState == PlayState.Ready)
        {
            currenntCountDown -= Time.deltaTime;

            int intNum = 0;

            if(currenntCountDown <= (float)CountStartTime && currenntCountDown >0)
            {
                intNum = (int)Mathf.Ceil(currenntCountDown);
                CountdownText.text = intNum.ToString();
            }
            else if(currenntCountDown  <= 0)
            {
                StartPlay();
                intNum = 0;
                CountdownText.text = "Start";

                StartCoroutine(WaitErase());
            }
        }
        else if(CurrentState == PlayState.Play)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    void CountdownStart()
    {
        currenntCountDown = (float)CountStartTime;
        SetPlayState(PlayState.Ready);
        CountdownText.gameObject.SetActive(true);
    }

    void StartPlay()
    {
        SetPlayState(PlayState.Play);
    }
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        CountdownText.gameObject.SetActive(false);
    }

    void SetPlayState(PlayState state)
    {
        CurrentState = state;
    }
}
