using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private float fireRate = 0.5f;
    private float currentFireRate;
    [SerializeField]
    public float nextFire = 0.5f;
    private int extraBullets = 5;

    private SoundManager _soundmanager;
   
    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        currentFireRate = fireRate;
    }

    void Update(){
        if(currentFireRate == 0){
            if(Input.GetButtonDown("Fire1")){
                //Debug.Log("tiro");
                Shoot();
                extraBullets--;
                if(extraBullets <= 0){
                    currentFireRate = fireRate;
                    extraBullets = 5;
                }
            }
        }else{
            if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
                nextFire = Time.time + fireRate;
                //Debug.Log("tiro");
                Shoot();
            }
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        AudioSource.PlayClipAtPoint(_soundmanager.ShootBullet(), transform.position, 1f);
    }

    public void SetFireRate(int fr){
        currentFireRate = fr;
    }
}
