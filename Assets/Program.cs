using UnityEngine;
using System.Collections;
using SocketConnection;
using System.Threading;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.IO;
using LitJson;

public class Program : MonoBehaviour {


    public UILabel ipShow;
    

    public SocketServer sockerServer = new SocketServer();
    BackgroundWorker checkConnection = new BackgroundWorker();
    
	// Use this for initialization
	void Start () {

        
       
        StartCoroutine(LocalIPCheck());
        DontDestroyOnLoad(gameObject);
	}

    IEnumerator LocalIPCheck()
    {
        while (true)
        {
            if (IsNeworkConnect() == true)
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ipa in host.AddressList)
                {
                    if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ipa.ToString();

                    }
                }
                sockerServer.Start(5656);
                sockerServer.cmd_callback += NetworkMsg;
                ipShow.text = localIP;
                break;
            }
            else
            {
                ipShow.text = "wait for wifi connection...";
            }
            yield return null;
        }
    }
   
	// Update is called once per frame
	void Update () {
	
	}
    bool IsNeworkConnect()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)//无网络
        {
            return false;
        }
        else
        {
            Debug.Log(Application.internetReachability);
            return true;
        }
    }
    void NetworkMsg(string msg)
    {
        Debug.Log(msg);
    }
    void OnApplicationQuit()
    {
      
    }
}
