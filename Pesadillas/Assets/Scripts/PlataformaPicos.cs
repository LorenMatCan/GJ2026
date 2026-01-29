using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaPicos: MonoBehaviour
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

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 centro = Application.isPlaying ? puntoCentral : transform.position;

      
        Vector3 lastPoint = centro + new Vector3(Mathf.Cos(0) * distanciaHorizontal, Mathf.Sin(0) * distanciaVertical, 0);
        for (float i = 0.1f; i <= Mathf.PI * 2; i += 0.1f)
        {
            Vector3 nextPoint = centro + new Vector3(Mathf.Cos(i) * distanciaHorizontal, Mathf.Sin(i) * distanciaVertical, 0);
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
}
