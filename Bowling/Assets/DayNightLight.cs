using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLight : MonoBehaviour
{
    // Start is called before the first frame update

    private Light[] lights;
    private GameObject[] glassArray;
    private GameObject[] camerasPreview;
    bool isNight;
    bool isPreviewDisabled;

    void Start()
    {
        lights = FindObjectsOfType(typeof(Light)) as Light[];
        glassArray = GameObject.FindGameObjectsWithTag("glass");
        camerasPreview = GameObject.FindGameObjectsWithTag("cameraPreview");

        isNight = false;
        isPreviewDisabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isNight = !isNight;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPreviewDisabled = !isPreviewDisabled;
        }

        foreach (GameObject camera in camerasPreview)
        {
            if (isPreviewDisabled)
            {
                camera.active = false;
            }
            else
            {
                camera.active = true;
            }
        }

        foreach (Light light in lights)
        {
            if (isNight)
            {
                foreach (GameObject glass in glassArray)
                {
                    var glassRenderer = glass.GetComponent<Renderer>();
                    glassRenderer.material.SetColor("_EmissionColor", Color.black);
                }

                if (light.name == "SunRayLight")
                {
                    light.intensity = 0;

                }
                else if (light.name == "SunGlowLight")
                {
                    light.intensity = 0;
                }
                else
                {
                    light.intensity = 1.3f;
                    light.color = new Color(0.8f, 0.85f, 1f, 1);
                }
            }
            else
            {
                foreach (GameObject glass in glassArray)
                {
                    var glassRenderer = glass.GetComponent<Renderer>();
                    glassRenderer.material.SetColor("_EmissionColor", Color.white);
                }


                if (light.name == "SunRayLight")
                {
                    light.intensity = 50f;

                }
                else if (light.name == "SunGlowLight")
                {
                    light.intensity = 2f;
                }
                else
                {
                    light.intensity = 1.5f;
                    light.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}
