using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float cameraPosLerpSpeed;
    [SerializeField] private float cameraRotLerpSpeed;

    private Transform cameraPos;

    private void Start()
    {
        cameraPos = Zomby.instance.CameraPos;
    }

    private void Update()
    {
        var pos = Vector3.Lerp(transform.position, cameraPos.position, cameraPosLerpSpeed * Time.deltaTime);
        var rot = Quaternion.Lerp(transform.rotation, cameraPos.rotation, cameraRotLerpSpeed * Time.deltaTime);
        transform.position = pos;
        transform.rotation = rot;
    }
}
