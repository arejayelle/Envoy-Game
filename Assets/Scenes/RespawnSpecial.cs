using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSpecial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("SpecialMode");
    }
    
}
