using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{

	//　トータル制限時間
	private float totalTime;
	//　制限時間（分）
	private int minute = 0;
	//　制限時間（秒）
	private float seconds = 60.0f;
	//ゲームオーバー文字
	[SerializeField]
	Text moji;
	//　前回Update時の秒数
	private float oldSeconds;
	private Text timerText;

	//ホイッスル
	public AudioClip finish1;
	AudioSource audioSource;

	void Start()
	{
		totalTime = minute * 60 + seconds;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		//　制限時間が0秒以下なら何もしない
		if (totalTime <= 0f)
		{
			return;
		}
		//　一旦トータルの制限時間を計測；
		totalTime = minute * 60 + seconds;
		totalTime -= Time.deltaTime;

		//　再設定
		minute = (int)totalTime / 60;
		seconds = totalTime - minute * 60;

		//　タイマー表示用UIテキストに時間を表示する
		if ((int)seconds != (int)oldSeconds)
		{
			timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
		}
		oldSeconds = seconds;
		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if (totalTime <= 0f)
		{
			moji.text = "しゅ～りょ～";
			audioSource.PlayOneShot(finish1);
			//Debug.Log("制限時間終了");
			Invoke(nameof(DelayMethod), 5.5f);
			
		}
	}
	void DelayMethod()
	{
		SceneManager.LoadScene("OneCushion");
    }
}