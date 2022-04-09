using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectPosition : MonoBehaviour
{
    [SerializeField] private Transform Follow;

    void Start()
    {
        transform.position = new Vector3(Follow.position.x, Follow.position.y, transform.position.z);

    }

    void Update()
    {
        transform.position = new Vector3(Follow.position.x, Follow.position.y, transform.position.z);
    }
}
