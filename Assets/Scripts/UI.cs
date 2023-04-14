using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;

    public int score;
    public int life;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
    }

    public void UpdateLives(int lifeChange)
    {
        life += lifeChange;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{score}";
        lifeText.text = $"{life}";

    }
}
