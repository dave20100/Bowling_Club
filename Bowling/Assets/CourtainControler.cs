using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtainControler : MonoBehaviour
{
    private Vector3 originPos;
    private bool _moveCourtine { get; set; } = false;
    public bool readyToReset { get; set; } = false;
    PinsGameControler _pgc;
    float countDown = -2.0f;

    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown > 0)
        {
            transform.position += Vector3.down * 5 * Time.deltaTime;
            if (countDown < 0.1 && readyToReset)
            {
                _pgc.ProcessPins();
                readyToReset = false;
            }
        }
        else if (countDown > -2.0f) 
            transform.position += Vector3.up * 5* Time.deltaTime;
    }
        
    public void MoveCourtine(PinsGameControler pgc )
    {
        _pgc = pgc;
        transform.position = originPos;
        countDown = 2.0f;
        readyToReset = true;

    }

}
