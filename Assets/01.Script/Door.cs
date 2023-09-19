using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool is_open;
    public Animator anim;
    string current_state;
    public GameObject tool_tip;
    BoxCollider2D bc2d;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        tool_tip.SetActive(false);
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.enabled = false;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemys = FindGameObjectsInLayer(LayerMask.NameToLayer("Enemy"));
        if(enemys != null) is_open = false;
        else is_open = true;

        if (is_open)
        {
            AnimationState("DoorOpen");
            if(!bc2d.enabled)
            bc2d.enabled = true;
        }
        else
        {
            AnimationState("DoorIdle");
        }

        if(tool_tip.activeSelf && !player.on_Help_UI)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                AudioMGR.instance.Play_Player_EnterGate();
                GetComponentInParent<RandomMapGenerator>().Generate(-1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && is_open)
        {
            tool_tip.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tool_tip.SetActive(false);
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
}

