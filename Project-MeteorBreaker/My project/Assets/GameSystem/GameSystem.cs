using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    public int Target;

    public Text TargetText;

    private float TimeCounter;
    public float Timer;
    public Text TimeText;

    public GameObject gamecontroller;
    public GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        gamecontroller = GameObject.Find("GameStartTimer");
        _gameController = gamecontroller.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.CurrentState == GameController.PlayState.Play)
        {
            TimeCount();
            OutputText();
            FlagON();
        }
    }

    void FlagON()
    {
        if (Target <= 0)
        {
            //GameObject.Find("GameManager").GetComponent<GameManager>().clearFlag = true;
            SceneManager.LoadScene("GameClear");
        }
    }

    void TimeCount()
    {
        Timer = (TimeCounter += Time.deltaTime);
    }
    void OutputText()
    {
        TargetText.text = Target.ToString();
        TimeText.text = Timer.ToString();
    }
}
