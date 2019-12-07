using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    BallController ball;
    string ballConfig;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Icosphere").GetComponent<BallController>();
        ball.Reset();
        //ball.Setup(ball.transform.position, startRotation);
    }

    // Update is called once per frame
    void Update()   
    {
        if (Input.anyKey)
        {
            ball.BallSetup();
            ballConfig = ball.GetCoords();
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        ballConfig = ball.GetCoords();

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 3 / 100;
        style.normal.textColor = Color.grey;
        GUI.Label(rect, ballConfig, style);
    }
}
