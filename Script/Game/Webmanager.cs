using UnityEngine;
using System.Collections;
using BestHTTP;
using System;
using UnityEngine.UI;

public class WebManager {
	Text mTopTen;

	public void getTopTen(Text toptenText) {
		mTopTen = toptenText;
		HTTPRequest request = new HTTPRequest(new Uri("http://rankwatcher.local:8000/shieldrunner/getHighScore"), receiveTopTen);
		request.Send();
	}
	
	private void receiveTopTen(HTTPRequest request, HTTPResponse response) {
		Debug.Log("Request Finished! Text received: " + response.DataAsText);
	}
	
	
}
