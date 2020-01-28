using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Impacter), typeof(Shooter))]
public class PlayerController : MonoBehaviour
{
    // Required Components need no reference
    public Health health;
    public Impacter impacter;
    public Shooter shooter;

    [Header("References")]
    [Tooltip("The health controller for the player")]
    public Rigidbody2D rb;

    [Header("General")]
    [Tooltip("The number of lives the player has")]
    public int lives;
    [Tooltip("The speed of the ship")]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += HandleDeath;
        impacter = GetComponent<Impacter>();
        impacter.OnImpact += ImpactHandler;
        shooter = GetComponent<Shooter>();

        rb = GetComponent<Rigidbody2D>();
    }

    void ImpactHandler(int damageTakenOnImpact, Collider2D collision)
    {
        GameObject source = collision.gameObject;

        if (source.GetComponent<Impacter>() is Impacter impacter && impacter != null)
        {
            health.TakeDamage(impacter.damageGivenOnImpact, source);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
        HandleMovement();
    }

    void HandleShooting()
    {
        if(Input.GetAxis("Fire1") != 0)
        {
            shooter.Shoot();
        }
    }

    private void HandleMovement()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void HandleDeath(int damageTaken, GameObject killer)
    {
        Debug.Log("Player died");
    }
}
