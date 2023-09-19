using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject Player;
    //float time = 0;
    public float speed;
    public float bullet_speed;//퍼지는 총알속도
    float angle;
    int deadtime = 8;
    public GameObject bullet;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        InvokeRepeating("chase", 0, 0.3f);
        Destroy(gameObject, deadtime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
    public void chase()
    {
        angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + Random.Range(-5,5), Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine("Boom");
        }
    }
    IEnumerator Boom()
    {
        CancelInvoke();
        AudioMGR.instance.Play_RPG_Boom();
        speed = 0;
        for (int i = 0; i < 360; i += 20)
        {
            EnemyBullet bullets = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullets.speed = bullet_speed;
            bullets.direction = i;
        }
        for (int i = 0; i < 10; i++)
        {
            EnemyBullet tmp = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            tmp.speed = bullet_speed*2;
            tmp.direction = angle + Random.Range(-20, 20);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
    public void setObj(GameObject obj)
    {
        bullet = obj;
    }
}
