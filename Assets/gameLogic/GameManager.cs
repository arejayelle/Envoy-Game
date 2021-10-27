using System.Collections;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    [SerializeField] private int enemiesMasked = 0;
    [SerializeField] private int enemiesKicked = 0;
    [SerializeField] private int tablesWiped = 0;
    public GameObject loseScreen;

    [Header("text ")]
    [SerializeField] TextMeshProUGUI RoundText;
    [SerializeField] TextMeshProUGUI MaskText;
    [SerializeField] TextMeshProUGUI KickText;
    [SerializeField] TextMeshProUGUI WipeText;
    // [SerializeField] TextMeshProUGUI RoundText;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator EndGame()
    {
        player.Kill();
        PopulateLoseScreen();
        yield return new WaitForSeconds(1f);
        TimeManager.Instance.Pause();
        loseScreen.SetActive(true);
    }

    private void PopulateLoseScreen()
    {
        var RoundsCompleted = RoundManager.Instance.RoundNumber - 1;


        RoundText.text = $"{RoundsCompleted}";
        MaskText.text = $"{enemiesMasked}";
        KickText.text = $"{enemiesKicked}";
        WipeText.text = $"{tablesWiped}";
    }

    public void tableCounter()
    {
        tablesWiped++;
    }
    public void maskCounter()
    {
        enemiesMasked ++;
    }
    public void unMask()
    {
        enemiesMasked --;
    }
    public void kickCounter()
    {
        enemiesKicked++;
    }
}