using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class MenuMove : MonoBehaviour
{
    private readonly int ID = 0;
    private Player p;

    private bool hasPressedActionKey = false;

    void Start()
    {
        p = ReInput.players.GetPlayer(ID);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if key gets pressed
        if (!hasPressedActionKey)
        {
            hasPressedActionKey = p.GetButtonDown("Action");
        }

        if (hasPressedActionKey)
        {
            SceneManager.LoadScene(1);
        }
    }
}
