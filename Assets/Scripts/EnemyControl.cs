using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    float speed; // brzina neprijatelja

    GameObject scoreUITextGO; // kada umre enemy, azuriramo score
    public GameObject ExplosionGO;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        // trenutna pozicija
        Vector2 position = transform.position;

        // izracunaj novu poz
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        // update
        transform.position = position;

        // donje leva ivica
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            Destroy(gameObject); // unistimo nas brod
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
