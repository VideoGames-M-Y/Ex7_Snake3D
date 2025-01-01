using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private string sceneName;
    public static HPManager Instance { get; private set; }

    private int HP =1;

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
        HPText.text = "HP: " + HPManager.Instance.GetHP();
        if (HP <= 0)
        {
            Debug.Log("Game Over");
            //SceneManager.LoadScene(sceneName);
            GameObject.Find("Player").GetComponent<CharacterForwardMover>().stop();
        }
    }

    public void AddHP(int value)
    {
        HP += value;
        Debug.Log("Score: " + HP);
    }

    public int GetHP()
    {
        return HP;
    }

    public void LoseHP()
    {
        HP--;
        if (HP <= 0)
        {
            Debug.Log("Game Over");
            //SceneManager.LoadScene("GameOver");
            GameObject.Find("Player").GetComponent<CharacterForwardMover>().stop();
        }
    }
}
