using UnityEngine;

public class RangoVision : MonoBehaviour
{
    public ComportamientoEnemigo enemigo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            enemigo.SetPersiguiendo(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            enemigo.SetPersiguiendo(false);
        }
    }
}
