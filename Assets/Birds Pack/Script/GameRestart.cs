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

    [SerializeField]
    private Text scorePoint; //結果合計
    // Start is called before the first frame update

    //音楽
    public AudioClip sound1;　//三(卍^o^)卍ﾄﾞｩﾙﾙﾙﾙ
    public AudioClip sound2;  //ジャジャーン
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }

    // Update is called once per frame
    void Update()
    {
        
        Invoke(nameof(Restat), 8.5f);
        Invoke(nameof(Points), 3.0f);
        Invoke(nameof(CharaComment), 4.0f);
        
    }

    void Points()
    {
        total.text = "合計ポイント";
        scorePoint.text = ScoreScript.score.ToString() + "point";
        audioSource.PlayOneShot(sound2);
    }
    void CharaComment()
    {
        
        if (ScoreScript.score <= 3000)
        {
            comment.text = "あなたのランク：ビギナー";
            come.text = "人類は滅亡した";
            
        }
        else if (ScoreScript.score <= 7000)
        {
            come.color = new Color(0.0f, 0.1f, 1.0f, 1.0f);
            comment.text = "あなたのランク：アマチュア";
            come.text = "大変よくできました！！";
        }
        else if (ScoreScript.score <= 15000)
        {
            come.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            comment.text = "あなたのランク：プロ";　
            come.text = "すっご～い！\n君はパズルゲームが得意な\nフレンズなんだね！！";
        }
        else
        {
            comment.text = "あなたのランク：レジェンド";
            come.text = "いや、やばすぎwwwwwwww\nwwwwwwwwwwwwww";

            
        }
    }

    void Restat()
    {
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
