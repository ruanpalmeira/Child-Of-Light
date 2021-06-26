using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    float dmg = -10f;
    [SerializeField]
    //p
    
    public GameObject heartPrefab;
    private Transform heartSpawn;
    //public ;GameObject deathEffect;
    private SoundManager _soundmanager;

    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
    }

    public void TakeDamage(int damage){
        health -= damage;
        AudioSource.PlayClipAtPoint(_soundmanager.EnemyHurt(), transform.position, 1f);
        if(health <= 0){
            Die();
        }
    }

    // Update is called once per frame
    void Die(){
        heartSpawn = transform;
        heartSpawn.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(heartPrefab, heartSpawn.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
        //Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerPlatformerController player = other.transform.GetComponent<PlayerPlatformerController>();

            if (player != null){
                AudioSource.PlayClipAtPoint(_soundmanager.PlayerHurt(), transform.position, 1f);
                player.TakeDamage(dmg);
            }
            //_anim.SetTrigger("OnEnemyDeath");
            //EnemyDestroy();
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerPlatformerController player = other.transform.GetComponent<PlayerPlatformerController>();
            dmg = -1f;
            if (player != null){
                AudioSource.PlayClipAtPoint(_soundmanager.PlayerHurt(), transform.position, 1f);
                player.TakeDamage(dmg);
            }
            //_anim.SetTrigger("OnEnemyDeath");
            //EnemyDestroy();
        }
        dmg = -10f;
    }
}
