using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;

    [Header("General")]
    public float minimumTimeBetweenShots;
    public int maximumLiveBullets;

    private float m_timeSinceLastShot;
    private int m_liveBullets;
    private Impacter.ImpactTeam impactTeam;

    void Start()
    {
        Impacter parentImpacter = GetComponentInParent<Impacter>();
        if(parentImpacter != null)
        {
            impactTeam = parentImpacter.team;
        }

        impactTeam = gameObject.GetComponentInParent<Impacter>().team;

        m_timeSinceLastShot = 0;
        m_liveBullets = 0;
    }
    void Update()
    {
        m_timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (m_timeSinceLastShot >= minimumTimeBetweenShots && m_liveBullets <= maximumLiveBullets)
        {
            GameObject bulletGO = Instantiate(bulletPrefab);
            bulletGO.transform.position = gameObject.transform.position;

            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.OnBulletDeath += OnBulletDeath;
            
            if(impactTeam == Impacter.ImpactTeam.Enemy)
            {
                bullet.speed *= -1;
            }

            Impacter bulletImpacter = bulletGO.GetComponent<Impacter>();
            bulletImpacter.team = impactTeam;

            m_timeSinceLastShot = 0;
            m_liveBullets++;
        }
    }

    private void OnBulletDeath()
    {
        m_liveBullets--;
    }
}
