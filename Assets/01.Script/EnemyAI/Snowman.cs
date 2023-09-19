using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public Animator anim;
    string current_state;
    bool on_atk = false;
    GameObject Player;
    Rigidbody2D rigid;
    float time = 0;
    float ATK_COOl = 4;
    float distance = 0;
    bool InBattle;
    public GameObject bullet;
    public GameObject spreadBullet;
    public Transform FirePosition;
    bool Left;
    int nextmove;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        time = Random.Range(0, ATK_COOl);
        Think();
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (InBattle && time <= 0)
        {
            on_atk = true;
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            AnimationState("YetiAtt");
            ProBullet tmp = Instantiate(bullet, FirePosition.position, Quaternion.identity).GetComponent<ProBullet>();
            tmp.setOBJ(spreadBullet);
            time = ATK_COOl;
            Invoke("set_atk", 1);
        }
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 10)
        {
            InBattle = true;
        }
        if (InBattle)
        {
            if (this.transform.position.x > Player.transform.position.x && !Left)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = true;
            }
            else if (this.transform.position.x <= Player.transform.position.x && Left)
            {
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = false;
            }
        }
    }
    void set_atk()
    {
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        AnimationState("YetiIdle");
        on_atk = false;
    }
    private void FixedUpdate()
    {
        if (!on_atk)
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
