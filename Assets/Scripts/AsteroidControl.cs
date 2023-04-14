using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    [SerializeField] int MaxHP;
    int HP;
    Rigidbody2D rb;
    SpriteRenderer rend;

    bool isMini;

    public static AsteroidControl instance;

    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;

        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        HP = MaxHP;
        isMini = transform.localScale.x < 2f;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.transform.tag == "Bullet")
        {
            if (GameObject.Find("Player").GetComponent<PlayerControl>().hasShotgun) HP -= 1;
            else HP -= 2;

            rend.material.SetColor("_EmissionColor", Color.white);
            

            Destroy(other.gameObject);

        }


        if (other.transform.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControl>().hasShield)
            {
                Debug.Log("Crash! Asteroid and shield both break");
                HP -= 1000;
                foreach (GameObject pobject in GameObject.FindGameObjectsWithTag("PowerUp"))
                {
                    if (pobject.GetComponent<PowerUp>().isPicked) 
                        if (pobject.GetComponent<PowerUp>().isShield) Destroy(pobject);
                }
                
                return;
            }

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-other.gameObject.GetComponent<Rigidbody2D>().velocity * 100f);
            UI.instance.UpdateLives(-1);
        }

    }
    
    void OnDestroy()
    {
        if (!isMini) GameManager.instance.SpawnAsteroid(Random.Range(1, 3), transform.position.x, transform.position.y, 2000f, 2f, 100f, 100f);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
            UI.instance.UpdateScore(10);
        }
    }
}
