using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    bool can_close = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<Player>().on_Help_UI = true;
        GameObject.Find("Player").GetComponent<Weapon>().on_Pause = true;
        GameObject.Find("Player").GetComponent<Player>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("set_close", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (can_close)
        {
            if (Input.anyKeyDown)
            {
                GameObject.Find("Player").GetComponent<Player>().on_Help_UI = false;
                GameObject.Find("Player").GetComponent<Weapon>().on_Pause = false;
                GameObject.Find("Player").GetComponent<Player>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Destroy(gameObject);
            }
        }
    }
    void set_close()
    {
        can_close = true;
    }
}
