using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    
    Player playerSC;
    Weapon weaponSC;
    Rigidbody2D player_rigid;

    public float intro_time;
    public GameObject intro_ui;
    public GameObject boss_spr;
    public GameObject anim_boss;

    int last_type = -1;
    public int move_amount;
    public float att_time;
    GameObject Player;
    public GameObject Bullet;
    public Transform FirePosition;
    [Header("boomerang")]
    public GameObject BoomBullet;
    public float Bullet_Speed_Min;
    public float Bullet_Speed_Max;

    [Header("CirCleChase")]
    public float CirCleRange;
    public float Bullet_Speed;

    [Header("SideToSide")]
    public float Side_Speed;
    public float start_Yposition;
    float[] Right_dy = { 3, 4, 5, 9, 10, 11 };
    public float Right_dx;
    float[] Left_dy = { 0, 1, 2, 6, 7, 8, 12, 13, 14 };
    public float Left_dx;
    [Header("turn")]
    public float turn_speed;
    bool left = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerSC = Player.GetComponent<Player>();
        weaponSC = Player.GetComponent<Weapon>();
        player_rigid = Player.GetComponent<Rigidbody2D>();
        playerSC.intro_time = true; weaponSC.intro_time = true; player_rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        Invoke("Think", att_time + intro_time);
        AudioMGR.instance.Play_OWO_Intro();
        intro_ui.SetActive(true);
        boss_spr.SetActive(false);
        anim_boss.SetActive(true);
        Invoke("HideIntro", intro_time);
    }

    void HideIntro()
    {
        playerSC.intro_time = false;
        weaponSC.intro_time = false;
        player_rigid.constraints = RigidbodyConstraints2D.None;
        player_rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        intro_ui.SetActive(false);
        boss_spr.SetActive(true);
        anim_boss.SetActive(false);
    }
    void Think()
    {
        int type = Random.Range(1, 5);
        while (type == last_type) type = Random.Range(1, 4);
        if (type == 1)
        {
            StartCoroutine("SideToSide");
            AudioMGR.instance.Play_OWO_Attack2();
        }
        else if (type == 2)
        {
            StartCoroutine("CirCleChase");
        }
        else if (type == 3)
        {
            StartCoroutine("Boomerang");
            AudioMGR.instance.Play_OWO_Attack2();
        }
        else if(type == 4)
        {
            AudioMGR.instance.Play_OWO_Attack1();
            boss_spr.SetActive(false);
            anim_boss.SetActive(true);
            anim_boss.GetComponent<Animator>().Play("OwoTP");
            Invoke("turn", turn_speed / 2);
        }
        last_type = type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void turn()
    {
        AudioMGR.instance.Play_OWO_Attack1();
        if (left)
        {
            left = false;
            transform.position = new Vector3(transform.position.x + move_amount, transform.position.y, 0);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Invoke("setactive", turn_speed);
        }
        else
        {
            left = true;
            transform.position = new Vector3(transform.position.x - move_amount, transform.position.y, 0);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Invoke("setactive", turn_speed);
        }
        
    }
    void setactive()
    {
        boss_spr.SetActive(true);
        anim_boss.SetActive(false);
        Invoke("Think", att_time);
    }
    IEnumerator SideToSide()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int t = 0; t < 5; t++)
            {
                for (int i = 0; i < 6; i++)
                {
                    EnemyBullet tmp = Instantiate(Bullet, new Vector3(Right_dx, Right_dy[i] + start_Yposition, 0), Quaternion.identity).GetComponent<EnemyBullet>();
                    tmp.Penetrate = true;
                    tmp.speed = Side_Speed;
                    tmp.direction = 180;
                }
                for (int i = 0; i < 9; i++)
                {
                    EnemyBullet tmp = Instantiate(Bullet, new Vector3(Left_dx, Left_dy[i] + start_Yposition, 0), Quaternion.identity).GetComponent<EnemyBullet>();
                    tmp.Penetrate = true;
                    tmp.speed = Side_Speed;
                    tmp.direction = 0;
                }
                yield return new WaitForSeconds(0.08f);
            }
            yield return new WaitForSeconds(0.7f);
        }

        Invoke("Think", att_time);
        yield break;
    }
    IEnumerator CirCleChase()
    {
        EnemyBullet[] EB = new EnemyBullet[24];
        for (int t = 0; t < 3; t++)
        {
            AudioMGR.instance.Play_OWO_Attack3();
            for (int i = 0; i < 360; i += 15)
            {
                EB[i / 15] = Instantiate(Bullet, FirePosition.position, Quaternion.identity).GetComponent<EnemyBullet>();
                EB[i / 15].speed = CirCleRange;
                EB[i / 15].direction = i + 90;
                StartCoroutine("setSPD", EB[i / 15]);
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 24; i++)
            {
                if(EB[i] != null)
                {
                    float angle = Mathf.Atan2(Player.transform.position.y - EB[i].gameObject.transform.position.y, Player.transform.position.x - EB[i].gameObject.transform.position.x) * Mathf.Rad2Deg;
                    EB[i].direction = angle + Random.Range(-8, 8);
                    EB[i].speed = Bullet_Speed;
                    EB[i].chase();
                }
            }
        }

        Invoke("Think", att_time);
    }
    IEnumerator setSPD(EnemyBullet eb)
    {
        yield return new WaitForSeconds(0.3f);
        if(eb != null)
        {
            eb.speed = 0;
        }
    }
    IEnumerator Boomerang()
    {
        PoisonBullet[] PB = new PoisonBullet[3];
        for (int i = 0; i < 3; i++)
        {
            PB[i] = Instantiate(BoomBullet, FirePosition.position, Quaternion.identity).GetComponent<PoisonBullet>();
            if(left)
                PB[i].direction = Random.Range(-5, 5);
            else if(!left)
                PB[i].direction = 180 + Random.Range(-5, 5);
            PB[i].speed = Random.Range(Bullet_Speed_Min, Bullet_Speed_Max);
            PB[i].penetrate = true;
            PB[i].setGameObject(Bullet);
            yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
        }
        yield return new WaitForSeconds(1);
        AudioMGR.instance.Play_OWO_Toout();
        for (int i = 0; i < 3; i++)
        {
            PB[i].Boom();
            yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
        }
        yield return new WaitForSeconds(1);
        AudioMGR.instance.Play_OWO_Toin();
        for (int i = 0; i < 3; i++)
        {
            PB[i].ReCall();
            yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
        }
        yield return new WaitForSeconds(1);
        AudioMGR.instance.Play_OWO_Toout();

        Invoke("Think", att_time);
    }
}
