using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // brzina metka
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // trenutna pozicija metka
        Vector2 position = transform.position;

        // nova pozicija
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // update
        transform.position = position;

        // kad ode van ekrana - unisti ga - memoriju cuvamo
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag"))
        {
            Destroy(gameObject); // unistimo nas metak
        }
    }
}
