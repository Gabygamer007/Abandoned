using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceScreen : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoixBalles()
    {
        PlayerPrefs.SetString("choix", "balles");
        SceneManager.LoadScene(3);
    }
    public void ChoixVie()
    {
        PlayerPrefs.SetString("choix", "vie");
        SceneManager.LoadScene(3);
    }
}
