using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour{

    private SoundManager _soundmanager;

    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerPlatformerController player = other.GetComponent<PlayerPlatformerController>();
            player.GotExtraLife();
            AudioSource.PlayClipAtPoint(_soundmanager.PowerUpHeart(), transform.position, 1f);
            Destroy(gameObject);
        }else if(other.gameObject.layer == 10) {
            transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            Destroy(gameObject, 10f);
        }
    }
}
