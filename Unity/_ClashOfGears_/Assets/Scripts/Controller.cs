using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    RaycastHit hit;
    GameObject firstObj;
    GameObject secondObj;
    
    public GameObject mainCam;

    public Transform[] initTrans;
    public float[] values;
    private bool testDriveCheck = false;

    public float smoothSpeed = 0.125f;
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
        //initCam.transform.position = mainCam.transform.position;
        //initCam.transform.rotation = mainCam.transform.rotation;
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

        if (testDriveCheck)
        {
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime, 0f);
            Vector3 desiredPosition = transform.position + offsetPos;
            //Vector3 desiredRotation = transform.rotation + offsetPos;
            //Vector3 smoothedPosition = Vector3.Lerp(mainCam.transform.position, desiredPosition, smoothSpeed);
            mainCam.transform.position = desiredPosition;
            mainCam.transform.rotation = Quaternion.Euler(transform.eulerAngles.x + offsetRot.x, 
                                                            transform.eulerAngles.y + offsetRot.y, 
                                                            transform.eulerAngles.z + offsetRot.z);
            Debug.Log(transform.eulerAngles.x + offsetRot.x);
            //mainCam.transform.Rotate(transform.eulerAngles.x + offsetRot.x, transform.eulerAngles.y + offsetRot.y, transform.eulerAngles.z + offsetRot.z);
        }   
    }

    //void FixedUpdate()
    //{
        
    //}

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

    public void testDrive()
    {
        testDriveCheck = !testDriveCheck;
        Debug.Log("testDriveCheck " + testDriveCheck);
        if (testDriveCheck == false)
        {
            mainCam.transform.position = new Vector3(-18.1f, 4.4f, 7.9f);
            mainCam.transform.rotation = new Quaternion(0.0f, 1.0f, -0.1f, 0.2f);
            transform.position = new Vector3(-18.1f, 1.3f, 0.3f);
            //transform.rotation = new Quaternion(0.0f, 0.7f, 0.1f, 0.0f);
            //Debug.Log(tempCam.position);
            //Debug.Log(tempCam.rotation);
        }
    }
}
