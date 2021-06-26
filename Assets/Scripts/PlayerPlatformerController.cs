using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PhysicsObject))]
public class PlayerPlatformerController : PhysicsObject
{   
    
    public float _maxSpeed = 7;
    public float _jumpTakeOffSpeed = 7;
    public Transform FirePoint;
    public GameObject postProcessVolume;

    //private int _currentHealth = 100;
    private int extraLife = 40;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private int _maxPlayerLife = 100;
    [SerializeField] private AudioClip _playerJump;
    [SerializeField] private AudioClip _playerDamage;
    
    [SerializeField] private UIManager _uimanager;
    [SerializeField] private GameManager _gamemanager;
    private SoundManager _soundmanager;
    Weapon weapon;
    
    void Awake()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }

    void Start() {
        UIManager.perm.SetMaxHealth();
        
    }

    protected override void ComputeVelocity(){
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if((Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space)) && _grounded){
            AudioSource.PlayClipAtPoint(_playerJump, transform.position,1f);
            //jumpTimer = 0.5f;
            _velocity.y = _jumpTakeOffSpeed;
            _groundNormal.y = 1;
            _groundNormal.x = 0;
        }else if ((Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.Space))){
            //if(jumpTimer > 0){
                //_velocity.y = 7;
            //}else
            if (_velocity.y > 0){
                _velocity.y = _velocity.y * 0.5f;
            }
        }
        
        bool flipSprite = spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f);
            if(flipSprite){
                spriteRenderer.flipX = !spriteRenderer.flipX;
                FirePoint.localPosition = new Vector3(-FirePoint.localPosition.x, 0f, 0f);
                float angle = (FirePoint.localRotation.y == 0)? (180) : 0;
                FirePoint.localRotation = Quaternion.Euler(0, angle, 0);
            }
        //jumpTimer -= Time.deltaTime;

        _targetVelocity = move * _maxSpeed;
    }
    public void TakeDamage(float dmg){
        //AudioSource.PlayClipAtPoint(_playerDamage, transform.position, 1f);
        //UIManager.perm._currentHealth -= dmg;
        //_soundmanager.PlayerDamageSound(transform.position);
        UIManager.perm.SetHealth(dmg);
        
    }

    public void Respawn(){
        _gamemanager.StartCoroutine(_gamemanager.respawnPlayer(this.gameObject));
        _gamemanager.createplayer();
    }

    public float GetPlayerLife(){
        return UIManager.perm._currentHealth;
    }

    public void GotExtraShots(){
        GetComponent<Weapon>().SetFireRate(0);
    }

    public void GotExtraLife(){
        UIManager.perm.SetHealth(20);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Move_point"){
            _uimanager.ShowMoveText();
            other.gameObject.SetActive(false);
        }
        if(other.name == "Shoot_point"){
            _uimanager.ShowShootText();
            other.gameObject.SetActive(false);
        }
        if(other.name == "Jump_point"){
            _uimanager.ShowJumpText();
            other.gameObject.SetActive(false);
        }
        if(other.name == "Intro_point"){
            _uimanager.ShowIntroText();
            other.gameObject.SetActive(false);
        }
        if(other.name == "ExtraShots_point" || other.name == "ExtraShots1_point"){
            _uimanager.ShowExtraShotText();
        }
        if(other.name == "ExtraShots_point" || other.name == "ExtraShots1_point"){
            _uimanager.ShowExtraShotText();
        }  
    }
}
