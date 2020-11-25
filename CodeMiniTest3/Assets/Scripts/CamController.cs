using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject playerGO;

    Vector3 posOffset = new Vector3(0, 3.0f, -8.0f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerGO.transform.position + posOffset, 0.1f);
    }
}
