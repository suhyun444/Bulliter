using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int atk;
    public float speed;
    public GameObject muzzle_flash;
    //float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setfalse", 3f);
        Destroy(Instantiate(muzzle_flash, transform.position, Quaternion.identity), 0.03f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
    void setfalse()
    {
        //this.gameObject.SetActive(false);
        Destroy(gameObject);
        Instantiate(Resources.Load("Dust", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity).transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Solid")
        ||  collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            setfalse();
    }
    public void setatk(int a)
    {
        atk = a;
    }
    public void setdir(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
