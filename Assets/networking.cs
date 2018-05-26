using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using System.Net;
using System;

public class networking : MonoBehaviour {

	NetworkStream stm;
	public GameObject chatBox;
	public InputField inputField;
	public Text label;

	// Use this for initialization
	void Start () {
		//Connect to server
		TcpClient tcpClient = new TcpClient();
		tcpClient.Connect("127.0.0.1", 3000);
		stm = tcpClient.GetStream();
	}
	
	// Update is called once per frame
	void Update () {
		//If there is new message
		if(stm.DataAvailable)
			GetMessage();
	}

	public void SendMessage()
	{
		//String to Bytes
		UTF8Encoding asen = new UTF8Encoding();
		byte[] ba=asen.GetBytes(inputField.text);
		stm.Write(ba,0,ba.Length);

		//Clear input field
		inputField.text = "";
	}

	void GetMessage()
	{
		Byte[] data = new Byte[256];
		String responseData = String.Empty;
		
		//Bytes to String
		Int32 bytes = stm.Read(data, 0,data.Length);
		responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);

		//Unity create label and add text
		Text labelC = Instantiate(label,transform.position,transform.rotation,chatBox.transform);
		labelC.text = responseData;
	}
}
