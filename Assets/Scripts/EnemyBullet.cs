using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    Vector2 _direction; // za pravac metka
    bool isReady; // kada je direction metka setovan

    //default vrednosti u Awake funkciji
    void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // da postavi pravac metka
    public void SetDirection(Vector2 direction)
    {
        // mora normalizovano, kako bi se dobio unit vektor
        _direction = direction.normalized;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            //trenutna poz
            Vector2 position = transform.position;

            // izracunaj novu
            position += _direction * speed * Time.deltaTime;

            // update
            transform.position = position;

            // ako izadje sa ekrana - remove
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag"))
        {
            Destroy(gameObject); // unistimo nas brod
        }
    }
}
