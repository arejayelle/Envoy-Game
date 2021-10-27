using TMPro;
using UnityEngine;


public enum ScoreType
{
    EnemyMasked,
    EnemyKicked,
    EnemyInfected,
    EnemyDied,
    TableWiped,
    TableInfected,
    SocialDistancing,
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    [Header("Display")]
    [SerializeField] TextMeshProUGUI scoreText;

    private int mScore = 0;

    public int GetScore()
    {
        return mScore;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"Score: {mScore}";
    }

    public void GainPoint(ScoreType type, int points =0)
    {
        mScore += getPoints(type, points);
        scoreText.text = $"Score: {mScore}";
        if (mScore <= -20) StartCoroutine(GameManager.instance.EndGame());
    }

    int getPoints(ScoreType type,int points)
    {
        switch (type)
        {
                case ScoreType.EnemyMasked:
                    if (points == 0) return 1;
                    return points;
                case ScoreType.EnemyKicked:
                    return 1;
                case ScoreType.EnemyInfected:
                    return -2;
                case ScoreType.EnemyDied:
                    return -5;
                
                case ScoreType.TableWiped:
                    return 2;
                case ScoreType.TableInfected:
                    return -1;
                
                case ScoreType.SocialDistancing:
                    return 5;
                default:
                    return 0;
            
        }
    }
}
