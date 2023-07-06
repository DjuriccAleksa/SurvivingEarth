using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO; // bullet prefab

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // da ispali metke
    void FireEnemyBullet()
    {
        //referenca ka nasem igracu
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null) // ako nije mrtav
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            // initial position
            bullet.transform.position = transform.position;

            // izracunaj direction ka igracu
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // postavi bullets direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
