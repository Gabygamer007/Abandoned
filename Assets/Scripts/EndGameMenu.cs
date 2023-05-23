using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    public Transform credits;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        credits.position = new Vector3(credits.position.x, credits.position.y + 0.005f, credits.position.z);
    }
}
