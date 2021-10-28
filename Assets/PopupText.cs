using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] static Transform mPosition;
    private float textSpeed = 5f;
    private float disappearTime = 1f;
    private float disappearSpeed = 3f;
    private Color textColour;
    
    
    public static PopupText Create(string message, bool isGain = false)
    {
        var ts = Instantiate(GameAssetManager.i.popupText, Vector3.up, Quaternion.identity);
        var pop = ts.GetComponent<PopupText>();
        pop.SetValue(message, isGain);
        return pop;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        scoreText.text = "";
    }

    public void SetValue(string message, bool isGain)
    {
        if (isGain)
        {
            textColour = scoreText.color;
        }
        else
        {
            textColour = Color.red;
        }
        
        scoreText.color = textColour;
        scoreText.text = message;
    }

    private void Update()
    {
        transform.position += new Vector3(0, textSpeed) * Time.deltaTime;
        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            textColour.a -= disappearSpeed * Time.deltaTime;
            scoreText.color = textColour;
            if(textColour.a <0)
                Destroy(gameObject);
        }
    }


    public static void SetPosition(Transform transformPosition)
    {
        mPosition = transformPosition;
    }
}
