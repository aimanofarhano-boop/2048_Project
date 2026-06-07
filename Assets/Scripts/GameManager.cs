using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI goldText;

    private int score;
    private int gold;

    public int Gold { get { return gold; } }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        SetGold(20);
        highScoreText.text = LoadHiScore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    public void AddGold(int amount)
    {
        SetGold(gold + Mathf.Max(0, amount));
    }

    public bool SpendGold(int amount)
    {
        if (amount <= 0 || amount > gold)
            return false;

        SetGold(gold - amount);
        return true;
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiScore();
    }

    private void SetGold(int gold)
    {
        this.gold = Mathf.Max(0, gold);
        goldText.text = this.gold.ToString();

        SaveGold();
    }

    private void SaveHiScore()
    {
        int hiScore = LoadHiScore();

        if (score > hiScore)
        {
            PlayerPrefs.SetInt("hiScore", score);
        }
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt("gold", gold);
    }

    private int LoadHiScore()
    {
        return PlayerPrefs.GetInt("hiScore", 0);
    }

    private int LoadGold()
    {
        return PlayerPrefs.GetInt("gold", 0);
    }
}
