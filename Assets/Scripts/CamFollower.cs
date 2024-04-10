using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _target;

    private Vector3 _camPos = new Vector3(), _offset = new Vector3();

    private void Start()
    {
        _offset = transform.position;
    }

    private void LateUpdate()
    {
        _camPos = new Vector3(_target.position.x + _offset.x,
                              _target.position.y + _offset.y,
                              _target.position.z + _offset.z);

        transform.position = _camPos;
    }
}
