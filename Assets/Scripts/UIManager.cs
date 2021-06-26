using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    public Text moveText;
    public Text shootText;
    public Text jumpText;
    public Text introText;
    public Text redCoinText;
    public Text extraShotsText;
    public Text inventoryText;
    public GameObject inventory;
    private bool inventoryIsActive = false;

    //player stats
    [SerializeField]
    public float _currentHealth;
    [SerializeField]
    private float _maxPlayerLife = 100f;
    public int[] coins = new int[7];

    //public bool changeHealth = false;

    public static UIManager perm;
    //public float dmg = -0.01f;

    // Start is called before the first frame update
    void Awake() {
     
        DontDestroyOnLoad(gameObject);
        //Singleton
        if(!perm){
            perm = this;
        }else{
            Destroy(gameObject);
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
         
    }
    public void SetMHealth(){
        _currentHealth = _maxPlayerLife;
        //slider.value = _currentHealth;
    }
    public void SetHealth(float dmgd){
        _currentHealth += dmgd;
        if(_currentHealth > _maxPlayerLife){
            _currentHealth = _maxPlayerLife;
        }
        slider.value = _currentHealth;
        if(_currentHealth <1){
            FindPlayer();
            SetMHealth();
        }         
    }

    public void SetMaxHealth(){
        slider.maxValue = _maxPlayerLife;
        slider.value = _maxPlayerLife;
        SetMHealth();
    }

    public void ShowMoveText(){
        moveText.GetComponent<Animator>().SetBool("play", true);
    }

    public void ShowShootText(){
        shootText.GetComponent<Animator>().SetBool("play", true);
    }
    public void ShowJumpText(){
        jumpText.GetComponent<Animator>().SetBool("play", true);
    }
    public void ShowIntroText(){
        introText.GetComponent<Animator>().SetBool("play", true);
        //introText.GetComponent<Animator>().
    }
    public void ShowRedCoinText(){
        redCoinText.GetComponent<Animator>().SetBool("play", true);
    }
    public void ShowExtraShotText(){
        extraShotsText.GetComponent<Animator>().SetBool("play", true);
    }
    public void ShowInventoryText(){
        inventoryText.GetComponent<Animator>().SetBool("play", true);
    }

    public void ShowInventory(){
        if(inventoryIsActive){
            inventory.SetActive(false);
            inventoryIsActive = false;
        }else{
            inventory.SetActive(true);
            inventoryIsActive = true;
        }
        
    }
    void FindPlayer () {
		GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
		if (searchResult != null){
			searchResult.GetComponent<PlayerPlatformerController>().Respawn();
        }else{
            Debug.LogError("Não achei o player");
        }
	}
}
