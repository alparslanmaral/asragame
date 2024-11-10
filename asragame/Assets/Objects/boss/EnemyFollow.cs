using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþi için gerekli


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
    public Transform player; // Oyuncunun Transform'u
    public float detectionRange = 3f; // Düþmanýn oyuncuyu algýlayacaðý menzil
    public float attackRange = 1f; // Saldýrý menzili
    public float speed = 2f; // Düþmanýn takip hýzý
    public string sceneToLoad = "NextScene"; // Geçilecek sahnenin adý (public olarak belirlenmiþ)

    private bool isAttack = false; // Oyuncuya saldýrýp saldýrmadýðýný kontrol eder
    private Animator animator; // Animatör bileþeni
    private float timeInAttackRange = 0f; // Oyuncunun saldýrý menzilinde geçirdiði süre

    void Start()
    {
        // Enemy'nin Animator bileþenini al
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Oyuncuyla düþman arasýndaki mesafeyi hesapla
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Oyuncu detectionRange içinde mi?
        if (distanceToPlayer <= detectionRange)
        {
            FollowPlayer(); // Oyuncuyu takip et

            // Eðer oyuncu attackRange içindeyse isAttack true, deðilse false
            if (distanceToPlayer <= attackRange)
            {
                isAttack = true;
                timeInAttackRange += Time.deltaTime; // Oyuncu saldýrý menzilindeyken süreyi artýr

                // Oyuncu 1 saniyeden fazla attackRange içinde durduysa sahneyi deðiþtir
                if (timeInAttackRange > 1f)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                isAttack = false;
                timeInAttackRange = 0f; // Saldýrý menzilinden çýkarsa süreyi sýfýrla
            }
        }
        else
        {
            isAttack = false; // Saldýrý modundan çýk
            StopFollowing(); // Takibi býrak
            timeInAttackRange = 0f; // Menzilden çýktýðýnda süreyi sýfýrla
        }

        // Animatör parametrelerini güncelle
        animator.SetFloat("Speed", isAttack || distanceToPlayer <= detectionRange ? speed : 0f); // Hýz parametresini kontrol et
        animator.SetBool("isAttack", isAttack); // isAttack parametresini ayarla
    }

    void FollowPlayer()
    {
        // Oyuncuya doðru hareket et
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Oyuncuya doðru dön
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void StopFollowing()
    {
        // Hýzý sýfýrlayýnca düþman olduðu yerde kalýr
        animator.SetFloat("Speed", 0f);
    }
}
