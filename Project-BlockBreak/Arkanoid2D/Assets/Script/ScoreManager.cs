using UnityEngine.SocialPlatforms.Impl;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //���݂̃X�R�A
    private int score = 0;
    private Text scoreText;

    //�n�C�X�R�A
    private int highScore = 0;
    private Text highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      //�V�[���؂�ւ������j�����Ȃ�

            // PlayerPrefs����n�C�X�R�A��ǂݍ���
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //�A�N�e�B�u���ɍs���֐�
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //��A�N�e�B�u���ɍs���֐�
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //�V�[�������[�h���ꂽ�^�C�~���O��UI��Text��T���Ď擾����inull�ɂȂ��Ă��܂��̂Łj
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
        {
            scoreText = scoreObj.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("ScoreText��������܂���B");
        }

        GameObject scoreHighObj = GameObject.Find("HighScoreText");
        if (scoreHighObj != null)
        {
            highScoreText = scoreHighObj.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("highScoreText��������܂���B");
        }

        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    //�X�R�A���Z
    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();        //�X�R�A�X�V
        CheckHighScore();       //�n�C�X�R�A�𒴂��Ă��邩�`�F�b�N
    }

    //�X�R�A���Z�b�g
    public void ResetScore()
    {
        score = 0;
    }

    //"scoreText"��UI�X�V
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    //"highScoreText"��UI�X�V
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HighScore " + highScore;
        }
    }

    //���݂̃X�R�A���n�C�X�R�A�𒴂��Ă��邩���m�F����`�F�b�N�֐�
    private void CheckHighScore()
    {
        //�n�C�X�R�A�𒴂��Ă�����n�C�X�R�A�X�V���s��
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreUI();

            //PlayerPrefs�ɕۑ�
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
}