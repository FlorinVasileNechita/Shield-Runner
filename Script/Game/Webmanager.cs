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
		mTopTen.text = "";
		int i = 0;
		JSONObject topten = new JSONObject(response.DataAsText);

		foreach(JSONObject j in topten.list){
			i++;
			mTopTen.text += i + " " + j.GetField("name").str +"\n";
		}

	}

	public void getIndividualScore(Text yourscore) {

		if (PlayerPrefs.GetInt("google_id", 0 ) != 0) {
			HTTPRequest request = new HTTPRequest(new Uri("https://google.com"), delegate(HTTPRequest req, HTTPResponse res) {
				JSONObject score = new JSONObject(res.DataAsText);

				yourscore.text = score.GetField("score").n +" You";
			});
			request.Send();
		}
	} 

	public void saveHighScore() {
		HTTPRequest request = new HTTPRequest(new Uri("http://server.com/path"), HTTPMethods.Post);
		request.AddField("google_id", PlayerPrefs.GetInt("google_id", 0 ).ToString());
		request.AddField("name", PlayerPrefs.GetString("name", ""));
		request.AddField("score", PlayerPrefs.GetInt("HighScore", 0 ).ToString());
		request.Send();
	}


	
}
