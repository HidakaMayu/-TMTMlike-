using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRestart : MonoBehaviour
{
    //リセット文字
    [SerializeField]
    Text reset;　　//タイトルへ戻るボタン
    public Text total;　　//合計文字
    public Text comment;
    public Text come; //余計な一言
    public Text highScoreText; //ハイスコア文字

    [SerializeField]
    private Text scorePoint; //結果合計

    //音楽
    public AudioClip sound1;　//三(卍^o^)卍ﾄﾞｩﾙﾙﾙﾙ
    public AudioClip sound2;  //ジャジャーン
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        StartCoroutine( Points() );
        StartCoroutine(Restat());
        StartCoroutine(CharaComment());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //左ボタン
        {
            //Debug.Log("Tap");
            SceneManager.LoadScene("Title");
        }
    }


    private IEnumerator Points()
    {
        yield return new WaitForSeconds( 3.0f );
        total.text = "合計ポイント";
        scorePoint.text = ScoreScript.score.ToString() + "point";
        audioSource.PlayOneShot(sound2);
    }
    private IEnumerator CharaComment()
    {
        yield return new WaitForSeconds(4.0f);
        if (ScoreScript.score <= 10000)
        {
            comment.text = "あなたのランク\nビギナー";
            come.text = "き、今日は釣れない日\nだったんだよ‼‼";
            
        }
        else if (ScoreScript.score <= 30000)
        {
            come.color = new Color(0.0f, 0.1f, 1.0f, 1.0f);
            comment.text = "あなたのランク\nアマチュア";
            come.text = "やったー！\nいっぱい釣れたね！！";
        }
        else if (ScoreScript.score <= 50000)
        {
            come.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            comment.text = "あなたのランク\nプロ";　
            come.text = "すっご～い！\n君は釣りが得意な\nフレンズなんだね！！";
        }
        else if (ScoreScript.score <= 100000)
        {
            come.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            comment.text = "あなたのランク\nレジェンド";
            come.text = "今日は大量だーーー‼‼‼‼！！";
        }
        else
        {
            comment.text = "あなたのランク\n神";
            come.text = "世界中の魚を\n釣りで支配しました";

            
        }
        highScoreText.text = "ハイスコア\n" + ScoreScript.highScore.ToString() + "point";
        PlayerPrefs.SetInt("HighScore", ScoreScript.highScore);
        PlayerPrefs.Save();
    }

    private IEnumerator Restat()
    {
        yield return new WaitForSeconds(7.0f);
        reset.text = "タップでタイトルへ戻る";
        if (Input.GetMouseButtonDown(0)) //左ボタン
        {
            SceneManager.LoadScene("Title");
        }
        
    }
    



        /*   if (Input.GetKeyDown("space"))
           {
               SceneManager.LoadScene("Title");
              // Debug.Log("ゲームをやり直す");
           }*/
        //３*連鎖数(連鎖数はx - 3　|| x　=　実際の連鎖個数)
        //text
        //TotalScore:だんだんと上から表示される形式
        //if score <= 100 素晴らしい！ 紙吹雪とか？
        //else if score <= 50|| score > 100 すごい！ 
        //else (_´Д｀)ﾉ~~ｵﾂｶﾚｰ

        //消すとき爆ぜるエフェクト
        //画面タッチした時可愛いエフェクト
        // https://tsubakit1.hateblo.jp/entry/2016/04/28/023104
        // https://www.google.com/search?q=%E7%94%BB%E9%9D%A2%E3%82%BF%E3%83%83%E3%83%81+%E3%82%A8%E3%83%95%E3%82%A7%E3%82%AF%E3%83%88+Unity&rlz=1C1JZAP_jaJP951JP951&oq=%E7%94%BB%E9%9D%A2%E3%82%BF%E3%83%83%E3%83%81%E3%80%80%E3%82%A8%E3%83%95%E3%82%A7%E3%82%AF%E3%83%88%E3%80%80Unity&aqs=chrome..69i57.16333j0j7&sourceid=chrome&ie=UTF-8

    
}
