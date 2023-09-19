using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    public Animator anim;
    string current_state;
    bool InBattle = false;
    bool onAttack = false;
    GameObject Player;
    Rigidbody2D rigid;
    int nextmove;
    float distance;
    bool Left = true;
    float time = 2;
    public float ATK_cool = 5;
    public GameObject Bullet;
    public float Bullet_speed;
    public Transform FirePosition;
    EnemyHP Hp;
    int[,] d_xy = new int[2,5]{ { 0, 0, 1, 0,-1}, {0, -1, 0, 1, 0}};
    GameObject[] bullets = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Hp = GetComponent<EnemyHP>();
        rigid = GetComponent<Rigidbody2D>();
        time = Random.Range(0, ATK_cool);
        Think();
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp.CUR_HP <= 0)
        {
            for(int i = 0; i < 5; i++)
            {
                Destroy(bullets[i].gameObject);
            }
        }
        time -= Time.deltaTime;
        if (InBattle && time <= 0)
        {
            StartCoroutine("Attack");
            time = ATK_cool;
        }
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 15)
        {
            InBattle = true;
        }
        else if(distance > 15)
        {
            InBattle = false;
        }
        if (InBattle)
        {
            if (this.transform.position.x > Player.transform.position.x && !Left && !onAttack)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = true;
            }
            else if (this.transform.position.x <= Player.transform.position.x && Left && !onAttack)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = false;
            }
        }
        if(onAttack)
        {
            AnimationState("MummyAtt");
        }
        else
        {
            if(nextmove == 0)
                AnimationState("MummyIdle");
            else
                AnimationState("MummyWalk");
        }
    }
    private void FixedUpdate()
    {
        if (!onAttack)
        {
            rigid.velocity = new Vector2(nextmove, rigid.velocity.y);
        }

        Vector2 frontVec = new Vector2(rigid.position.x + nextmove * 1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 0, 1));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1);
        if (rayHit.collider == null)
        {
            nextmove *= -1;
            CancelInvoke();
            Invoke("Think", 1);
        }
    }
    IEnumerator Attack()
    {
        
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        rigid.freezeRotation = true;
        onAttack = true;
        EnemyBullet tmp;
        for (int i = 0; i < 5; i++)
        {
            int dx = d_xy[0, i];
            int dy = d_xy[1, i];
            bullets[i] = Instantiate(Bullet, new Vector3(FirePosition.position.x + dx, FirePosition.position.y + dy, 0), Quaternion.identity) as GameObject;
            bullets[i].transform.parent = this.gameObject.transform;
            bullets[i].tag = "Untagged";
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1);
        //float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
        AudioMGR.instance.Play_Mummy_Attack();
        for(int i = 0; i < 5; i++)
        {
            bullets[i].transform.parent = null;
            bullets[i].tag = "EnemyBullet";
            tmp = bullets[i].GetComponent<EnemyBullet>();
            tmp.speed = Bullet_speed;
            tmp.chase_player = true;
            tmp.Penetrate = true;
            tmp.chase();
        }
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        onAttack = false;
        
        yield break;
    }
    void Think()
    {
        nextmove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }

    void AnimationState(string state)
    {
        if (current_state == state) return;

        anim.Play(state);
        current_state = state;
    }
}
