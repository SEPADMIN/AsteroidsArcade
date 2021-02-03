using UnityEngine;
using UnityEngine.UI;

public static class UIController
{
    public static GameObject Panel { get; private set; }
    private const string FinalScoreBaseText = "Your score: ";
    private static Text _scoreText;
    private static Text _finalScoreText;
    private static Image[] Lives = new Image[4];

    public static void Reload()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _finalScoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        Panel = GameObject.Find("GameOverPanel");
        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i] = GameObject.Find("Life" + (i + 1)).GetComponent<Image>();
        }
        Reset();
    }

    private static void Reset()
    {
        Panel.SetActive(false);
        _scoreText.text = "00";
        SetLives(Player.Lives);
    }

    public static void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }
    
    public static void SetFinalScore(int score)
    {
        _finalScoreText.text = FinalScoreBaseText + score.ToString();
    }

    public static void SetLives(int lives)
    {
        for (int i = 0; i < lives; i++)
        {
            Lives[i].enabled = true;
        }
        for (int i = lives; i < Lives.Length; i++)
        {
            Lives[i].enabled = false;
        }
    }

    public static void SetPanelActive(bool flag)
    {
        Panel.SetActive(flag);
    }
}
