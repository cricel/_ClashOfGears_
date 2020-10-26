using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    public Transform center;
    public float degreePerSec = -70.0f;
    private Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        v = transform.position - center.position;
    }

    // Update is called once per frame
    void Update()
    {
        v = Quaternion.AngleAxis(degreePerSec * Time.deltaTime, Vector3.down) * v;
        transform.position = center.position + v;
    }
}
