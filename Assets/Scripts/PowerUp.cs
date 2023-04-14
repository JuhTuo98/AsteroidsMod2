using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isPicked;
    public bool isShield;
    public bool isGun;
    public bool isHeal;

    GameObject Player;

    public static PowerUp instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;

        Player = GameObject.Find("Player");

    }

    void OnDestroy()
    {
        if (isPicked) 
        {
            if (isShield) 
            {
                Player.GetComponent<PlayerControl>().hasShield = false;
                Player.GetComponent<PlayerControl>().Shield = null;
            }
            if (isGun) 
            {
                Player.GetComponent<PlayerControl>().hasShotgun = false;
                Player.GetComponent<PlayerControl>().Shotgun = null;
            }

            isPicked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;
        
        isPicked = Vector2.Distance(
            Player.transform.position, 
            transform.position) < 1;

        if (isPicked) 
        {
            if (isGun)
            {
                Player.GetComponent<PlayerControl>().hasShotgun = true;
                Player.GetComponent<PlayerControl>().Shotgun = this;
            }

            if (isShield)
            {
                Player.GetComponent<PlayerControl>().hasShield = true;
                Player.GetComponent<PlayerControl>().Shield = this;
            }

            if (isShield || isGun)
            {
                transform.parent = Player.transform;
                transform.position = Player.transform.position;
                gameObject.layer = 1;
                transform.localScale = new Vector2(2f, 2f);
                Destroy(gameObject, 15f);
            }
            else
            {
                UI.instance.UpdateLives(1);
                Destroy(gameObject);
            }
        }
    }
}
