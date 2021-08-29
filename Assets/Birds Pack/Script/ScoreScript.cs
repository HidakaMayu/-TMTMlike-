using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

	public static int score;

	void Start()
	{
		score = 0;
	//初期スコア(0点)を表示
	GetComponent<Text>().text = "Score: " + score.ToString();
	}
	//ballScriptからSendMessageで呼ばれるスコア加算用メソッド
	public void AddPoint(int point)
	{
		score += point;
		GetComponent<Text>().text = "Score: " + score.ToString();
	}
}
//3 * i(連鎖回数-2)がスコア。　score += 3 * i;　追加されるたび処理を行う
