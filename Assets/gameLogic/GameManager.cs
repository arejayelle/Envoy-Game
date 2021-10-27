using System.Collections;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    
    public GameObject loseScreen;

    [Header("text ")]
    [SerializeField] TextMeshProUGUI RoundText;
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
        var MasksUsed = 420;
        var TablesWiped = 0;

        RoundText.text = $"Rounds Completed: {RoundsCompleted}";
    }
}