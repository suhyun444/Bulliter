using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public GameObject Help;
    GameObject player;
    bool on_UI = false;
    Player playerSC;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSC = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on_UI)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(playerSC.money >= 10)
                {
                    playerSC.money -= 10;
                    AudioMGR.instance.Play_Player_Heal();
                    playerSC.Heal();

                    Destroy(gameObject);
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Help.SetActive(true);
            on_UI = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Help.SetActive(false);
            on_UI = false;
        }
    }
}
