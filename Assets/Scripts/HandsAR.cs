using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class HandsAR : MonoBehaviour
{
    ARFaceManager faceManager;
    ARFace face;

    const int chinIndex = 152;
    Vector3 chinPosition;
    Vector3 facePosition;

    [SerializeField] Transform rightHandPosition;
    [SerializeField] Transform leftHandPosition;

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
        faceManager.facesChanged += GetPath;
        if(face.vertices.Length > 0)
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
        leftHandPosition.DOMove(facePosition, 0.5f);
    }

    IEnumerator FollowPath(Transform hand, List<Vector3> path, Vector3 pathOffset)
    {
        for (int i = 0; i < path.Count; i++)
        {
            float t = 0;
            float duration = 0.8f;
            Vector3 initialPos = hand.position;

            while (t < 1)
            {
                t += Time.deltaTime / duration;
                Debug.Log(t);
                hand.position = Vector3.Lerp(initialPos, path[i] + pathOffset, t);
                yield return null;
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
