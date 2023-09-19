using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMapGenerator : MonoBehaviour
{
    Player player;
    public GameObject starting_room;
    public GameObject[] lv_1_rooms;
    public GameObject[] lv_2_rooms;
    public GameObject[] lv_3_rooms;
    public GameObject[] boss_rooms;
    public GameObject[] merchant_rooms;
    public GameObject curr_room;
    int last_room_id = -1;
    public int room_count = 0;
    public GameObject[] do_not_des;
    AudioSource audio;
    bool on_Death_BGM = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audio = GetComponent<AudioSource>();
        Generate(-1);
    }
    public AudioClip BGM_Stage1;
    public AudioClip BGM_Snake;
    public AudioClip BGM_Stage2;
    public AudioClip BGM_OWO;
    public AudioClip BGM_Stage3;
    public AudioClip BGM_Slav;
    public AudioClip BGM_Deadth;
    // Update is called once per frame
    void Update()
    {
        if (player.dead && !on_Death_BGM)
        {
            audio.Stop();
            audio.clip = BGM_Deadth;
            audio.Play();
            on_Death_BGM = true;
        }
        //if(Input.GetKeyDown(KeyCode.R)) Generate(-1);
    }

    public void Generate(int room_id)
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            bool dis = true;
            foreach (GameObject dnd in do_not_des)
                if (o == dnd) dis = false;
            if(o.transform.parent != null) dis = false;
            if (dis) Destroy(o);
        }


        if (curr_room != null) Destroy(curr_room);

        int id = room_id;

        GameObject s_room = null;
        if (room_count == 0)
        {
            Camera.main.transform.GetComponent<BackgroundChanger>().ChangeBackground(0);
            audio.clip = BGM_Stage1;
            audio.Play();
            s_room = starting_room;
        }
        else if(room_count > 0 && room_count <= 5)
        {
            if(room_count == 1)
            {
                PlayerPrefs.SetInt("Play_Tutorial", 1);
            }
            
            if (room_count == 3)
                s_room = merchant_rooms[0];
            else if (room_count == 5)
            {
                audio.clip = BGM_Snake;
                audio.Play();
                s_room = boss_rooms[0];
            }
            else
            {
                id = Random.Range(0, lv_1_rooms.Length);
                while (id == last_room_id) id = Random.Range(0, lv_1_rooms.Length);
                s_room = lv_1_rooms[id];
            }
        }
        else if (room_count > 5 && room_count <= 10)
        {
            Camera.main.transform.GetComponent<BackgroundChanger>().ChangeBackground(1);
            if(room_count == 6)
            {
                audio.clip = BGM_Stage2;
                audio.Play();
            }
            if (room_count == 8)
                s_room = merchant_rooms[1];
            else if (room_count == 10)
            {
                audio.clip = BGM_OWO;
                audio.Play();
                s_room = boss_rooms[1];
            }
            else
            {
                id = Random.Range(0, lv_2_rooms.Length);
                while (id == last_room_id) id = Random.Range(0, lv_2_rooms.Length);
                s_room = lv_2_rooms[id];
            }
        }
        else if (room_count > 10 && room_count <= 15)
        {
            Camera.main.transform.GetComponent<BackgroundChanger>().ChangeBackground(2);
            if (room_count == 11)
            {
                audio.clip = BGM_Stage3;
                audio.Play();
            }
            if (room_count == 13)
                s_room = merchant_rooms[2];
            else if (room_count == 15)
            {
                audio.clip = BGM_Slav;
                audio.Play();
                s_room = boss_rooms[2];
            }
            else
            {
                id = Random.Range(0, lv_3_rooms.Length);
                while (id == last_room_id) id = Random.Range(0, lv_3_rooms.Length);
                s_room = lv_3_rooms[id];
            }
        }
        else
        {
            SceneManager.LoadScene(4);
            //end of game
        }
        if(s_room != null)
        {
            curr_room = Instantiate(s_room, transform.position, Quaternion.identity, transform);
        }

        last_room_id = id;

        room_count++;
    }
}
