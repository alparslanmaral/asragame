using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

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

public class SpikeTrap : MonoBehaviour
{
    public GameObject spikeUp;        
    public float raiseHeight = 2f;    
    public float raiseSpeed = 2f;     
    public string sceneToLoad;        

    private Vector3 initialPosition;  
    private bool playerTriggered = false;  

    void Start()
    {
        if (spikeUp != null)
        {
            initialPosition = spikeUp.transform.position;  
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTriggered = true;  
            StartCoroutine(WaitAndChangeScene()); 
        }
    }

    
    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(2f);  
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); 
        }
        else
        {

        }
    }

    void Update()
    {
        if (playerTriggered && spikeUp != null)
        {
            
            Vector3 targetPosition = initialPosition + new Vector3(0, raiseHeight, 0);
            spikeUp.transform.position = Vector3.MoveTowards(spikeUp.transform.position, targetPosition, raiseSpeed * Time.deltaTime);

            
            if (spikeUp.transform.position == targetPosition)
            {
                playerTriggered = false;  
            }
        }
    }
}
