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
    MissedOut
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
        
        if (TimeManager.Instance.IsBulletTime)
        {
            mScore += getBulletTimePoints(type, points);

        }
        else
        {
            mScore += getPoints(type, points);
        }
        scoreText.text = $"Score: {mScore}";
        if (mScore <= -20) StartCoroutine(GameManager.instance.EndGame());
    }

    int getPoints(ScoreType type,int points)
    {
        switch (type)
        {
                case ScoreType.EnemyMasked:
                    if (points == 0)
                    {
                        PopupText.Create($"Enemy Masked! +1");
                        return 1;
                    }

                    PopupText.Create($"Multi-Mask bonus! +{points}");
                    return points;
                
                case ScoreType.EnemyKicked:
                    PopupText.Create($"Kicked Out! +1");
                    return 1;
                case ScoreType.EnemyInfected:
                    PopupText.Create($"Person Infected! -2");
                    return -2;
                case ScoreType.EnemyDied:
                    PopupText.Create($"Person Died! -5");
                    return -5;
                
                case ScoreType.TableWiped:
                    PopupText.Create($"Table wiped! +2");
                    return 2;
                case ScoreType.TableInfected:
                    PopupText.Create($"Table Infected! -1");
                    return -1;
                
                case ScoreType.SocialDistancing:
                    PopupText.Create($"Social Distancing Bonus! +5");
                    return 5;
                default:
                    return 0;
            
        }
    }
    int getBulletTimePoints(ScoreType type,int points)
    {
        switch (type)
        {
                case ScoreType.EnemyMasked:
                    if (points == 0) return 1;
                    return points-1; // no bonus point in bulletTime
                case ScoreType.EnemyKicked:
                case ScoreType.TableWiped:
                    return 1;
                
                
                case ScoreType.EnemyInfected:
                case ScoreType.EnemyDied:
                case ScoreType.TableInfected:
                case ScoreType.MissedOut:
                    return -2;
                    
                default:
                    return 0;
            
        }
    }
}
