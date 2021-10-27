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
        TimeManager.Pause();
        loseScreen.SetActive(true);
    }

    private void PopulateLoseScreen()
    {
        var RoundsCompleted = RoundManager.Instance.RoundNumber - 1;


        RoundText.text = $"Rounds Completed: {RoundsCompleted}";
        MaskText.text = $"People Masked: {enemiesMasked}";
        KickText.text = $"People Kicked out: {enemiesKicked}";
        WipeText.text = $"Tables Wiped: {tablesWiped}";
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