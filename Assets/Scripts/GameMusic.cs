using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private static GameMusic myGameMusic;

    void Awake()
    {
        if (myGameMusic == null)
        {
            myGameMusic = this;
            DontDestroyOnLoad(myGameMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
