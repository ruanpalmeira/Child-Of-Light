using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    public char _coinID;
    [SerializeField]
    private PostProcessing PP;
    [SerializeField]
    private UIManager _uimanager;
    private SoundManager _soundmanager;
    
    private void Start() {
            
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.LogError("Ae1"+ other.tag);
        if(other.tag == "Player"){
            //Debug.LogError("Ae2"+ other.tag);
            //AddColor(_coinID);
            
            switch (_coinID){
                case '1':
                    RedCoin();
                    _uimanager.ShowRedCoinText();
                    _uimanager.ShowInventoryText();
                    break;
                case '2':
                    OrangeCoin();
                    break;
                case '3':
                    YellowCoin();
                    break;
                case '4':
                    GreenCoin();
                    break;
                case '5':
                    BlueCoin();
                    break;
                case '6':
                    IndigoCoin();
                    break;
                case '7':
                    VioletCoin();
                    break;
                case '8':
                    NormalWorld();
                    break;
                default:
                    DarkWorld();
                    break;
            }
            AudioSource.PlayClipAtPoint(_soundmanager.RedCoin(), transform.position, 1f);
            
            Destroy(this.gameObject);
        }else{
            transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public void DarkWorld(){
        PP.setup(false, 10 ,10 ,10 ,10 ,10 ,10 ,10 ,10 ,10);
    }

    public void NormalWorld(){
        PP.setup(false, 100, 0, 0, 0, 100, 0, 0, 0, 100);
    }

    public void RedCoin(){
        PP.setup(true, 53, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    public void OrangeCoin(){
        PP.setup(true, 53, 0, 0, 0, 35, 0, 0, 0, 0);
    }

    public void YellowCoin(){
        PP.setup(true, 53, 0, 0, 0, 75, 0, 0, 0, 0);
    }

    public void GreenCoin(){
        PP.setup(true, 0, 0, 0, 0, 75, 0, 0, 0, 0);
    }

    public void BlueCoin(){
        PP.setup(true, 0, 0, 0, 0, 0, 0, 0, 0, 84);
    }

    public void IndigoCoin(){
        PP.setup(true, 11, 0, 0, 0, 15, 0, 0, 0, 32);
    }

    public void VioletCoin(){
        PP.setup(true, 30, 0, 0, 0, 0, 0, 0, 0, 84);
    }
    /*
    public void VioletCoin(){
        PP.setup(true, 30, 0, 0, 0, 0, 0, 0, 0, 84);
    }*/
}
