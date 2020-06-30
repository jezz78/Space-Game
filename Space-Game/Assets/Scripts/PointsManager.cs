using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

    public static int points;
    public Text Points_text;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        Points_text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        AddPoints();
    }

    void AddPoints()
    {
        Points_text.text = points.ToString("F0");
    }
}
