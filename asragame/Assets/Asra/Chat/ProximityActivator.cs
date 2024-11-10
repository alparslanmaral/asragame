using UnityEngine;

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


public class ProximityActivator : MonoBehaviour
{
    public GameObject targetCanvas; // Aktif/pasif yapaca��m�z Canvas

    private void Start()
    {
        // Ba�lang��ta Canvas'� devre d��� yap
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player tag'li obje Trigger alan�na girerse Canvas'� etkinle�tir
        if (other.CompareTag("Player") && targetCanvas != null)
        {
            targetCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player tag'li obje Trigger alan�ndan ��karsa Canvas'� devre d��� b�rak
        if (other.CompareTag("Player") && targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
    }
}