using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO; 

    public GameObject PlayerBulletGO; 
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    public GameObject ExplosionGO;

    public float speed;  

    public Text LivesUIText; 
    const int MAX_LIVES = 3;
    int lives; 

    public void Init()
    {
        lives = MAX_LIVES;
        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();

            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position; 

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal"); 
        float y = Input.GetAxisRaw("Vertical"); 

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); 
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); 

        

        max.x = max.x - 0.0225f; 
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);

                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
