using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class SkipIntro : MonoBehaviour
{
    private Player p;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        p = ReInput.players.GetPlayer(0);
        timer = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (p.GetButtonDown("Action"))
        {
            SceneManager.LoadScene(5);

        }

        if (timer ==0)
        {

            SceneManager.LoadScene(5);
        }
        timer--;

    }
}
