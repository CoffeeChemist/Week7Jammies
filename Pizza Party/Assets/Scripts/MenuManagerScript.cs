using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject b1;
 
    private readonly int ID = 0;
    private Player p;

    public bool Selected_button;
    public bool Cando;

    public float counter;
    //Audio
    private AudioSource source;
    [SerializeField] private AudioClip[] clips;


    void Start()
    {
        Selected_button = true;
        Cando = true;
        source = GetComponent<AudioSource>();
        counter = 0f;

       p = ReInput.players.GetPlayer(ID);
    }

    // Update is called once per frame
    void Update()
    {

        if (p.GetButtonDown("Action"))
        {
            if (Selected_button)
            {
                SceneManager.LoadScene(1);
            }
        }

    }
}
