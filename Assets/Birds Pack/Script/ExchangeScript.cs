﻿using UnityEngine;
using System.Collections;

public class ExchangeScript : MonoBehaviour
{
	//public birdScript BirdScript;

	public void Exchange()
	{
		//配列に「respawn」タグのついているオブジェクトを全て格納
		GameObject[] piyos = GameObject.FindGameObjectsWithTag("Respawn");
		//全て取り出し、削除
		foreach (GameObject obs in piyos)
		{
			Destroy(obs);
		}
		//ballScriptのDropBallメソッドを実行し、50のひよこを作成
	//	BirdScript.SendMessage("DropBall", 50);
	}
}