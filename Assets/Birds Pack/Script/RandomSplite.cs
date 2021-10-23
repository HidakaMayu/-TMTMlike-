using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSplite : MonoBehaviour
{
    public Sprite[] sp;
    IEnumerator ShowImage()
    {
        var wait = new WaitForSeconds(1f);//1秒ごと
        var image = GetComponent<Image>();//画像
        while (true)//無限ループ
        {
            yield return wait;//1秒
            image.sprite = sp[Random.Range(0, sp.Length)];//Randomでもってくる
        }
    }
}
