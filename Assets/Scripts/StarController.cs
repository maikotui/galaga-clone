using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public bool forceStayAlive { get; set; }

    public float lifespan { get; set; }

    void Start()
    {
        if (!forceStayAlive)
        {
            Destroy(gameObject, lifespan);
        }
    }
}
