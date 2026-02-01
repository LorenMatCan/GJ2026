
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public static BGM BGMMenu;

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit(); 
    }

    
    public void CambiarEscena()
    {
        BGM.BGMMenu.CambiarMusica(1);  
        SceneManager.LoadScene("PrimerNivel");
    }

    public void Menu()
    {
        BGM.BGMMenu.CambiarMusica(0);  
         SceneManager.LoadScene("PantallaInicio"); 
    }
}
