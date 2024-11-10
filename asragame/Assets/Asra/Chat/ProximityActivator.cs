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
    public GameObject targetCanvas; // Aktif/pasif yapacaðýmýz Canvas

    private void Start()
    {
        // Baþlangýçta Canvas'ý devre dýþý yap
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player tag'li obje Trigger alanýna girerse Canvas'ý etkinleþtir
        if (other.CompareTag("Player") && targetCanvas != null)
        {
            targetCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player tag'li obje Trigger alanýndan çýkarsa Canvas'ý devre dýþý býrak
        if (other.CompareTag("Player") && targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
    }
}