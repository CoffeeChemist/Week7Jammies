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

    private AudioSource source;
    [SerializeField] private AudioClip[] clips;

    void Start()
    {
        p = ReInput.players.GetPlayer(ID);
        source = GetComponent<AudioSource>();
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
            source.PlayOneShot(clips[0]);
            SceneManager.LoadScene(1);
        }
    }
}
