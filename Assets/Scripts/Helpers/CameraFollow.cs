using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private static bool cameraExistes;

    // Start is called before the first frame update
    void Start()
    {
        if (!cameraExistes)
        {
            cameraExistes = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
