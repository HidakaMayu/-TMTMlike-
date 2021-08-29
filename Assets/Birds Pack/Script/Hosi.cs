using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hosi : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider H, S, V;
    void Start()
    {
        Renderer coloring = this.GetComponent<Renderer>();
        coloring.material.color = new Color(Random.value, Random.value, Random.value, 1.0f); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        } //GameStartの下二つのcommentアウトを消した後、TitleのZ：13に設定すると出現。普通に気持ち悪いので没
    }
}
