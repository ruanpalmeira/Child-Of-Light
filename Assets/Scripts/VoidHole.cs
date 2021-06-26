using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoidHole : MonoBehaviour
{   
    [SerializeField] float dmg = -10f;
    private SoundManager _soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.LogError("Ae1"+ other.tag);
        if(other.tag == "Player"){
            AudioSource.PlayClipAtPoint(_soundmanager.PlayerHurt(), transform.position, 1f);
            UIManager.perm.SetHealth(dmg);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            dmg = -1f;
            AudioSource.PlayClipAtPoint(_soundmanager.PlayerHurt(), transform.position, 1f);
            //PlayerPlatformerController player = other.GetComponent<PlayerPlatformerController>();
            //player.TakeDamage(dmg);
            UIManager.perm.SetHealth(dmg);
        }
        dmg = -10f;
    }
}
