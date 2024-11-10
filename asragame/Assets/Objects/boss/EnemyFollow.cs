using UnityEngine;
using UnityEngine.SceneManagement; 


//
//         _______                    _____                   _______         
//        /::\    \                  /\    \                 /:::\    \        
//       /::::\    \                /::\    \               /:::::\    \       
//      /::::::\    \              /::::\    \             /:::::::\    \      
//     /::::::::\    \            /::::::\    \           /:::::::::\    \     
//    /:::/ ~~\::\    \          /:::/\:::\    \         /:::/~~ \:::\    \    
//   /:::/     \::\    \        /:::/__\:::\    \       /:::/     \:::\    \   
//  /:::/    /  \::\    \      /::::\   \:::\    \     /:::/    /  \:::\    \  
// /:::/____/    \::\____\    /::::::\   \:::\    \   /:::/____/    \:::\____\ 
// |:::|    |     |:::|   |  /:::/\:::\   \:::\    \  |:::|    |    |:::|    |
// |:::|____|     |:::|   | /:::/  \:::\   \:::\____\ |:::|____|    |:::|    |
// \:::\    \    /:::/    / \::/    \:::\  /:::/    / \:::\    \   /:::/    /
//  \:::\    \  /:::/    /   \/____/ \:::\/:::/    /   \:::\    \ /:::/    /
//   \:::\     /:::/    /             \::::::/    /     \:::\    /:::/    /
//    \:::\__ /:::/    /               \::::/    /       \:::\__ /::/    /
//     \:::::::::/    /                /:::/    /         \::::::::/    /
//      \:::::::/    /                /:::/    /           \::::::/    /
//       \:::::/    /                /:::/    /             \::::/    /
//        \:::/    /                /:::/    /               \::/    /
//         ~~  ___/                 \::/    /                 ~~ ___/
//                                   \/____/

public class EnemyFollow : MonoBehaviour
{
    public Transform player; 
    public float detectionRange = 3f; 
    public float attackRange = 1f; 
    public float speed = 2f; 
    public string sceneToLoad = "NextScene";

    private bool isAttack = false; 
    private Animator animator; 
    private float timeInAttackRange = 0f;

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
            FollowPlayer(); 

            
            if (distanceToPlayer <= attackRange)
            {
                isAttack = true;
                timeInAttackRange += Time.deltaTime;

                
                if (timeInAttackRange > 1f)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                isAttack = false;
                timeInAttackRange = 0f; 
            }
        }
        else
        {
            isAttack = false; 
            StopFollowing(); 
            timeInAttackRange = 0f; 
        }

        
        animator.SetFloat("Speed", isAttack || distanceToPlayer <= detectionRange ? speed : 0f);
        animator.SetBool("isAttack", isAttack);
    }

    void FollowPlayer()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void StopFollowing()
    {
        
        animator.SetFloat("Speed", 0f);
    }
}
