
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonGameOver : MonoBehaviour
{


    public void ResetearJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tilemap11111");
    }

}
