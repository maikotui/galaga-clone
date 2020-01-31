using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector Variables
    [Header("References")]
    public EnemyCharacterController enemyCharacterController;
    public Path divePath;

    [Header("Movement")]
    [Tooltip("The distance at which the enemy will switch target points")]
    public float speed = 1000;
    public float turnDistance = 1;

    // Properties
    public bool IsDiving { get; private set; }

    // Member variables
    private int m_divePathIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all inspector references have a reference
        if (enemyCharacterController == null)
        {
            Debug.LogError(name + " is missing an EnemyCharacterController reference.");
        }
        if (divePath == null)
        {
            Debug.LogError(divePath + " is missing a Path reference.");
        }

        IsDiving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDiving)
        {
            if (m_divePathIndex < divePath.points.Length)
            {
                Transform currentPoint = divePath.points[m_divePathIndex];

                float distance = Vector3.Distance(currentPoint.position, transform.position);

                if(distance < turnDistance)
                {
                    m_divePathIndex++;
                }
                else
                {
                    Vector3 direction = currentPoint.position - transform.position;
                    enemyCharacterController.rigidbody2d.velocity = direction * speed * Time.deltaTime;
                }
            }
        }
    }

    public void StartDive()
    {
        if (!IsDiving)
        {
            m_divePathIndex = 0;
            IsDiving = true;
        }
        else
        {
            Debug.LogError(name + " is already diving.");
        }
    }
}
