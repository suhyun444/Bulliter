using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    public Animator anim;
    string current_state;
    bool InBattle = false;
    GameObject Player;
    Rigidbody2D rigid;
    int nextmove;
    float distance;
    bool Left = true;
    float time = 0;
    public float ATK_cool = 5;
    public GameObject Bullet;
    public float Bullet_speed;
    public GameObject FirePosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        time = Random.Range(0, ATK_cool);
        Think();
        AnimationState("ScorpionWalk");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (InBattle && time <= 0)
        {
            StartCoroutine("Attack");
            time = ATK_cool;
        }
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 10)
        {
            InBattle = true;
        }
        if (distance > 10)
        {
            InBattle = false;
        }
        if (InBattle)
        {
            if (this.transform.position.x > Player.transform.position.x && !Left )
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = true;
            }
            else if (this.transform.position.x <= Player.transform.position.x && Left )
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
        if (rayHit.collider == null)
        {
            nextmove *= -1;
            CancelInvoke();
            Invoke("Think", 1);
        }
    }
    IEnumerator Attack()
    {
        AnimationState("ScorpionShoot");
        AudioMGR.instance.Play_Scolpion_Attack();
        for (int i = 0; i < 8; i++)
        {
            float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y,Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
            EnemyBullet tmp = Instantiate(Bullet, FirePosition.transform.position, Quaternion.identity).GetComponent<EnemyBullet>();tmp.speed = Bullet_speed;
            if (Left)
            {
                tmp.direction = angle + 13 + Random.Range(-15, 16);
            }
            else if (!Left)
            {
                tmp.direction = angle - 13 + Random.Range(-15, 16);
            }

            
            yield return new WaitForSeconds(0.1f);
        }
        AnimationState("ScorpionWalk");
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
