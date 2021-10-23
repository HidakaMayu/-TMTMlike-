using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Text highScoreTitle;
    
    //public GameObject Star;
    void Start()
    {
        
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        int startHi = PlayerPrefs.GetInt("HighScore");
        highScoreTitle.text = "現在のハイスコア\n" + startHi.ToString() + "point";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //左ボタン
        {
            SceneManager.LoadScene("Help");
            gameObject.GetComponent<GraphicRaycaster>().enabled = false;
        }
        /* if (Input.GetKeyDown("space"))
         {
             SceneManager.LoadScene("SampleScene");
             Debug.Log("スペースキーが押された");
         }*/

        //float x = Random.Range(-3.0f, 3.0f);
        //Instantiate(Star, new Vector3(x, 6.0f, 2.0f), Quaternion.identity);
    }
}