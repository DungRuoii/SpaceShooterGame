using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject GameControll;

    public GameObject PlayerBullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject Explosion;

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;

    public void Init()
    {
        lives = MaxLives;

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
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = bulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x -= 0.225f;
        min.x += 0.225f;

        max.y -= 0.285f;
        min.y += 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();
            
            if(lives == 0)
            {
                GameControll.GetComponent<GameController>().SetGameControllerState(GameController.GameControllerState.GameOver);

                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }
}
