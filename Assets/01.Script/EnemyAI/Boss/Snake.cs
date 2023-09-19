using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    GameObject Player;
    Player playerSC;
    Weapon weaponSC;
    Rigidbody2D player_rigid;
    public GameObject Bullet;
    public GameObject fireposition;

    public Animator anim;
    string current_state;

    public float intro_time;
    public GameObject intro_ui;
    public GameObject snake_spr;
    public GameObject intro_snake;
    public float att_time;

    [Header("poison")]
    public GameObject Poison_Bullet;
    public float Poison_bullet_time;
    public float Poison_Bullet_Speed;

    [Header("shouting")]
    public int rain_time;
    public float rain_speed;
    public float rain_rate_min;
    public float rain_rate_max;
    float r_y = 10;
    float[] r_x = {-13,-10,-7,-4,-1,1,4,7,10,13};
    

    [Header("Spin")]
    public int spin_time;
    public float spin_Speed;
    public float bullet_speed;

    int last_type=-1;

    //float time = 2;
    //float atk_cool = 4;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerSC = Player.GetComponent<Player>();
        weaponSC = Player.GetComponent<Weapon>();
        player_rigid = Player.GetComponent<Rigidbody2D>();
        playerSC.intro_time = true; weaponSC.intro_time = true;player_rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        Invoke("Think", att_time + intro_time);
        Invoke("Scream", intro_time / 2);
        intro_ui.SetActive(true);
        snake_spr.SetActive(false);
        intro_snake.SetActive(true);
        Invoke("HideIntro", intro_time);
        //StartCoroutine("shouting");
        //StartCoroutine("Spin");
        //StartCoroutine("poison");
    }
    void Scream()
    {
        AudioMGR.instance.Play_Snake_Intro();
    }
    // Update is called once per frame
    void Update()
    {
        //time -= Time.deltaTime;

    }
    void Think()
    {
        int type = Random.Range(1, 4);
        while(type == last_type) type = Random.Range(1, 4);
        if (type == 1)
        {
            StartCoroutine("shouting");
        }
        else if(type == 2)
        {
            StartCoroutine("spin");
        }
        else if(type == 3)
        {
            StartCoroutine("poison");
        }
        last_type = type;
    }
    IEnumerator spin()
    {
        AnimationState("SnakeHead2");
        AudioMGR.instance.Play_Snake_Spin();
        int cnt = spin_time;
        while(cnt >= 0)
        {
            fireposition.transform.Rotate(Vector3.forward * spin_Speed * 100);
            EnemyBullet temp1 = Instantiate(Bullet, fireposition.transform.position, fireposition.transform.rotation).GetComponent<EnemyBullet>();temp1.speed = bullet_speed; temp1.Penetrate = true;
            EnemyBullet temp2 = Instantiate(Bullet, fireposition.transform.position, fireposition.transform.rotation).GetComponent<EnemyBullet>();temp2.speed = bullet_speed; temp2.Penetrate = true;
            EnemyBullet temp3 = Instantiate(Bullet, fireposition.transform.position, fireposition.transform.rotation).GetComponent<EnemyBullet>();temp3.speed = bullet_speed; temp3.Penetrate = true;
            EnemyBullet temp4 = Instantiate(Bullet, fireposition.transform.position, fireposition.transform.rotation).GetComponent<EnemyBullet>();temp4.speed = bullet_speed; temp4.Penetrate = true;
            temp1.direction = fireposition.transform.eulerAngles.z;
            temp2.direction = fireposition.transform.eulerAngles.z+90;
            temp3.direction = fireposition.transform.eulerAngles.z+180;
            temp4.direction = fireposition.transform.eulerAngles.z+270;
            cnt--;
            yield return new WaitForSeconds(0.1f);
        }
        AnimationState("SnakeHead1");
        Invoke("Think", att_time);
        yield break;
    }
    IEnumerator shouting()
    {
        AnimationState("SnakeHead2");
        AudioMGR.instance.Play_Snake_Rain();
        int cnt = rain_time;
        while(cnt >= 0)
        {
            for(int i = 0; i < 10; i++)
            {
                int index = Random.Range(0, 10);
                EnemyBullet tmp = Instantiate(Bullet, new Vector3(r_x[index], r_y, 0), Quaternion.identity).GetComponent<EnemyBullet>();
                if(Random.Range(0,100) <= 10)
                    tmp.chase_player = true;
                else
                    tmp.chase_player = false;
                tmp.Penetrate = true;
                tmp.speed = rain_speed;
                tmp.direction = -90;
                
                yield return new WaitForSeconds(Random.Range(rain_rate_min, rain_rate_max));
                r_x[i] += Random.Range(-1f, 1f);
            }
            cnt--;
            yield return null;
        }
        AnimationState("SnakeHead1");
        Invoke("Think", att_time);
        yield break;
    }
    IEnumerator poison()
    {
        AnimationState("SnakeHead2");
        for (int i = 0; i < 5; i++)
        {
            AudioMGR.instance.Play_Snake_Poison();
            PoisonBullet tmp = Instantiate(Poison_Bullet,fireposition.transform.position,Quaternion.identity).GetComponent<PoisonBullet>();
            tmp.speed = Poison_Bullet_Speed;
            tmp.direction = i * 20 - 130;
            tmp.penetrate = true;
            tmp.setGameObject(Bullet);
            yield return new WaitForSeconds(Poison_bullet_time);
        }
        AnimationState("SnakeHead1");
        Invoke("Think", att_time);
        yield break;
    }
    //need rewark
    /*
    IEnumerator dash()
    {
        transform.position = new Vector3(-13.5f, transform.position.y, 0);
        Vector3 target = new Vector3(transform.position.x + 30, transform.position.y, 0);
        while(Vector3.Distance(target,transform.position) >= 1)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, dash_speed);

            fireposition.transform.Rotate(Vector3.forward * rot_Speed * 100 * Time.deltaTime);
            EnemyBullet temp = Instantiate(Bullet,fireposition.transform.position,fireposition.transform.rotation).GetComponent<EnemyBullet>();
            temp.speed = bullet_speed;

            temp.direction = fireposition.transform.eulerAngles.z;
            yield return new WaitForSeconds(0.05f);
        }
        yield break;

    }*/

    void HideIntro()
    {
        playerSC.intro_time = false;
        weaponSC.intro_time = false;
        player_rigid.constraints = RigidbodyConstraints2D.None;
        player_rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        intro_ui.SetActive(false);
        snake_spr.SetActive(true);
        intro_snake.SetActive(false);
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
}
