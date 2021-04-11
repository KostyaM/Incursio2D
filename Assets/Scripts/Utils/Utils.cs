using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Utils
{
    public static Vector3 GetMouseWorldPosition() 
    {
        return GetAbsolutePointPosition(Input.mousePosition);
    }

    public static Vector3 GetAbsolutePointPosition(Vector3 point)
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(point);
        vec.z = 0f;
        return vec;
    }


    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return null;
    }
}
