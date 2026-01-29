using UnityEngine;

public class PerseguirJugador : MonoBehaviour
{
    public ComportamientoEnemigo enemigo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // enemigo.persiguiendoJugador = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //enemigo.persiguiendoJugador = false;
        }
    }
}
