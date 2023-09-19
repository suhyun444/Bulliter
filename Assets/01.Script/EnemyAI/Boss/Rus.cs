using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rus : MonoBehaviour
{
    GameObject Player;
    Player playerSC;
    Weapon weaponSC;
    Rigidbody2D player_rigid;

    public Animator anim;
    string current_state;

    public float intro_time;
    public GameObject intro_ui;
    public GameObject boss_spr;
    public GameObject intro_boss;

    public float att_time;
    public GameObject Bullet;
    public Transform FirePosition;
    [Header("Grenade")]
    public GameObject Grenade;
    [Header("AK47")]
    public float Bullet_Speed;
    public float ak_time;
    public float ak_shoot_time;
    public GameObject AK47_Left;
    public Transform Left_FirePosition;
    public GameObject AK47_Right;
    public Transform Right_FirePosition;
    [Header("Missile")]
    public GameObject Missile;
    public float missile_Speed;
    public GameObject missile_left;
    public Transform missile_left_FirePosition;
    public GameObject missile_right;
    public Transform missile_right_FirePosition;
    public GameObject EndingDialog;
    public GameObject Ending_BGM;
    int last_type = -1;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerSC = Player.GetComponent<Player>();
        weaponSC = Player.GetComponent<Weapon>();
        player_rigid = Player.GetComponent<Rigidbody2D>();
        playerSC.intro_time = true; weaponSC.intro_time = true; player_rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        Invoke("Think", att_time + intro_time);
        Invoke("Scream", intro_time / 2);
        intro_ui.SetActive(true);
        boss_spr.SetActive(false);
        intro_boss.SetActive(true);
        Invoke("HideIntro", intro_time);
        InvokeRepeating("ShootAk", 0f, ak_shoot_time);
        //StartCoroutine("missile");
    }

    void Think()
    {
        int type = Random.Range(1, 4);
        while (type == last_type) type = Random.Range(1, 4);
        if (type == 1)
        {
            AK_ATK();
        }
        else if (type == 2)
        {
            StartCoroutine("missile");
        }
        else if (type == 3)
        {
            StartCoroutine("GrenadeATK");
        }
        last_type = type;
    }

    // Update is called once per frame
    void Update()
    {
        if (AK47_Left.activeSelf)
        {
            AK47_Left.transform.position = new Vector3(AK47_Left.transform.position.x, Player.transform.position.y, 0);
        }

        if (AK47_Right.activeSelf)
        {
            AK47_Right.transform.position = new Vector3(AK47_Right.transform.position.x, Player.transform.position.y, 0);
        }
    }
    IEnumerator missile()
    {
        for (int i = 0; i < 2; i++)
        {
            Missile ms;
            missile_left.SetActive(true);
            missile_right.SetActive(true);
            AudioMGR.instance.Play_RPG_Shot();
            ms = Instantiate(Missile, missile_right_FirePosition.position, Quaternion.identity).GetComponent<Missile>();
            ms.speed = missile_Speed;
            ms.setObj(Bullet);
            yield return new WaitForSeconds(1f);
            AudioMGR.instance.Play_RPG_Shot();
            ms = Instantiate(Missile, missile_left_FirePosition.position, Quaternion.identity).GetComponent<Missile>();
            ms.speed = missile_Speed;
            ms.setObj(Bullet);
            Invoke("setfalse", 2);
            //Invoke("Think", att_time);
            yield return new WaitForSeconds(1f);
        }
        Invoke("Think", att_time);
        yield break;
    }
    void setfalse()
    {
        missile_left.SetActive(false);
        missile_right.SetActive(false);
    }
    IEnumerator GrenadeATK()
    {
        AudioMGR.instance.Play_Grenade_Throw();
        Grenade tmp = Instantiate(Grenade, FirePosition.position, Quaternion.identity).GetComponent<Grenade>();
        tmp.setOBJ(Bullet);
        tmp.dir = 1;
        yield return new WaitForSeconds(1f);
        AudioMGR.instance.Play_Grenade_Throw();
        Grenade tmp2 = Instantiate(Grenade, FirePosition.position, Quaternion.identity).GetComponent<Grenade>();
        tmp2.setOBJ(Bullet);
        tmp2.dir = 2;
        yield return new WaitForSeconds(1f);
        Invoke("Think", att_time);
        yield break;
    }
    int min(int a,int b)
    {
        if (a > b) return b;
        return a;
    }
    int max(int a, int b)
    {
        if (a > b) return a;
        return b;
    }
    void AK_ATK()
    {
        //StartCoroutine("AK_Left");
        //StartCoroutine("AK_Right");
        AK47_Left.SetActive(true);
        AK47_Right.SetActive(true);

        Invoke("and_of_ak", ak_time);
        
        //Invoke("Think", att_time);
    }

    void and_of_ak()
    {
        AK47_Left.SetActive(false);
        AK47_Right.SetActive(false);
        Invoke("Think", att_time);
    }

    void ShootAk()
    {
        StartCoroutine("IE_ShootAk");
    }

    IEnumerator IE_ShootAk()
    {
        if (AK47_Left.activeSelf)
        {
            AudioMGR.instance.Play_Slav_Attack1();
        }
        for (int i = 0; i < 3; i++)
        {
            if (AK47_Left.activeSelf)
            {
                EnemyBullet tmp = Instantiate(Bullet, Left_FirePosition.position, Quaternion.identity).GetComponent<EnemyBullet>();
                tmp.speed = Bullet_Speed;
                tmp.direction = Random.Range(-5, 5);
            }

            if (AK47_Right.activeSelf)
            {
                EnemyBullet tmp = Instantiate(Bullet, Right_FirePosition.position, Quaternion.identity).GetComponent<EnemyBullet>();
                tmp.speed = Bullet_Speed;
                tmp.direction = 180 + Random.Range(-5, 5);
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    void HideIntro()
    {
        playerSC.intro_time = false;
        weaponSC.intro_time = false;
        player_rigid.constraints = RigidbodyConstraints2D.None;
        player_rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        intro_ui.SetActive(false);
        boss_spr.SetActive(true);
        intro_boss.GetComponent<SnakeIntro>().StopScream();
        intro_boss.SetActive(false);
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
}
