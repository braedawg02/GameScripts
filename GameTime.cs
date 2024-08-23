using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public Transform Sun;
    public new Light light;
    public static float timeScale = 1f;
    public static bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Sun = GetComponent<Transform>();
        light = GetComponent<Light>();
    }

    // Update is called once per frame
  void Update()
{
    Sun.RotateAround(Vector3.zero, Vector3.right, 1f * UnityEngine.Time.deltaTime);
    float rotation = Sun.eulerAngles.x;

    if (rotation > 180 && rotation < 360)
    {
        // Sun is below the ground, so set light intensity to 0
        light.intensity = 0;
        // Make the ambient light darker
        RenderSettings.ambientIntensity = 0.1f;
    }
    else if (rotation > 0 && rotation < 180)
    {
        // Sun is above the ground, so adjust light intensity based on rotation
        light.intensity = 0.000001f + (180 - rotation) / 180;
        // Make the ambient light brighter
        RenderSettings.ambientIntensity = 1f;
    }
    
    if (rotation >= 360)
    {
        Sun.eulerAngles = new Vector3(0, Sun.eulerAngles.y, Sun.eulerAngles.z);
    }
}
 }
