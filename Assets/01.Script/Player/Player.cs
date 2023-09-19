using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform cam_pos;
    public GameObject weapon;
    public bool dead = false;
    Rigidbody2D rigidbody2d;
    public bool intro_time = false;

    int MAX_HP = 6;
    public GameObject[] HPUI = new GameObject[10];
    public GameObject[] HP_LV = new GameObject[2];
    public int HP_level = 0;
    int CUR_HP;
    public float speed = 15f;

    int jumping = 0;
    public int j_power = 8;

    public Animator anim;
    [SerializeField]
    string current_state;

    private Material mat_white;
    private Material def_mat;
    public SpriteRenderer Player_spr;
    bool unBeatTime = false;
    public bool bonusTime = false;
    public GameObject death_sceen;
    public GameObject death_text;
    public GameObject HUD;

    //대쉬
    public float dash_speed;
    public float dash_time;
    float curr_dash_time;
    public Vector2 dash_dir;
    public int MAX_dash_count = 1;
    public int dash_count = 0;
    public float dash_cooltime;
    float curr_dash_cooltime;


    bool isDash;
    bool back_to_title = false;

    private Vector2 mousePos;

    Weapon weaponSC;
    public GameObject[] dashUI = new GameObject[2];
    public GameObject[] DashIcon = new GameObject[3];
    public int money = 0;
    public Text moneyTXT;
    public GameObject PlayerHitUI;
    public GameObject Death_TEXT;

    public bool is_tutorial = false;
    public Transform tp_point;
    public GameObject weaponSpr;
    bool on_UI = false;
    public bool on_Help_UI;
    public GameObject pauseUI;
    public bool groundCheck = false;

    void Start()
    {
        is_tutorial = (SceneManager.GetActiveScene().buildIndex == 2);
        weaponSC = GetComponent<Weapon>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        CUR_HP = MAX_HP;
        isDash = false;
        mat_white = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        def_mat = Player_spr.material;
        death_sceen.SetActive(false);
        HUD.SetActive(true);
        death_text.SetActive(false);
        dash_count = MAX_dash_count;
        if (is_tutorial)
        {
            weaponSpr.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !is_tutorial)
        {
            if (!on_UI)
            {
                weaponSC.on_Pause = true;
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
            if (on_UI)
            {
                weaponSC.on_Pause = false;
                pauseUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
        Camera.main.transform.position = new Vector3(cam_pos.position.x, cam_pos.position.y, -10f);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerHitUI.SetActive(unBeatTime);
        if (isDash)
        {
            rigidbody2d.velocity = dash_dir * dash_speed;
        }
        else
        {
            Vector3 myScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 playerLook = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myScreenPos.z));

            dash_dir = playerLook - new Vector2(transform.position.x, transform.position.y);
            dash_dir.Normalize();
        }
        curr_dash_cooltime += Time.deltaTime;
        if(curr_dash_cooltime >= dash_cooltime && dash_count < MAX_dash_count)
        {
            DashIcon[dash_count].SetActive(true);
            dash_count++;
            curr_dash_cooltime = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumping < 2 && !dead && !intro_time && !on_Help_UI)
        {
            AudioMGR.instance.Play_Player_Jump();
            rigidbody2d.velocity = Vector2.up * j_power;
            jumping++;
            CreateDust(Player_spr.transform.position);
        }

        curr_dash_time -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dead && !intro_time && dash_count > 0 && !on_Help_UI)
        {
            if(dash_count == MAX_dash_count)
            {
                curr_dash_cooltime = 0;
            }
            AudioMGR.instance.Play_Player_Dash();
            dash_count--;
            DashIcon[dash_count].SetActive(false);
            isDash = true;
            curr_dash_time = dash_time;
        }
        if (curr_dash_time <= 0)
        {
            if (isDash)
            {
                rigidbody2d.velocity = Vector2.zero;
                isDash = false;
            }
        }
        //}
        Dash();
        if(CUR_HP <= 0)
        {
            AnimationState("PlayerDeath");

            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            death_sceen.SetActive(true);
            HUD.SetActive(false);
            death_text.SetActive(true);
            Invoke("set_title",2.5f);
            if (Input.anyKeyDown && back_to_title)
            {
                SceneManager.LoadScene("01 title");
            }
        }
        else
        {
            float axis = Input.GetAxisRaw("Horizontal");
            if (groundCheck)
            {
                //if (current_state == "PlayerIdle")
                    if (axis != 0) AnimationState("PlayerWalk");
                //if (current_state == "PlayerWalk")
                    if (axis == 0) AnimationState("PlayerIdle");
            }
            else
            {
                if (rigidbody2d.velocity.y < -0.1) AnimationState("PlayerJ2");
                else AnimationState("PlayerJ1");
            }

        }
        moneyTXT.text = money.ToString();
    }
    public void resume_button()
    {
        weaponSC.on_Pause = false;
        pauseUI.SetActive(false);
        on_UI = false;
        Time.timeScale = 1;
    }
    public void back_to_title_button()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void set_title()
    {
        Death_TEXT.SetActive(true);
        back_to_title = true;
    }
    public void Heal()
    {
        CUR_HP = MAX_HP;
        for(int i = 0; i < MAX_HP; i++)
        {
            HPUI[i].SetActive(true);
        }
    }
    public void DashUpgrade()
    {
        MAX_dash_count = 3;
        dash_count = MAX_dash_count;
        dashUI[0].SetActive(true);
        dashUI[1].SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            DashIcon[i].SetActive(true);
        }
    }
    public void max_HPUP()
    {
        MAX_HP += 2;
        HP_LV[HP_level].SetActive(true);
        HP_level++;
        for(int i = 0; i < 2; i++)
        {
            HPUI[CUR_HP].SetActive(true);
            CUR_HP++;
        }
    }
    void Dash()
    {
        if (isDash == true) 
        {
            CreateDust(Player_spr.transform.position);
            CreateDust(transform.position);
            //CreateDust(transform.up/2);
            //TimeSpan += Time.deltaTime;
            //if (TimeSpan < CheckTime)
            //{
               // rigidbody2d.gravityScale = 0;
                //transform.position = Vector2.Lerp(transform.position, dashPointPos, Time.deltaTime * dashSpeed);
                //rigidbody2d.velocity = dashPointPos;// * dashSpeed;
            //}
            //else if (TimeSpan > CheckTime) 
           // {
               // TimeSpan = 0;
               // rigidbody2d.velocity = Vector2.zero; 
               // rigidbody2d.gravityScale = 5;
               // isDash = false;
            //}
        }
    }
    private void FixedUpdate()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        if (!intro_time && !on_Help_UI)
        {
            Vector2 moveAmout = new Vector2(axis * speed, rigidbody2d.velocity.y);
            if (!isDash) rigidbody2d.velocity = moveAmout;
        }
    }
    public void GroundCheck()
    {
        CreateDust(Player_spr.transform.position);
        jumping = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isDash && !unBeatTime && !bonusTime && !dead)
            {
                AudioMGR.instance.Play_Player_DMG();
                Player_spr.material = mat_white;
                unBeatTime = true;
                Invoke("MaterialToDef", 0.1f);
                Invoke("setBeatTime", 0.5f);
                CUR_HP--;
                HPUI[CUR_HP].SetActive(false);
                if (CUR_HP == 0)
                {

                    weapon.SetActive(false);
                    weaponSC.dead = true;
                    dead = true;
                    Debug.Log("game over");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            AudioMGR.instance.Play_Player_GetMoney();
            money += collision.gameObject.GetComponent<Coin>().cost;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Instantiate(Resources.Load("Dust", typeof(GameObject)) as GameObject, collision.transform.position, Quaternion.identity);//.transform.rotation = collision.transform.rotation;
            if (is_tutorial && !isDash && !bonusTime)
            {
                AudioMGR.instance.Play_Player_DMG();
                Player_spr.material = mat_white;
                unBeatTime = true;
                Invoke("MaterialToDef", 0.1f);
                Invoke("setBeatTime", 0.5f);
                if (tp_point != null)
                {
                    if (!isDash && !bonusTime)
                        transform.position = tp_point.position;
                }
            }
            else
            {
                if (!isDash && !unBeatTime && !bonusTime && !dead)
                {
                    AudioMGR.instance.Play_Player_DMG();
                    Player_spr.material = mat_white;
                    unBeatTime = true;
                    Invoke("MaterialToDef", 0.1f);
                    Invoke("setBeatTime", 0.5f);
                    CUR_HP--;
                    HPUI[CUR_HP].SetActive(false);
                    if (CUR_HP == 0)
                    {
                        weapon.SetActive(false);
                        weaponSC.dead = true;
                        dead = true;
                        Debug.Log("game over");
                    }
                }
            }
        }
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
    void MaterialToDef()
    {
        Player_spr.material = def_mat;
    }
    void setBeatTime()
    {
        unBeatTime = false;
    }

    void CreateDust(Vector3 pos)
    {
        Instantiate(Resources.Load("Dust", typeof(GameObject)) as GameObject, pos, Quaternion.identity);
    }
}
