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
    public TextMeshProUGUI attackValueText;

    private int score;
    private int gold;
    private int attackValue;
    private Enemy currentEnemy;
    private Coroutine enemyDamageCoroutine;

    public int Gold { get { return gold; } }

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    private void Start()
    {
        NewGame();
        SpawnEnemy();
    }

    public void NewGame()
    {
        SetScore(0);
        SetGold(20);
        SetAttackValue(4);
        highScoreText.text = LoadHiScore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

        UpdateAttackValue();
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

    public void UpdateAttackValue()
    {
        SetAttackValue(board.GetAttackValue());
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || currentEnemy != null)
        {
            return;
        }

        Vector3 spawnPosition = enemySpawnPoint != null ? enemySpawnPoint.position : transform.position;
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            enemy = enemyObject.AddComponent<Enemy>();
        }

        enemy.Defeated += OnEnemyDefeated;
        currentEnemy = enemy;

        StopEnemyDamageCoroutine();
        enemyDamageCoroutine = StartCoroutine(EnemyDamageTick());
    }

    private void OnEnemyDefeated(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Defeated -= OnEnemyDefeated;
        }

        currentEnemy = null;
        StopEnemyDamageCoroutine();
        SpawnEnemy();
    }

    private IEnumerator EnemyDamageTick()
    {
        while (currentEnemy != null)
        {
            yield return new WaitForSeconds(1f);
            if (currentEnemy != null && attackValue > 0)
            {
                currentEnemy.TakeDamage(attackValue);
            }
        }
    }

    private void StopEnemyDamageCoroutine()
    {
        if (enemyDamageCoroutine != null)
        {
            StopCoroutine(enemyDamageCoroutine);
            enemyDamageCoroutine = null;
        }
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

    private void SetAttackValue(int attackValue)
    {
        this.attackValue = Mathf.Max(0, attackValue);
        attackValueText.text = this.attackValue.ToString();
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
