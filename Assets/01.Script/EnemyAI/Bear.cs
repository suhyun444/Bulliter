using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    bool InBattle = false;
    GameObject Player;
    Rigidbody2D rigid;
    int nextmove;
    float distance;
    bool Left = true;
    bool on_atk = false;
    float time = 0;
    public float Dash_cool = 5;
    public GameObject Bullet;
    public Transform shoot_pos;
    public float bullet_speed;
    public GameObject ak;
    float gun_time = 0;
    public float gun_cool = 3;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        time = Random.Range(0, Dash_cool);
        gun_time = Random.Range(0, gun_cool);
        Think();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        gun_time -= Time.deltaTime;
        if (InBattle && time <= 0)
        {
            StartCoroutine("Dash");
            time = Dash_cool;
        }
        if(InBattle && gun_time <= 0)
        {
            StartCoroutine("shotgun");
            gun_time = gun_cool;
        }
        
        float angle = Mathf.Atan2(Player.transform.position.y - ak.transform.position.y, Player.transform.position.x - ak.transform.position.x) * Mathf.Rad2Deg;
        ak.transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);

        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 12)
        {
            InBattle = true;
        }
        if(distance > 12)
        {
            InBattle = false;
        }
        if (InBattle)
        {
            if (this.transform.position.x > Player.transform.position.x && !Left && !on_atk)
            {
                ak.transform.localScale = new Vector3(-5, -5, 1);
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                Left = true;
            }
            else if (this.transform.position.x <= Player.transform.position.x && Left && !on_atk)
            {
                ak.transform.localScale = new Vector3(5, 5, 1);
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
    void Think()
    {
        nextmove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }
    IEnumerator shotgun()
    {
        float angle = Mathf.Atan2(Player.transform.position.y - shoot_pos.position.y, Player.transform.position.x - shoot_pos.position.x) * Mathf.Rad2Deg;
        AudioMGR.instance.Play_Bear_Gun();
        for (int i = 0; i < 7; i++)
        {
            EnemyBullet tmp = Instantiate(Bullet, shoot_pos.position, Quaternion.identity).GetComponent <EnemyBullet>();
            tmp.direction = angle + Random.Range(-10, 10);
            tmp.speed = bullet_speed;
            yield return new WaitForSeconds(Random.Range(0.02f, 0.04f));
        }
    }
    IEnumerator Dash()
    {
        Debug.Log("대쉬");
        gameObject.tag = "Enemy";
        on_atk = true;
        AudioMGR.instance.Play_Bear_Attack();
        int cnt = 40;
        while (cnt >= 0)
        {
            if (Left)
            {
                transform.Translate(-0.2f, 0, 0);
            }
            else if (!Left)
            {
                transform.Translate(0.2f, 0, 0);
            }
            cnt--;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.tag = "Untagged";
        on_atk = false;
        yield break;
    }
}
