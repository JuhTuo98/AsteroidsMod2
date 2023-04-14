using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject DeadShip;
    [SerializeField] GameObject Asteroid;
    [SerializeField] GameObject MiniAsteroid;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject Shotgun;
    [SerializeField] GameObject Heal;

    [SerializeField] float MinSpawnTimeAst;
    [SerializeField] float MaxSpawnTimeAst;
    float spawnTimeAst;
    [SerializeField] float MinSpawnTimePup;
    [SerializeField] float MaxSpawnTimePup;
    float spawnTimePup;

    bool toggleButtons = false;
    bool toggleAsteroidSpawner = true;
    bool togglePowerUpSpawner = true;

    public static GameManager instance;

    

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        
        spawnTimeAst = Random.Range(MinSpawnTimeAst, MaxSpawnTimeAst);
        spawnTimePup = Random.Range(MinSpawnTimePup, MaxSpawnTimePup);
    }

    void OnGUI()
    {
        toggleButtons = GUI.Toggle(new Rect(0, 0, 100, 15), toggleButtons, "Show buttons");

        if (toggleButtons)
        {

            if (GUI.Button(new Rect(10, 15, 100, 25), "Spawn Asteroid"))
            {
                SpawnAsteroid(1, 5f, 10f, 1000f, 5f, 50f, 30f);
            }

            toggleAsteroidSpawner = GUI.Toggle(new Rect(125, 15, 100, 25), toggleAsteroidSpawner, "Toggle AstSpawner");

            if (GUI.Button(new Rect(10, 50, 100, 25), "Spawn Shield"))
            {
                SpawnShield(1, 5f, 10f, 1000f);
            }
            
            togglePowerUpSpawner = GUI.Toggle(new Rect(125, 50, 100, 25), togglePowerUpSpawner, "Toggle PuPSpawner");

            if (GUI.Button(new Rect(10, 85, 100, 25), "Spawn Shotgun"))
            {
                SpawnShotgun(1, 5f, 10f, 1000f);
            }

            if (GUI.Button(new Rect(10, 120, 100, 25), "Spawn Heal"))
            {
                SpawnHeal(1, 5f, 10f, 1000f);
            }
        }
    }

    public void SpawnAsteroid(int number, float xRang, float yRang, float rotRang, float sizeRang, float forceRang, float torqRang)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 randPos = new Vector2(Random.Range(-xRang, xRang), Random.Range(-yRang, yRang));
            float randSize = Random.Range(1, sizeRang);
            float randForce = Random.Range(-forceRang, forceRang);
            float randTorque = Random.Range(-torqRang, torqRang);
            Quaternion randRot = Quaternion.Euler(0, 0, Random.Range(-rotRang * 100, rotRang * 100));
            GameObject newAst;
            if (randSize < 2) newAst = Instantiate(MiniAsteroid, randPos, randRot);
            else newAst = Instantiate(Asteroid, randPos, randRot);

            Rigidbody2D astRb = newAst.GetComponent<Rigidbody2D>();

            newAst.transform.localScale = new Vector2(randSize, randSize);
            astRb.AddForce(newAst.transform.up * (randForce * 200) * Time.deltaTime);
            astRb.AddTorque(randTorque * Time.deltaTime);
            

            Destroy(newAst, Random.Range(5f, 30f));

        }
    }

    void SpawnShield(int number, float xRang, float yRang, float rotRang)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 randPos = new Vector2(Random.Range(-xRang, xRang), Random.Range(-yRang, yRang));
            Quaternion randRot = Quaternion.Euler(0, 0, Random.Range(-rotRang * 100, rotRang * 100));
            GameObject newShield = Instantiate(Shield, randPos, randRot);
            newShield.GetComponent<PowerUp>().isShield = true;

            Destroy(newShield, Random.Range(15f, 60f));

        }
    }

    void SpawnShotgun(int number, float xRang, float yRang, float rotRang)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 randPos = new Vector2(Random.Range(-xRang, xRang), Random.Range(-yRang, yRang));
            Quaternion randRot = Quaternion.Euler(0, 0, Random.Range(-rotRang * 100, rotRang * 100));
            GameObject newGun = Instantiate(Shotgun, randPos, randRot);
            newGun.GetComponent<PowerUp>().isGun = true;
            
            Destroy(newGun, Random.Range(15f, 60f));

        }
    }

    void SpawnHeal(int number, float xRang, float yRang, float rotRang)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 randPos = new Vector2(Random.Range(-xRang, xRang), Random.Range(-yRang, yRang));
            Quaternion randRot = Quaternion.Euler(0, 0, Random.Range(-rotRang * 100, rotRang * 100));
            GameObject newHeal = Instantiate(Heal, randPos, randRot);
            newHeal.GetComponent<PowerUp>().isHeal = true;
            
            Destroy(newHeal, Random.Range(15f, 60f));
            
        }
    }

    void SpawnAsteroidsTimed()
    {
        if (spawnTimeAst <= 0)
        {
            SpawnAsteroid(Random.Range(1, 3), 5f, 10f, 1000f, 5f, 100f, 30f);
            spawnTimeAst = Random.Range(MinSpawnTimeAst, MaxSpawnTimeAst);
        }

        spawnTimeAst -= Time.deltaTime;
    }
    
    void SpawnPowerUpsTimed()
    {
        if (spawnTimePup <= 0)
        {
            switch(Random.Range(0, 3))
            {
                case 0: SpawnShield(1, 5f, 10f, 1000f); break;
                case 1: SpawnShotgun(1, 5f, 10f, 1000f); break;
                case 2: SpawnHeal(1, 5f, 10f, 1000f); break;
            }

            spawnTimePup = Random.Range(MinSpawnTimePup, MaxSpawnTimePup);
        }

        spawnTimePup -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleAsteroidSpawner) SpawnAsteroidsTimed();
        if (togglePowerUpSpawner) SpawnPowerUpsTimed();
        if (UI.instance.life <= 0) 
        {
            if (Player != null)
            {
                Instantiate(DeadShip, Player.transform.position, Quaternion.Euler(0, 0, 0));
                
                Destroy(Player);
            }

            
            
        }

    }
}
