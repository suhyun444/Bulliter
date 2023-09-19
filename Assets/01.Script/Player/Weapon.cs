using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool dead = false;
    bool LR = true;
    Vector2 mouseposition;
    public GameObject weapon;
    public Transform FirePosition;
    public GameObject bullet;
    public float bullet_cooldown;
    public int speed_level = 0;
    public float knife_cooldown;
    public GameObject knife;
    public Animator anim;
    string current_state;
    CapsuleCollider2D knife_bc2d;
    float knife_time = 0;
    float time = 0;
    public bool onShopping = false;
    public int damage; // 공격력
    public int dmg_level = 0;
    public float angle;
    public GameObject[] Bullets = new GameObject[6];
    public GameObject ReloadUI;
    public GameObject ReloadBar;
    RectTransform reloadRT;
    bool on_reload = false;
    int max_ammo = 6;
    int cur_ammo;
    public bool bullet_time = false; //튕길시 불렛타임 온오프
    public bool intro_time = false;
    public GameObject weaponSpr;
    public bool on_Pause;
    // Start is called before the first frame update
    void Start()
    {
        knife_bc2d = knife.GetComponent<CapsuleCollider2D>();
        unenable();
        cur_ammo = max_ammo;
        reloadRT = ReloadBar.GetComponent<RectTransform>();
        ReloadUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouseposition.y - this.transform.position.y, mouseposition.x - this.transform.position.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (LR && !dead)
        {
            if (mouseposition.x < this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                weapon.transform.localScale = new Vector3(-1, -1, 1);
                LR = false;
            }
        }
        else if (!LR && ! dead)
        {
            if (mouseposition.x > this.transform.position.x)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                weapon.transform.localScale = new Vector3(1, 1, 1);
                LR = true;
            }
        }

        time -= Time.deltaTime;
        if (Input.GetMouseButton(0) && !dead &&  weaponSpr.activeSelf && !on_Pause)
        {
            if (time <= 0 && !onShopping && cur_ammo > 0 && !on_reload && !intro_time)
            {
                AudioMGR.instance.Play_Player_Attack();
                time = bullet_cooldown;
                cur_ammo--;
                Bullets[cur_ammo].SetActive(false);
                bullet B = Instantiate(bullet, FirePosition.position, FirePosition.rotation).GetComponent<bullet>();
                B.atk = damage;
                GetComponentInChildren<ScreenShake>().AddScreenShake(0.15f, 0.04f);
            }
            else if(time <= 0 && !onShopping && cur_ammo == 0 && !on_reload)
            {
                StartCoroutine("Reload");
            }
        }
        knife_time -= Time.deltaTime;
        if (Input.GetMouseButton(1) && knife_time <= 0 && !onShopping && !intro_time && !dead && !on_Pause)
        {
            AudioMGR.instance.Play_Player_Swing();
            knife_time = knife_cooldown;
            knife_bc2d.enabled = true;
           GetComponentInChildren<ScreenShake>().AddScreenShake(0.1f, 0.03f);
            AnimationState("SwordSwing");
            Invoke("unenable", 0.2f);
        }
        if (Input.GetKey(KeyCode.R) && !on_reload)
        {
            StartCoroutine("Reload");
        }
    }
    void unenable()
    {
        knife_bc2d.enabled = false;
        AnimationState("SwordNone");
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
    IEnumerator Reload()
    {
        AudioMGR.instance.Play_Reload_Sound();
        on_reload = true;
        ReloadUI.SetActive(true);
        reloadRT.anchoredPosition = new Vector2(-38.5f, reloadRT.anchoredPosition.y);
        float dx = 84;//-38.5f;
        for(int i = 0; i < 12; i++)
        {
            dx -= 7;
            reloadRT.anchoredPosition = new Vector2(-38.5f + dx, reloadRT.anchoredPosition.y);
            yield return new WaitForSeconds(0.04f);
        }
        cur_ammo = max_ammo;
        for(int i = 0; i < 6; i++)
        {
            Bullets[i].SetActive(true);
        }
        ReloadUI.SetActive(false);
        on_reload = false;
        yield break;
    }
}
