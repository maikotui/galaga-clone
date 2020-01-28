using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacter : MonoBehaviour
{
    public delegate void ImpactHandler(int damageTakenOnImpact, Collider2D source);
    public event ImpactHandler OnImpact;

    public ImpactTeam team;
    public int damageGivenOnImpact;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Impacter>() is Impacter impacter && impacter != null)
        {
            if (team != impacter.team)
                OnImpact(impacter.damageGivenOnImpact, collision);
        }
    }

    public enum ImpactTeam
    {
        Player,
        Enemy,
        Object
    }
}
