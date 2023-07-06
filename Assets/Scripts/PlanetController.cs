using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;

    Queue<GameObject> availablePlanes = new Queue<GameObject> ();

    // Start is called before the first frame update
    void Start()
    {
        availablePlanes.Enqueue(Planets[0]);
        availablePlanes.Enqueue(Planets[1]);
        availablePlanes.Enqueue(Planets[2]);

        InvokeRepeating("MovePlanetDown",0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MovePlanetDown()
    {
        EnqueuePlanets();

        if (availablePlanes.Count == 0)
            return;

        GameObject aPlanet = availablePlanes.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach(GameObject aPlanet in Planets)
        {
            if((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                // restartuj poziciju
                aPlanet.GetComponent<Planet>().ResetPosition();

                availablePlanes.Enqueue(aPlanet);
            }
        }
    }
}
