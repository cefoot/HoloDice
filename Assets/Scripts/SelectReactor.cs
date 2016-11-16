using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectReactor : MonoBehaviour
{

    private Rigidbody _rigi;

    // Use this for initialization
    void Start()
    {
        _rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect()
    {
        _rigi.useGravity = !_rigi.useGravity;
        if (!_rigi.useGravity)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0F, transform.position.z);
        }
    }
}
