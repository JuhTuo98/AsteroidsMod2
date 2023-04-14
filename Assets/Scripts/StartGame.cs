using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static StartGame instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Application.LoadLevel("GameScene");
            Application.UnloadLevel("MenuScene");
        }
        
    }
}
