using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    public GameObject Star;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //左ボタン
        {
            SceneManager.LoadScene("Help");
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