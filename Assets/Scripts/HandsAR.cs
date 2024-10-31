using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class HandsAR : MonoBehaviour
{
    ARFace face;

    const int chinIndex = 152;
    Vector3 chinPosition;
    Vector3 facePosition;

    [SerializeField] Transform rightHandInitialPosition;
    [SerializeField] Transform leftHandInitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();
        if(face.vertices.Length > 0)
        {
            chinPosition = face.vertices[chinIndex];
            facePosition = chinPosition - Vector3.up * 1.5f;
            MoveHandsToFace();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveHandsToFace()
    {
        rightHandInitialPosition.DOMove(facePosition, 0.5f);
        leftHandInitialPosition.DOMove(facePosition, 0.5f);
    }
}
