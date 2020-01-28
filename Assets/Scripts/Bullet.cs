using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Impacter))]
public class Bullet : MonoBehaviour
{
    public float timeAlive = 3f;
    public float speed = 3f;

    public delegate void DestroyHandler();
    public event DestroyHandler OnBulletDeath;

    Impacter impacter;

    void Start()
    {
        Destroy(gameObject, timeAlive);
        impacter = GetComponent<Impacter>();
        impacter.OnImpact += ImpactHandler;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
    }

    void OnDestroy()
    {
        OnBulletDeath?.Invoke();
    }

    private void ImpactHandler(int damage, Collider2D collision)
    {
        Destroy(gameObject);
    }
}
