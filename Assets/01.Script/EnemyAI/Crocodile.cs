using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    public Animator anim;
    string current_state;
    bool InBattle = false;
    GameObject Player;
    Rigidbody2D rigid;
    int nextmove;
    [SerializeField]
    float distance;
    [SerializeField]
    bool Left = true;
    bool on_atk = false;
    float time = 0;
    public float Dash_cool = 5;
    public GameObject Bullet;
    public Transform shoot_pos;
    public float bullet_speed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        time = Random.Range(0, Dash_cool);
        Think();
        AnimationState("CrocodileWalk");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(InBattle && time <= 0)
        {
            StartCoroutine("Dash");
            time = Dash_cool;
        }
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance <= 9)
        {
            InBattle = true;
        }
        if(distance > 9)
        {
            InBattle = false;
        }
        if (InBattle)
        {
            if (this.transform.position.x > Player.transform.position.x && !Left && !on_atk)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = true;
            }
            else if (this.transform.position.x <= Player.transform.position.x && Left && !on_atk)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = false;
            }
        }
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextmove * 1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 0, 1));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1);
        if(rayHit.collider == null)
        {
            nextmove *= -1;
            CancelInvoke();
            Invoke("Think", 1);
        }
    }
    void Think()
    {
        nextmove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }
    IEnumerator Dash()
    {
        AnimationState("CrocodileDash");
        gameObject.tag = "Enemy";
        on_atk = true;
        AudioMGR.instance.Play_Crocodile_Attack();
        int cnt = 20;
        while(cnt >= 0)
        {
            if (Left)
            {
                transform.Translate(-0.2f, 0, 0);
            }
            else if (!Left)
            {
                transform.Translate(0.2f, 0, 0);
            }
            if(cnt == 14)
            {
                EnemyBullet tmp1 = Instantiate(Bullet, shoot_pos.position, Quaternion.identity).GetComponent<EnemyBullet>(); tmp1.speed = bullet_speed;
                EnemyBullet tmp2 = Instantiate(Bullet, shoot_pos.position, Quaternion.identity).GetComponent<EnemyBullet>(); tmp2.speed = bullet_speed;
                EnemyBullet tmp3 = Instantiate(Bullet, shoot_pos.position, Quaternion.identity).GetComponent<EnemyBullet>(); tmp3.speed = bullet_speed;
                if (Left)
                {
                    tmp1.direction = 180;
                    tmp2.direction = 160;
                    tmp3.direction = 200;
                }
                else if (!Left)
                {
                    tmp1.direction = 0;
                    tmp2.direction = 20;
                    tmp3.direction = -20;
                }
            }
            cnt--;
            yield return new WaitForSeconds(0.01f);
        }
        on_atk = false;
        //AnimationState("CrocodileIdle");
        gameObject.tag = "Untagged";
        AnimationState("CrocodileWalk");
        yield break;
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
}
