using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public AudioClip _playerHurt, _shootBullet, _enemyHurt, _bossHurt, _bossDead, _redCoin, _playerJump, _powerUpHeart, _powerUpExtraShoots;

    // Start is called before the first frame update
    void Start(){
        if(_playerHurt == null){
            Debug.LogError("Player hurt audio not found");
        }
        if(_playerJump == null){
            Debug.LogError("Player jump audio not found");
        }
        if(_enemyHurt == null){
            Debug.LogError("Enemy hurt audio not found");
        }
        if(_bossHurt == null){
            Debug.LogError("Boss hurt audio not found");
        }
        if(_bossDead == null){
            Debug.LogError("Boss dead audio not found");
        }
        if(_shootBullet == null){
            Debug.LogError("Shoot bullet audio not found");
        }
        if(_powerUpHeart == null){
            Debug.LogError("PowerUp heart audio not found");
        }
        if(_powerUpExtraShoots == null){
            Debug.LogError("PowerUp extra shoots audio not found");
        }
        if(_redCoin == null){
            Debug.LogError("Redcoin audio not found");
        }
    }

    public AudioClip PlayerHurt(){
        return _playerHurt;
    }
    public AudioClip PlayerJump(){
        return _playerJump;
    }
    public AudioClip EnemyHurt(){
        return _enemyHurt;
    }
    public AudioClip BossHurt(){
        return _bossHurt;
    }
    public AudioClip BossDead(){
        return _bossHurt;
    }
    public AudioClip ShootBullet(){
        return _shootBullet;
    }
    public AudioClip PowerUpHeart(){
        return _powerUpHeart;
    }
    public AudioClip PowerUpExtraShoots(){
        return _powerUpExtraShoots;
    }
    public AudioClip RedCoin(){
        return _redCoin;
    }
}