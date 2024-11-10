using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string YuklenecekSahne = "SahneAdiYaz"; // AnaMap sahnesinin adý

    public void ReturnToMainMap()
    {
        SceneManager.LoadScene(YuklenecekSahne); // AnaMap sahnesine geçiþ yap
    }
}