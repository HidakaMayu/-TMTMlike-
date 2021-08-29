using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdSclipt : MonoBehaviour
{
    // 鳥のプレハブを格納する配列
    public GameObject[] birdPrefabs;

    //消したとき音が鳴る
    //public AudioClip birdSE;


    // 連鎖判定用の距離
    const float BirdDistance = 1.6f;

    //最低連鎖数
    const int MinChain = 3;

    // クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();
    

    //連鎖
    public GameObject lineObj;
    List<GameObject> lineBirdList = new List<GameObject>();

    //スコアポイント
    public GameObject scoreGUI;
    private int point = 0; 


    //ボタン
    //public GameObject exchangeButton;


    // Start is called before the first frame update
    void Start()
    {
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
                    PushToLineList(hitObj, lastBird);

                    lastBird = hitObj;
                    PushToBirdList(hitObj);
                }
            }
        };
        TouchManager.Ended += (info) =>
        {
            int count = removableBirdList.Count;

            if (count >= MinChain)
            {
                //リストに格納されている鳥を消す
                foreach (GameObject obj in removableBirdList)
                {
                    Destroy(obj);
                    //AudioSource.PlayClipAtPoint(birdSE, transform.position);　//鳥削除音鳴らす
                }

                //ラインを引き終わった後消す
                foreach (GameObject obj in lineBirdList)
                {
                    Destroy(obj);
                }
                scoreGUI.SendMessage("AddPoint", point = count * 100);
            }
            StartCoroutine(DropBirds(count));
            

            foreach (GameObject obj in removableBirdList)
            {
                ChangeColor(obj, 1.0f);
            }
            foreach(GameObject obj in lineBirdList)
            {
                Destroy(obj);
            }
            removableBirdList = new List<GameObject>();
            firstBird = null;
            lastBird = null;
        };
        StartCoroutine(DropBirds(50));
    }
    private void PushToLineList(GameObject lastObj, GameObject hitObj)
    {
        GameObject line = new GameObject();
        line.AddComponent<LineRenderer>();
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        //hutosa
        renderer.startWidth = 0.2f;
        renderer.endWidth = 0.2f;
        //tyoutennkazu
        renderer.positionCount = 2;
        //tyoutennsettei
        renderer.SetPosition(0, new Vector3(lastObj.transform.position.x,
            lastObj.transform.position.y, -1.0f));
        renderer.SetPosition(1, new Vector3(hitObj.transform.position.x,
            hitObj.transform.position.y, -1.0f));
        lineBirdList.Add(line);
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

   /* IEnumerator RestrictPush()
    {
        exchangeButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(5.0f);
        exchangeButton.GetComponent<Button>().interactable = true;
    }*/
}
