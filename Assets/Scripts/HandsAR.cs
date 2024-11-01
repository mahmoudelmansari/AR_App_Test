using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.ARFoundation;

public class HandsAR : MonoBehaviour
{
    ARFaceManager faceManager;
    ARFace face;
    FaceData faceData;

    const int chinIndex = 152;
    Vector3 chinPosition;
    Vector3 facePosition;

    [SerializeField] Transform rightHandPosition;
    [SerializeField] Transform leftHandPosition;

    [SerializeField] MultiPositionConstraint rightConstraint;
    [SerializeField] MultiPositionConstraint leftConstraint;

    [SerializeField] Vector3 rightPathOffset;
    [SerializeField] Vector3 leftPathOffset;

    int[] rightIndices = { 423, 266, 330, 347, 448, 265};
    int[] leftIndices = { 203, 36, 101, 118, 111, 143};

    List<Vector3> rightPath = new List<Vector3>();
    List<Vector3> leftPath = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();
        faceManager = FindAnyObjectByType<ARFaceManager>();
        faceData = FindAnyObjectByType<FaceData>();

        faceManager.facesChanged += GetPath;
        rightHandPosition = faceData.rightHandPosition;
        leftHandPosition = faceData.leftHandPosition;

        var rightData = rightConstraint.data.sourceObjects;
        rightData.SetTransform(0, rightHandPosition);
        rightConstraint.data.sourceObjects = rightData;

        var leftData = leftConstraint.data.sourceObjects;
        leftData.SetTransform(0, leftHandPosition);
        leftConstraint.data.sourceObjects = leftData;

        if (face.vertices.Length > 0)
        {
            chinPosition = face.vertices[chinIndex];
            facePosition = chinPosition - Vector3.up * 0.5f;
            MoveHandsToFace();
        }

    }

    void MoveHandsToFace()
    {
        rightHandPosition.DOMove(facePosition, 0.5f).OnComplete(() =>
        {
            StartCoroutine(FollowPath(rightHandPosition, rightPath, rightPathOffset));
        });

        leftHandPosition.DOMove(facePosition, 0.5f).OnComplete(() =>
        {
            StartCoroutine(FollowPath(leftHandPosition, leftPath, leftPathOffset));

        });
    }

    IEnumerator FollowPath(Transform hand, List<Vector3> path, Vector3 pathOffset)
    {
        while (true)
        { 
            float t;
            float duration = 0.8f;
            
            for (int i = 0; i < path.Count; i++)
            {
                t = 0;
                Vector3 initialPos = hand.position;

                while (t < 1)
                {
                    t += Time.deltaTime / duration;
                    hand.position = Vector3.Lerp(initialPos, path[i] + pathOffset, t);
                    yield return null;
                }

                t = 0;
                duration = 0.2f;
                Vector3 handPos = hand.position;

                while (t < 1)
                {
                    t += Time.deltaTime / duration;
                    hand.position = Vector3.Lerp(handPos, facePosition, t);
                    yield return null;
                }
            }
        }
    }

    void GetPath(ARFacesChangedEventArgs eventArgs)
    {
        for (int i = 0; i < rightIndices.Length; i++)
        {
            rightPath.Add(face.vertices[rightIndices[i]]);
            leftPath.Add(face.vertices[leftIndices[i]]);
        }
    }

}
