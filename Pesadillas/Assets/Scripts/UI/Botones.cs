
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit(); 
    }

    
    public void CambiarEscena()
    {
        SceneManager.LoadScene("´PrimerNivel");
    }
}
