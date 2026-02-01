using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFlotante : MonoBehaviour
{
    [Header("Configuración del Movimiento")]
    [Tooltip("Qué tan lejos se mueve a los lados (Izquierda/Derecha)")]
    public float distanciaHorizontal = 3f;

    [Tooltip("Qué tan lejos se mueve arriba y abajo")]
    public float distanciaVertical = 3f;

    [Tooltip("Velocidad de giro")]
    public float velocidad = 2f;

    private Vector3 puntoCentral;

    void Start()
    {
        //
        puntoCentral = transform.position;
    }

    void Update()
    {
        float x = Mathf.Cos(Time.time * velocidad) * distanciaHorizontal;
        float y = Mathf.Sin(Time.time * velocidad) * distanciaVertical;

        transform.position = puntoCentral + new Vector3(x, y, 0);
    }

   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            collision.transform.SetParent(null);
        }
    }

    
    
}