using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public static int Points;
    Text PointText;
    // Start is called before the first frame update
    void Start()
    {
        Points = 0;
        PointText = GameObject.FindGameObjectWithTag("PointText").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateStats()
    {
            //PointText.text = "";
    }
}
