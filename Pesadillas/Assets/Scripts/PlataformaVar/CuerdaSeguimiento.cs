using UnityEngine;

public class CuerdaSeguimiento : MonoBehaviour
{
    public Transform puntoFijo;
    public Transform puntoMovil;

    private float largoOriginal;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Tamaño REAL del sprite sin escala
        largoOriginal = sr.sprite.bounds.size.x;
    }

    void LateUpdate()
    {
        transform.position = puntoFijo.position;

        Vector3 dir = puntoMovil.position - puntoFijo.position;

        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        float distancia = dir.magnitude;

        float escalaX = distancia / largoOriginal;

        Vector3 escalaPadre = transform.parent.lossyScale;

        transform.localScale = new Vector3(
            escalaX / escalaPadre.x,
            1f / escalaPadre.y,
            1f
        );

    }
}
