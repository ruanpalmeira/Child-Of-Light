using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    int dmg = -20;
    public int bossHealth = 16;
    public bool isInvulnerable = false;
	public bool isFlipped = false;
    public bool canRun = true;
    [SerializeField]
    private AudioClip _bossHurt;
    private SoundManager _soundmanager;

    void Start() {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

	public void LookAtPlayer()
	{
        if(player != null){
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }else{
            FindPlayer();
        }
	}

    void FindPlayer () {
		GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
		if (searchResult != null){
			player = searchResult.transform;
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerPlatformerController player = other.transform.GetComponent<PlayerPlatformerController>();
            if (player != null){
                AudioSource.PlayClipAtPoint(_soundmanager.PlayerHurt(), transform.position, 1f);
                player.TakeDamage(dmg);
            }
        }
        if(other.gameObject.layer == 10) {
            canRun = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        canRun = true;
    }
    
    public void BossHurt(){
        AudioSource.PlayClipAtPoint(_soundmanager.BossHurt(), transform.position, 1f);
    }

    public void BossDead(){
        AudioSource.PlayClipAtPoint(_soundmanager.BossDead(), transform.position, 1f);
    }
}
