using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports; // make sure to select ".Net Framework" in the "ApiCompatibilityLevel" (Edit>ProjectSettings>Player>)

public class Rotation_4 : MonoBehaviour
{
    public Transform ThumbFinger, Thumb2, IndexFinger, Index2, Index3, MiddleFinger, Middle2, Middle3, RingFinger, Ring2, Ring3, LittleFinger, Little2, Little3; // Objects For Rotation
    SerialPort data_stream = new SerialPort("/dev/cu.usbmodem2101", 19200); // Arduino is connected to /dev/cu.usbmodem2101, with 19200 Baud Rate
    public string receivedstring;

    // Start is called before the first frame update
    void Start()
    {
        data_stream.Open(); // Inititate the Serial Stream
        InvokeRepeating("Serial_Data_Reading", 0f, 0.0002f); // Gives time to read the data every 0.0001 seconds
    }

    // Update is called once per frame
    void Update()
    {
        int recv_angl = (-1) * Serial_Data_Reading(); // Getting Data from Serial port

        // This is how does eulerAngle work (only for y rotation)
        Debug.Log(recv_angl); // Display values
        // Debug.Log(recv_angl*(-1)); // Display values and inverse it in negative for accuracy
        
        // Thumb Finger
        ThumbFinger.transform.eulerAngles = new Vector3(0, recv_angl, 0);
        Thumb2.transform.eulerAngles = new Vector3(0, ThumbFinger.transform.eulerAngles.y + recv_angl, 0);

        // Index Finger
        IndexFinger.transform.eulerAngles = new Vector3(0, recv_angl, 0);
        Index2.transform.eulerAngles = new Vector3(0, IndexFinger.transform.eulerAngles.y + recv_angl, 0);
        Index3.transform.eulerAngles = new Vector3(0, Index2.transform.eulerAngles.y + recv_angl, 0);

        // Middle Finger
        MiddleFinger.transform.eulerAngles = new Vector3(0, recv_angl, 0);
        Middle2.transform.eulerAngles = new Vector3(0, MiddleFinger.transform.eulerAngles.y + recv_angl, 0);
        Middle3.transform.eulerAngles = new Vector3(0, Middle2.transform.eulerAngles.y + recv_angl, 0);

        // Ring Finger
        RingFinger.transform.eulerAngles = new Vector3(0, recv_angl, 0);
        Ring2.transform.eulerAngles = new Vector3(0, RingFinger.transform.eulerAngles.y + recv_angl, 0);
        Ring3.transform.eulerAngles = new Vector3(0, Ring2.transform.eulerAngles.y + recv_angl, 0);

        // Little Finger
        LittleFinger.transform.eulerAngles = new Vector3(0, recv_angl, 0);
        Little2.transform.eulerAngles = new Vector3(0, LittleFinger.transform.eulerAngles.y + recv_angl, 0);
        Little3.transform.eulerAngles = new Vector3(0, Little2.transform.eulerAngles.y + recv_angl, 0);
    }

    // Sync the unity and serial port frequency
    int Serial_Data_Reading()
    {
        // Reading the Serial Data Below------------------
        receivedstring = data_stream.ReadLine();
        //int recv_angl_data = receivedstring;
        int recv_angl_data = Mathf.RoundToInt(float.Parse(receivedstring)); // Round up the data obtained

        return recv_angl_data;
    }
}
