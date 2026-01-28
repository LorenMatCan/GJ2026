
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundChecker;
    public float checkDistance = 0.5f;
    public LayerMask groundLayer;

    private bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;
    public Sprite spriteEnemigoIluminado;
    public Sprite spriteEnemigo;

    public float velocidadHuida = 1f;
    private bool huyendo = false;

    private BoxCollider2D boxCollider;
    public Transform player;
    private Rigidbody2D playerRb;
    private bool enemigoIluminado = false;
    public GameObject rangoVision;


    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (enemigoIluminado)
            Huir();
        else
            Patrullaje();
    }

    void Patrullaje()
    {
        ///PATRULLAJE
        // Dirección según hacia dónde mira
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        transform.Translate(direction * speed * Time.deltaTime);

        // Raycast hacia abajo (desde delante)
        RaycastHit2D ray = Physics2D.Raycast(
            groundChecker.position,
            Vector2.down,
            checkDistance,
            groundLayer
        );

        // Si no hay suelo, girar
        if (!ray.collider)
        {
            Flip();
        }

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    //COMPORTAMIENTO CUANDO RECIBE LA LUZ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Luz"))
        {
            spriteRenderer.sprite = spriteEnemigoIluminado;
            enemigoIluminado = true;
            huyendo = true;
            boxCollider.isTrigger = true;
            rangoVision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Luz"))
        {
            spriteRenderer.sprite = spriteEnemigo;
            enemigoIluminado = false;
            boxCollider.isTrigger = false;
            rangoVision.gameObject.SetActive(true);
        }

    }

    void Huir()
    {
        // Dirección contraria al jugador (posición, no velocidad)
        float dir = transform.position.x - player.position.x;
        int fleeDir = dir > 0 ? 1 : -1;

        // Comprobar suelo antes de moverse
        RaycastHit2D ray = Physics2D.Raycast(
            groundChecker.position,
            Vector2.down,
            checkDistance,
            groundLayer
        );

        // Si no hay suelo delante, NO moverse
        if (!ray.collider)
            return;

        transform.Translate(Vector2.right * fleeDir * velocidadHuida * Time.deltaTime);

        // Girar sprite correctamente
        if (fleeDir > 0 && !isFacingRight) Flip();
        if (fleeDir < 0 && isFacingRight) Flip();
    }



}
