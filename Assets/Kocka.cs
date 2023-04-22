using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Kocka : MonoBehaviour
{
    [SerializeField] private bool collided;
    [SerializeField]
    private int collCount;

    private void Awake()
    {
        collided = false;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2, Quaternion.identity, 1<<10);
        if (colliders.Length.Equals(1))
            collided = false;
        else
            collided = true;
        collCount = colliders.Length;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
    }
}
