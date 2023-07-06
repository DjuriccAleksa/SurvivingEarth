using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO; // gm

    public GameObject PlayerBulletGO; // prefab za metak
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    public GameObject ExplosionGO;

    public float speed;  // za brzinu naseg broda

    public Text LivesUIText; // referenca ka zivotima
    const int MAX_LIVES = 3;
    int lives; // trenutni zivoti

    public void Init()
    {
        lives = MAX_LIVES;
        LivesUIText.text = lives.ToString();

        // reset na center ekrana igraca
        transform.position = new Vector2(0, 0);

        // postavi na aktivnog
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // pucamo na space
        if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();

            // instanciramo metak
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position; // inicijalna pozicija

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal"); // -1 0 1 levo nista desno
        float y = Input.GetAxisRaw("Vertical"); // -1 0 1 dole nista gore

        // u zavisnosti od unosa, pravimo direction vector -> a zatim ga normalizujemo u unit vector
        Vector2 direction = new Vector2(x, y).normalized;

        // sad funkcija koja racuna i setuje player poziciju
        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        // da ogranicimo kuda moze da ide - nadjemo ivice ekrana (coskove)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // dole levo
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // gore desno

        // visina i sirina naseg broda isto se uzima u obzir

        max.x = max.x - 0.0225f; // pola sirine onog sprajta
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        // trenutna pozicija broda
        Vector2 pos = transform.position;

        // nova pozicija
        pos += direction * speed * Time.deltaTime;

        // ne sme van ekrana
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // update
        transform.position = pos;
    }

    // trigerovace se kad dodje do kolizije game objectsa
    void OnTriggerEnter2D(Collider2D col)
    {
        // sa protivnickim brodom ili metkovima
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                // gm state na gameover
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);

                // Destroy(gameObject); // unistimo nas brod --- sakricemo ga
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
