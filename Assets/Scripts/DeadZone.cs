using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerPlatformerController player = other.transform.GetComponent<PlayerPlatformerController>();
            if (player != null){
                player.TakeDamage(-player.GetPlayerLife());
            }
        }
    }
}