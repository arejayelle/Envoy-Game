using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTextUpdater : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] TextMeshProUGUI WaveText;

    private WaveSpawner waveSpawner;
    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = transform.GetComponent<WaveSpawner>();
        
        WaveText.text = $"Wave {waveSpawner.WaveIndex +1}";
    }

    public void UpdateText()
    {
        WaveText.text = $"Wave {waveSpawner.WaveIndex +1}";
    }
}
