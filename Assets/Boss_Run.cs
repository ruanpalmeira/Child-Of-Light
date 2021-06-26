using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float speed = 2.0f;
    public float runRange = 25f;
    private GameObject _coinPrefab;
    public GameObject _enemyPrefab2;
    [SerializeField]
    private AudioClip bossDead;
    //private SoundManager _soundmanager;
    /*
    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }*/

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        //boss.isInvulnerable = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        
        if (animator.GetInteger("changeLife") <= 4){
            speed = 5f;
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.isInvulnerable = false;
        if(player != null){
            if(Vector2.Distance(player.position, rb.position) <= runRange){
                    boss.LookAtPlayer();
                    Vector2 target = new Vector2(player.position.x, rb.position.y);
                    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                if(newPos.x < 235){ 
                    if(newPos.x > 206){
                        rb.MovePosition(newPos);
                    }else{
                        newPos.x += 1;
                        rb.MovePosition(newPos);
                    }
                }else{
                    newPos.x -= 1;
                }
                rb.MovePosition(newPos);
            }
        }else{
            FindPlayer();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("canChange", false);
        //boss.isInvulnerable = true;
            if(animator.GetInteger("changeLife") == 12 || animator.GetInteger("changeLife") == 8 || animator.GetInteger("changeLife") == 4){
                Instantiate(_enemyPrefab2, rb.position, rb.transform.rotation);
            }else if(animator.GetInteger("changeLife") == 0){
                speed = 0;
                _coinPrefab = GameObject.Find("RedCoin");
                _coinPrefab.transform.position = rb.position;
                //Instantiate(_coinPrefab, rb.position, rb.transform.rotation);
                AudioSource.PlayClipAtPoint(bossDead, animator.transform.position, 100f);
                Destroy(animator.gameObject);
        }
    }

    void FindPlayer () {
		GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
		if (searchResult != null){
			player = searchResult.transform;
        }
	}
}