using UnityEngine;
using System.Collections;
using BestHTTP;
using System;
using UnityEngine.UI;

public class WebManager {
	Text mTopTen;
	string highScoreLink = "https://rankwatcher-rankwatcher.rhcloud.com/shieldrunner/getHighScore";
	string individualScoreLink = "https://rankwatcher-rankwatcher.rhcloud.com/shieldrunner/getIndividualRecord/";
	string saveRecordLink = "https://rankwatcher-rankwatcher.rhcloud.com/shieldrunner/saveRecord/";
	
	public void getTopTen(Text toptenText) {
		mTopTen = toptenText;
		string top = PlayerPrefs.GetString("TopTen", "[]");
		setTopTen(top);
		
		if (checkInternectConnection()) {
			HTTPRequest request = new HTTPRequest(new Uri(highScoreLink), delegate(HTTPRequest req, HTTPResponse res) {
				setTopTen(res.DataAsText);
				PlayerPrefs.SetString("TopTen", res.DataAsText);
			});
			request.Send();	
		}
	}
	
	private void setTopTen(string textList) {
		mTopTen.text = "";
		int i = 1;
		JSONObject topten = new JSONObject(textList);
		
		foreach(JSONObject j in topten.list){
			mTopTen.text += i + " " + j.GetField("name").str +"\n";
			i++;
		}
	}

	public void getIndividualScore() {
		string google_id = PlayerPrefs.GetString("google_id", "" );
		if (checkIsLogin() && checkInternectConnection()) {
			HTTPRequest request = new HTTPRequest(new Uri(individualScoreLink + google_id),
			 delegate(HTTPRequest req, HTTPResponse res) {
				JSONObject json = new JSONObject(res.DataAsText);
				if (json.Count > 0) {
					float score = json.GetField("score").n;
					if (score > PlayerPrefs.GetInt("HighScore", 0 )) {
						PlayerPrefs.SetInt("HighScore", (int)score );
					}
				}
					 
			});
			request.Send();
		}
	}

	public void saveHighScore() {
		string googleId = PlayerPrefs.GetString("google_id", "" ).ToString();
		string name =  PlayerPrefs.GetString("name", "hsin");
		string score = PlayerPrefs.GetInt("HighScore", 10 ).ToString();
			
		if (checkIsLogin()) {
			HTTPRequest request = new HTTPRequest( new Uri(saveRecordLink+ googleId +"/"+score+"/"+name ));
			request.Send();
		}
	}
	
	private bool checkIsLogin() {
		return (PlayerPrefs.GetString("google_id", "" ) == "") ? false : true;			
		
	}
	
	private bool checkInternectConnection() {
		var connectionTestResult = Network.TestConnection();
		return (connectionTestResult == ConnectionTesterStatus.Error) ? false : true;			
	}

	
}
