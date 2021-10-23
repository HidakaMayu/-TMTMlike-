using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class BirdSclipt : MonoBehaviour
{
    // 鳥のプレハブを格納する配列
    public GameObject[] birdPrefabs;

    //消したとき音が鳴る
    public AudioClip water; //バシャーん
    AudioSource audioSource;


    // 連鎖判定用の距離
    const float BirdDistance = 1.4f;

    //最低連鎖数
    const int MinChain = 3;

    // クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();

    //釣った時に変わる用の画像
    public GameObject hito;
    public GameObject fisher;


    //連鎖
    private int[] stickBonus =
        { 0, 0, 0, 0, 50, 100, 150, 300, 500, 1000, 1500, 3000, 5000, 8000, 10000, 15000, 30000, 50000, 80000};

    //スコアポイント
    public GameObject scoreGUI;
    private int point = 0;
    public Text myText;


    //ボタン
    //public GameObject exchangeButton;


    void Start()
    {
        hito.SetActive(true);
        TouchManager.Began += (info) =>
        {
            // クリック地点でヒットしているオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                Vector2.zero);
            if (hit.collider)
            {
                GameObject hitObj = hit.collider.gameObject;
                // ヒットしたオブジェクトのtagを判別し初期化
                if (hitObj.tag == "Bird")
                {
                    firstBird = hitObj;
                    lastBird = hitObj;
                    currentName = hitObj.name;
                    removableBirdList = new List<GameObject>();
                    PushToBirdList(hitObj);
                }
            }
        };
        TouchManager.Moved += (info) =>
        {
            if (!firstBird)
            {
                return;
            }
            // クリック地点でヒットしているオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                Vector2.zero);
            if (hit.collider)
            {
                GameObject hitObj = hit.collider.gameObject;
                // ヒットしたオブジェクトのtagが鳥、尚且名前が一緒、
                // 尚且最後にhitしたオブジェクトと違う、尚且リストに格納されていない
                if (hitObj.tag == "Bird" && hitObj.name == currentName
                && hitObj != lastBird && 0 > removableBirdList.IndexOf(hitObj))
                {

                    float distance = Vector2.Distance(hitObj.transform.position,
                        lastBird.transform.position);
                    if(distance > BirdDistance)
                    {
                        return;
                    }

                    lastBird = hitObj;
                    PushToBirdList(hitObj);
                }
            }
        };
        TouchManager.Ended += (info) =>
        {
            int count = removableBirdList.Count;
            removableBirdList.ToList().ForEach(obj => Destroy(obj));

            if (count >= MinChain)
            {
                StartCoroutine(ShowImageSecond(2f));
                
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(water);
                if(count <= 18)
                {
                    scoreGUI.SendMessage("AddPoint", point = count * 100 + stickBonus[count]);
                }
                else
                {
                    scoreGUI.SendMessage("AddPoint", point = count * 100 + 100000);
                }
                
                StartCoroutine(DropBirds(count));
            }
            
            


            foreach (GameObject obj in removableBirdList)
            {
                ChangeColor(obj, 1.0f);
            }
            removableBirdList = new List<GameObject>();
            firstBird = null;
            lastBird = null;
        };
        StartCoroutine(DropBirds(50));
    }
    

    private void PushToBirdList(GameObject obj)
    {
        removableBirdList.Add(obj);
        ChangeColor(obj, 0.5f);
    }
    private void ChangeColor(GameObject obj, float transparency)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            transparency);
    }

    IEnumerator DropBirds(int count)
    {
        /*if (count == 50)
        {
            StartCoroutine("RestrictPush");
        }*/


            for (int i = 0; i < count; i++)
            {
                // ランダムで出現位置を作成
                Vector2 pos = new Vector2(Random.Range(-4.20f, 4.20f), 8.16f);
                // ランダムで鳥を出現させてIDを格納
                int id = Random.Range(0, birdPrefabs.Length);
                // 鳥を発生させる
                GameObject bird = (GameObject)Instantiate(birdPrefabs[id],
                    pos,
                    Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
                // 作成した鳥の名前を変更します
                bird.name = "Bird" + id;
                // 0.05秒待って次の処理へ
                yield return new WaitForSeconds(0.05f);
            }
        
    }
    IEnumerator ShowImageSecond(float second)
    {
        fisher.SetActive(true);
        hito.SetActive(false);
        yield return new WaitForSeconds(second); 
        fisher.SetActive(false);
        hito.SetActive(true);
    }


    /* IEnumerator RestrictPush()
     {
         exchangeButton.GetComponent<Button>().interactable = false;
         yield return new WaitForSeconds(5.0f);
         exchangeButton.GetComponent<Button>().interactable = true;
     }*/
}
