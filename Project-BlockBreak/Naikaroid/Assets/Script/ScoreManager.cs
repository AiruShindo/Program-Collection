using UnityEngine.SocialPlatforms.Impl;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //現在のスコア
    private int score = 0;
    private Text scoreText;

    //ハイスコア
    private int highScore = 0;
    private Text highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      //シーン切り替え時も破棄しない

            // PlayerPrefsからハイスコアを読み込む
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //アクティブ時に行う関数
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //非アクティブ時に行う関数
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //シーンがロードされたタイミングでUIのTextを探して取得する（nullになってしまうので）
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
        {
            scoreText = scoreObj.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("ScoreTextが見つかりません。");
        }

        GameObject scoreHighObj = GameObject.Find("HighScoreText");
        if (scoreHighObj != null)
        {
            highScoreText = scoreHighObj.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("highScoreTextが見つかりません。");
        }

        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    //スコア加算
    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();        //スコア更新
        CheckHighScore();       //ハイスコアを超えているかチェック
    }

    //スコアリセット
    public void ResetScore()
    {
        score = 0;
    }

    //"scoreText"のUI更新
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    //"highScoreText"のUI更新
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HighScore " + highScore;
        }
    }

    //現在のスコアがハイスコアを超えているかを確認するチェック関数
    private void CheckHighScore()
    {
        //ハイスコアを超えていたらハイスコア更新を行う
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreUI();

            //PlayerPrefsに保存
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
}