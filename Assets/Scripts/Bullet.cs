using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 30;
    private int bossLife;
    [SerializeField] float bulletLifeTime = 1.0f;

    //public GameObject impactEffect;
    
    void Start(){
        rb.velocity = transform.right * speed;
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if(hitInfo.tag == "Enemy"){
            Enemy enem = hitInfo.GetComponent<Enemy>();
            if(enem != null){
                enem.TakeDamage(damage);
            }
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if (hitInfo.tag == "Boss"){
            if(!hitInfo.GetComponent<Animator>().GetBool("canchange")){
                //hitInfo.GetComponent<Boss>().isInvulnerable = true;
                bossLife = hitInfo.GetComponent<Animator>().GetInteger("changeLife");
                bossLife--;
                hitInfo.GetComponent<Boss>().bossHealth = bossLife;
                hitInfo.GetComponent<Animator>().SetInteger("changeLife", bossLife);
                hitInfo.GetComponent<Animator>().SetBool("canChange", true);
                hitInfo.GetComponent<Boss>().BossHurt();
                Destroy(gameObject);
            }else{
                Destroy(gameObject);
            }
        }
    }
}