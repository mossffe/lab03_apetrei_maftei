using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreLabel;

    static GameManager instance = null;
    int score = 0;

    private GameManager()
    {
    }

    void Awake()
    {
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        UpdateScoreLabel();
    }

    public void AddPlayerScore(int value)
    {    
        score += value;
        UpdateScoreLabel();
    }

    void UpdateScoreLabel()
    {
        scoreLabel.text = string.Format("Score: {0}", score);
    }
}
