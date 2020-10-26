using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoLayout : MonoBehaviour
{
    int order = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (order == 0 && this.transform.childCount == 0)
        {

        }

        switch (this.transform.childCount)
        {
            case 1:
                this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(500, 550);
                //this.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(462, 62, 0);
                this.transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(950, 500, 0);
                //this.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(950, 500, 0);
                break;

            case 2:
                this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 120);
                //this.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(93, -110, 0);
                this.transform.GetChild(1).GetComponent<RectTransform>().position = new Vector3(150, 100, 0);
                break;
            case 3:
                this.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 120);
                //this.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(859, -110, 0);
                this.transform.GetChild(2).GetComponent<RectTransform>().position = new Vector3(1700, 100, 0);
                break;

            default:
                break;
        }
    }
}
