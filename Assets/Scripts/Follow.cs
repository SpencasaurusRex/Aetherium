using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public GameObject target;

    Vector3 targetPosition;

    private void Start()
    {
        targetPosition.z = transform.position.z;
    }

    void LateUpdate ()
    {
        targetPosition.x = target.transform.position.x;
        targetPosition.y = target.transform.position.y;
        transform.position = targetPosition;
    }
}
