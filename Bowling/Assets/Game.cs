using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    BallController ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Icosphere").GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.Shoot();
        }
    }



}
