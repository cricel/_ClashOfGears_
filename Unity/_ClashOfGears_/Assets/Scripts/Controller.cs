using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    RaycastHit hit;
    GameObject firstObj;
    GameObject secondObj;

    public Transform[] initTrans;
    public float[] values;
    // Start is called before the first frame update
    void Start()
    {
        initTrans = new Transform[5];
        int i = 0;
        foreach (Transform child in transform.Find("/Parts"))
        {
            initTrans[i] = Instantiate(child);
            //Debug.Log(initTrans[i].localPosition);
            i = i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (firstObj == null)
                {
                    firstObj = hit.collider.gameObject;
                    Debug.Log(firstObj.name);
                    foreach (Transform child in transform.Find("Slot"))
                    {
                        if (child.name.Split(' ')[1] == firstObj.name.Split(' ')[0])
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    secondObj = hit.collider.gameObject;
                    try
                    {
                        if (secondObj.name.Split(' ')[1] == firstObj.name.Split(' ')[0] && secondObj.name.Split(' ')[0] == "Slot")
                        {
                            Debug.Log("match");
                            //firstObj.transform.position = secondObj.transform.position;
                            firstObj.transform.SetParent(transform.Find("Parts"));
                            firstObj.transform.localPosition = Vector3.zero;
                            firstObj.transform.localRotation = Quaternion.identity;
                        }
                    }
                    catch { Debug.Log("ignore this, but somthing wrong with the object name you click, it need to have two word with a space in it"); }

                    // Deactivate
                    firstObj = null;
                    secondObj = null;
                    foreach (Transform child in transform.Find("Slot"))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
        
    }

    public void ResetParts()
    {
        int i = 0;
        foreach (Transform child in transform.Find("Parts"))
        {
            Debug.Log(child.name);
            Debug.Log(initTrans[i].position);
            child.transform.SetParent(transform.Find("/Parts"));
            child.localPosition = initTrans[i].localPosition;
            child.localRotation = initTrans[i].localRotation;
            i = i + 1;
        }
    }
}
