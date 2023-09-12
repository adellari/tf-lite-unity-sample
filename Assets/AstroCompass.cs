using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroCompass : MonoBehaviour
{
    public float Az;
    public float At;

    public float altitude;
    public float latitude;

    public bool isLoc = false;

    public IEnumerator Start()
    {
        Input.location.Start();
        Input.gyro.enabled = true;
        
        

        

        

        var locWait = 10;

        while(Input.location.status == LocationServiceStatus.Initializing && locWait > 0)
        {
            yield return new WaitForSeconds(1);
            locWait--;
        }

        if (locWait < 1)
        {
            Debug.LogWarning("Timed out waiting for location services!");
            isLoc = false;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Failed initializing location services");
            isLoc = false;
            yield break;
        }
        else
        {
            isLoc = true;
            altitude = Input.location.lastData.altitude;
            latitude = Input.location.lastData.latitude;

            Az = Input.compass.trueHeading;
            Debug.Log("Successfully obtained geographic data");
        }


        // Input.location.Stop();

    }


    

    // Update is called once per frame
    void Update()
    {
        Quaternion deviceRotation = Input.gyro.attitude;
        deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);
        float pitch = deviceRotation.eulerAngles.x;

        float altitude;
        if (pitch <= 180f)
            altitude = 90f - pitch;
        else altitude = pitch - 270f;

        if (isLoc)
            Az = Input.compass.trueHeading;
        Debug.Log("Altitude: " + altitude);
        Debug.Log("Azimuth: " + Az);
    }

    private void OnApplicationQuit()
    {
        Input.location.Stop();
        Input.gyro.enabled = false;
    }
}
