using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_Crosshair : MonoBehaviour
{
    static public float spread = 0;

    public const int PISTOL_SHOOTING_SPREAD = 20;
    public const int JUMP_SPREAD = 50;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 25;

    public GameObject crosshair;
    GameObject topPart;
    GameObject bottomPart;
    GameObject leftPart;
    GameObject rightPart;

    float initialPosition;

    void Start()
    {
        topPart = crosshair.transform.Find("Top_part").gameObject;
        bottomPart = crosshair.transform.Find("Bottom_part").gameObject;
        leftPart = crosshair.transform.Find("Left_part").gameObject;
        rightPart = crosshair.transform.Find("Right_part").gameObject;

        initialPosition = topPart.GetComponent<RectTransform>().localPosition.y;
    }
    void Update()
    {
        if(spread != 0)
        {
            topPart.GetComponent<RectTransform>().localPosition = new Vector3(0, initialPosition + spread, 0);
            bottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initialPosition + spread), 0);
            leftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(initialPosition + spread), 0, 0);
            rightPart.GetComponent<RectTransform>().localPosition = new Vector3(initialPosition + spread, 0, 0);
            spread -= 0.1f;
            if (spread < 0) spread = 0;
        }
    }

}
