using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public Transform credits;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        credits.position = new Vector3(credits.position.x, credits.position.y + 0.001f, credits.position.z);
        if (credits.localPosition.y > 700f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
