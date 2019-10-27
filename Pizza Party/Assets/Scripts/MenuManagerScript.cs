using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject b1;
    
    public GameObject b2;
 
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

        if (p.GetAxis("VerticalStick") != 0 && Cando)
        {
            Selected_button = !Selected_button;
            Cando = false;
            source.PlayOneShot(clips[0]);
        }

        if (Selected_button)
        {
            b1.SetActive(true);
            b2.SetActive(false);
            
            //bug.Log("yes");
        }
        else
        {
            b1.SetActive(false);
            b2.SetActive(true);
            
            //Debug.Log("no");
        }

        if (p.GetButtonDown("Action"))
        {
            if (Selected_button)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                Application.Quit();
            }
        }

        if(!Cando)
        {
            counter += Time.deltaTime;
            if (counter > 1)
            {
                Cando = true;
                counter = 0f;
            }
        }

    }
}
