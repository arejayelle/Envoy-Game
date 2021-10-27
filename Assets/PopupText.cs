using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    
    public static PopupText Create(string message)
    {
        var ts = Instantiate(GameAssetManager.i.popupText, Vector3.zero, Quaternion.identity);
        var pop = ts.GetComponent<PopupText>();
        pop.SetValue(message);
        return pop;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        scoreText.text = "";


    }

    // Update is called once per frame
    public void SetValue(string message )
    {
        scoreText.text = message;
    }
}
