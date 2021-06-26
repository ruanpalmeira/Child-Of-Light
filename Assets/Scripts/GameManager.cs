using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _player;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private UIManager _uimanager;
    // Start is called before the first frame update
    void Start()
    {/*
        if(_player == null){
            Debug.LogError("Player is NULL on GameManager");
        }*/
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_playerPrefab == null){
            Debug.LogError("Player prefab is NULL on GameManager");
        }
        if(_spawnPoint == null){
            Debug.LogError("SpawnPoint is NULL on GameManager");
        }
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
             Debug.LogError("Fim");
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            //_uimanager.ShowInventory();
        }

    }

    public IEnumerator respawnPlayer(GameObject _player){
        
        Destroy(_player);
        yield return new WaitForSeconds(1f);
        
    }

    public void createplayer(){
        Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
