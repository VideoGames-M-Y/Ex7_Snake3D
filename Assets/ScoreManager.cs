using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static ScoreManager Instance { get; private set; }

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}
