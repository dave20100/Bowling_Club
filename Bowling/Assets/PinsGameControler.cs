using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinsGameControler : MonoBehaviour
{

    GameObject[] pinsList;
    GameObject detector;
    List<Vector3> pinPosList;
    List<Quaternion> pinRotList;

    // Start is called before the first frame update
    void Start()
    {
        pinsList = GameObject.FindGameObjectsWithTag("Pin");
        detector = GameObject.FindGameObjectWithTag("PinDetectorGame");

        pinPosList = new List<Vector3>();
        pinRotList = new List<Quaternion>();
        foreach (var pin in pinsList)
        {
            pinPosList.Add(pin.transform.position);
            pinRotList.Add(pin.transform.rotation);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveKnockedOut()
    {
        foreach (GameObject pin in pinsList)
        {
            if (!pin.GetComponent<Collider>().bounds.Intersects(detector.GetComponent<Collider>().bounds))
            {
                pin.gameObject.SetActive(false);
            }
        }
    }

    public void ResetPins()
    {
        for (int i = 0; i < pinsList.Length; i++)
        {
            pinsList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pinsList[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pinsList[i].transform.position = pinPosList[i];
            pinsList[i].transform.rotation = pinRotList[i];
            pinsList[i].gameObject.SetActive(true);

        }
    }

    public void ProcessPins()
    {
        RemoveKnockedOut();
        if(!pinsList.Any(x => x.active == true))
        {
            ResetPins();
        }
    }

}
