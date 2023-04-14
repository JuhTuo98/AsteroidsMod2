using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAround : MonoBehaviour
{
    float halfScreenWidth = 11.2f;
    float halfScreenHeight = 5.2f;

    public static WarpAround instance;

    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 xVect = Vector3.right * halfScreenWidth * 2;
        Vector3 yVect = Vector3.up * halfScreenHeight * 2;

        if (transform.position.x > halfScreenWidth) transform.position -= xVect;
        if (transform.position.x < -halfScreenWidth) transform.position += xVect;
        if (transform.position.y > halfScreenHeight) transform.position -= yVect;
        if (transform.position.y < -halfScreenHeight) transform.position += yVect;
    }
}
