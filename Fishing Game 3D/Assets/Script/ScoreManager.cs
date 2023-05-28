using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;
    //public Text highScoreText;
    //private int highScore;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
        //UpdateHighScoreText();
    }
    public int getScore ()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
        //CheckHighScore();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        //Debug.Log("Score : "+score);
    }
    public void AddScore(int x)
    {
        score += x;
        UpdateScoreText();
    }

    //public void UpdateHighScoreText()
    //{
    //    highScoreText.text = "High Score: " + highScore;
    //}

    //public void CheckHighScore()
    //{
    //    if (score > highScore)
    //    {
    //        highScore = score;
    //        PlayerPrefs.SetInt("HighScore", highScore);
    //        UpdateHighScoreText();
    //    }
    //}

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}