using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gridLayout : MonoBehaviour
{
    public float width; // size
    public float height; // size
    public float btn; // size

    // Start is called before the first frame update
    void Start()
    {

        height = (Screen.height / 5)/5;

        if (height >= 0) { 
        Vector2 newSize = new Vector2(20, height);
        this.gameObject.GetComponent<GridLayoutGroup>().spacing = newSize;
    } }
}
