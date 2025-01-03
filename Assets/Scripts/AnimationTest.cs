using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator an;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        if(an != null)
        {
            PlayerManager.gameStarted = false;
            Time.timeScale = 1;
        }
    }
}
