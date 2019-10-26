using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Is_Selected = false;
    public int type = 1;
    public GameObject child;
    public Transform tr;
    private bool hasPressedActionKey = false;
    private readonly int ID = 0;
    private Player p;
    
    

    void Start()
    {
        tr = child.transform;
        p = ReInput.players.GetPlayer(ID);
     

    }

    // Update is called once per frame
    void Update()
    {
        hasPressedActionKey = p.GetButtonDown("Action");

        if (type == 1)
        {
            Is_Selected = GameObject.Find("MainMenuGame").GetComponent<MenuManagerScript>().b1_bool;

        }
        else
        {

            Is_Selected = GameObject.Find("MainMenuGame").GetComponent<MenuManagerScript>().b2_bool;


        }



        if (Is_Selected)
        {
            tr.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            

        }
        else
        {
            tr.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        }




        if (hasPressedActionKey && Is_Selected)
        {
            if (type == 1)
            {
                Debug.Log("Yo You loadin");
                SceneManager.LoadScene("ReadyUpMenu", LoadSceneMode.Additive);
            }
            else
            {
                Debug.Log("Yo You exitin");
                Application.Quit();
            }
        }
    }
}

