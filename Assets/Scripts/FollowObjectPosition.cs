using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectPosition : MonoBehaviour
{
    [SerializeField] private Transform Follow;

    [SerializeField] private float positionYOffset = 1;
    void Start()
    {
        transform.position = new Vector3(Follow.position.x, Follow.position.y + positionYOffset, transform.position.z);

    }

    void Update()
    {
        transform.position = new Vector3(Follow.position.x, Follow.position.y + positionYOffset, transform.position.z);
    }
}
