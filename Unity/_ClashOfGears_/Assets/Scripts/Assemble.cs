using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private bool posChange = false;
    private Vector3 startPos;
    private GameObject slotObj;

    private Transform orgTrans;
    // Start is called before the first frame update

    void Start()
    {
        orgTrans = this.transform;
    }

    private void OnMouseDown()
    {
        //mZCoord = Camera.main.WorldToScreenPoint(this.gameObject.transform.position).z;
        //mOffset = gameObject.transform.position - GetMouseWorldPos();
        Debug.Log("The " + this.gameObject.name + " was clicked");
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        if (posChange == true)
        {
            this.gameObject.transform.position = slotObj.transform.position;
            this.gameObject.transform.SetParent(slotObj.transform.parent);
        }
        else
        {
            this.gameObject.transform.SetParent(null);
            this.gameObject.transform.position = orgTrans.position;

        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (this.gameObject.name.Split('_')[0] == collision.gameObject.name.Split('_')[0])
        {
            posChange = true;
            slotObj = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        posChange = false;
    }
}
