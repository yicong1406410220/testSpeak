using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class net : MonoBehaviour {
    //服务器IP好端口号
    public InputField hostInput;
    public InputField portInput;

    //显示客户端收到的消息
    public Text recvText;
    public string recvStr;

    //显示客户端IP和端口
    public Text clientText;

    //聊天输入框
    public InputField textInput;

    //Socket和接收缓冲区
    Socket socket;
    const int BUFFER_SIZE = 1024;
    public byte[] readBuff = new byte[1024];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        recvText.text = recvStr;
	}
    /// <summary>
    /// 连接
    /// </summary>
    public void Connetion()
    {
        //清理
        recvText.text = "";
        //socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Connect
        string host = hostInput.text;
        int port = int.Parse(portInput.text);
        socket.Connect(host, port);
        clientText.text = "客户端地址" + socket.LocalEndPoint.ToString();
        //Recv
        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);

    }

    //接收回调
    private void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            //count是接收数据的大小
            int count = socket.EndReceive(ar);
            //数据处理
            string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
            if (recvStr.Length > 3000)
                recvStr = "";
            recvStr += str + "\n";
            //继续接收
            socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception e)
        {
            recvStr += "连接已断开";
            socket.Close();
        }
    }

    //发送数据
    public void Send()
    {
        string str = PlayerPrefs.GetString("name") + ":" +textInput.text;
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
        //Debug.Log(System.Text.Encoding.UTF8.GetString(bytes));       
        try
        {
            socket.Send(bytes);
            textInput.text = "";
        }
        catch
        {

        }
        


    }

}
