using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallConfig ballConfig = new BallConfig();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        ballConfig = new BallConfig();
    }

    public void Shoot()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(400 * Vector3.right, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 1));
    }
}


public class BallConfig
{
    private int horizontalPossition = 0;
    public int HorizontalPossition
    {
        get => horizontalPossition;
        set
        {
            horizontalPossition = value > 50 ? 50 :
                value < -50 ? -50 : value;
        }
    }

    private int streinght = 0;
    public int Streinght
    {
        get => streinght;
        set
        {
            streinght = value > 50 ? 50 :
                value < -50 ? -50 : value;
        }
    }

    private int hight = 0;
    public int Hight
    {
        get => hight;
        set
        {
            hight = value > 100 ? 100 :
                value < 0 ? 0 : value;
        }
    }

    //public object Clone()
    //{
    //    return this.MemberwiseClone();
    //}

    public void Reset()
    {
        horizontalPossition = 0;
        streinght = 0;
        hight = 0;
    }
}