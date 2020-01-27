using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The prefab of the stars that will be loaded into the game area")]
    public GameObject starPrefab;

    [Header("General")]
    public float timeBetweenStarCreation = 10.0f;
    public float starLifespan = 10.0f;
    public float maximumLifespanOffset = 3f;
    public int initialStarsCreated = 16;
    public int initialStaticStars = 10;

    [Header("Star Creation Area")]
    [Tooltip("The width of the box which stars can be instantiated from")]
    public float width;
    [Tooltip("The height of the box which stars can be instantiated from")]
    public float height;

    private float m_timeSinceLastStarCreation;

    void Start()
    {
        for (int i = 0; i < initialStaticStars; i++)
        {
            InstantiateNewStar(true, 10);
        }

        for (int i = 0; i < initialStarsCreated - initialStaticStars; i++)
        {
            float lifespan = starLifespan + Random.Range(0, maximumLifespanOffset) + (i * 2);

            InstantiateNewStar(false, starLifespan);
        }

        m_timeSinceLastStarCreation = 0;
    }

    private void InstantiateNewStar(bool ignoreLifeSpan, float lifespan)
    {
        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);

        GameObject star = GameObject.Instantiate(starPrefab, new Vector3(x, y, 0), Quaternion.identity);
        star.transform.parent = gameObject.transform;
        StarController starController = star.GetComponent<StarController>();
        starController.forceStayAlive = ignoreLifeSpan;
        starController.lifespan = lifespan;

        SpriteRenderer starSprite = star.GetComponent<SpriteRenderer>();
        if (starSprite != null)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            starSprite.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeSinceLastStarCreation >= timeBetweenStarCreation)
        {
            InstantiateNewStar(false, starLifespan);
            m_timeSinceLastStarCreation = 0;
        }
        else
        {
            m_timeSinceLastStarCreation += Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
#endif
    }
}
