using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject visualizer;
    public RectTransform panel; 

    Vector3 upside;
    public Quaternion startR;
    
    void Start()
    {
        Input.gyro.enabled = true;
        var gyro = Input.gyro.attitude;
        gyro = new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w);
        startR = gyro; //visualizer.transform.rotation;
        visualizer.transform.rotation = startR;

        upside = visualizer.transform.up;
    }

    void getOrientation()
    {
    Quaternion gyro = Input.gyro.attitude;
    gyro = new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w); // convert the gyroscope data
    var difference = gyro * Quaternion.Inverse(startR);

    float angle;
    Vector3 axis;

    difference.ToAngleAxis(out angle, out axis);
    axis.Normalize();

    var dotted = Mathf.Abs(Vector3.Dot(axis, upside));
    Debug.Log(dotted * angle);

    visualizer.transform.rotation = gyro;
    }


    // Update is called once per frame
    void Update()
    {
        getOrientation();
    }
}
