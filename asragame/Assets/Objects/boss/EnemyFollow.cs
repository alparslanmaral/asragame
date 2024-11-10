using UnityEngine;
using UnityEngine.SceneManagement; // Sahne ge�i�i i�in gerekli


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
    public float detectionRange = 3f; // D��man�n oyuncuyu alg�layaca�� menzil
    public float attackRange = 1f; // Sald�r� menzili
    public float speed = 2f; // D��man�n takip h�z�
    public string sceneToLoad = "NextScene"; // Ge�ilecek sahnenin ad� (public olarak belirlenmi�)

    private bool isAttack = false; // Oyuncuya sald�r�p sald�rmad���n� kontrol eder
    private Animator animator; // Animat�r bile�eni
    private float timeInAttackRange = 0f; // Oyuncunun sald�r� menzilinde ge�irdi�i s�re

    void Start()
    {
        // Enemy'nin Animator bile�enini al
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Oyuncuyla d��man aras�ndaki mesafeyi hesapla
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Oyuncu detectionRange i�inde mi?
        if (distanceToPlayer <= detectionRange)
        {
            FollowPlayer(); // Oyuncuyu takip et

            // E�er oyuncu attackRange i�indeyse isAttack true, de�ilse false
            if (distanceToPlayer <= attackRange)
            {
                isAttack = true;
                timeInAttackRange += Time.deltaTime; // Oyuncu sald�r� menzilindeyken s�reyi art�r

                // Oyuncu 1 saniyeden fazla attackRange i�inde durduysa sahneyi de�i�tir
                if (timeInAttackRange > 1f)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                isAttack = false;
                timeInAttackRange = 0f; // Sald�r� menzilinden ��karsa s�reyi s�f�rla
            }
        }
        else
        {
            isAttack = false; // Sald�r� modundan ��k
            StopFollowing(); // Takibi b�rak
            timeInAttackRange = 0f; // Menzilden ��kt���nda s�reyi s�f�rla
        }

        // Animat�r parametrelerini g�ncelle
        animator.SetFloat("Speed", isAttack || distanceToPlayer <= detectionRange ? speed : 0f); // H�z parametresini kontrol et
        animator.SetBool("isAttack", isAttack); // isAttack parametresini ayarla
    }

    void FollowPlayer()
    {
        // Oyuncuya do�ru hareket et
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Oyuncuya do�ru d�n
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void StopFollowing()
    {
        // H�z� s�f�rlay�nca d��man oldu�u yerde kal�r
        animator.SetFloat("Speed", 0f);
    }
}
