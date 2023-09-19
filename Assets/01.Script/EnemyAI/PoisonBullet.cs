using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : MonoBehaviour
{
    public float BulletSpeed;
    public EnemyBullet[] bullets = new EnemyBullet[12];
    GameObject Player;
    GameObject bullet;
    public bool chase_player;
    public float direction;
    //float time = 0;
    public float speed;
    public bool penetrate = false;
    int deadtime = 8;
    // Start is called before the first frame update
    void Start()
    {
        if (chase_player)
        {
            Player = GameObject.Find("Player");
            float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        }

        Destroy(gameObject, deadtime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Solid"))
        {
            Boom();
        }
    }
    public void setGameObject(GameObject tmp)
    {
        bullet = tmp;
    }
    public void Boom()
    {
        int angle_offset = Random.Range(0, 30);
        for (int i = 0; i < 360; i += 30)
        {
            bullets[i / 30] = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullets[i / 30].Penetrate = penetrate;
            bullets[i/30].speed = BulletSpeed;
            bullets[i/30].direction = i + angle_offset;
        }
        Destroy(gameObject);
    }
    public void ReCall()
    {
        for(int i = 0; i < 12; i++)
        {
            if(bullets[i] != null)
            {
                bullets[i].direction += 180;
                bullets[i].chase();
            }
        }
    }
}
