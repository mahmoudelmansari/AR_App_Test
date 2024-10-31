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

    [SerializeField] Transform rightHandPosition;
    [SerializeField] Transform leftHandPosition;

    [SerializeField] Vector3 rightPathOffset;
    [SerializeField] Vector3 leftPathOffset;

    int[] rightPath = { 423, 266, 330, 347, 448, 265};
    int[] leftPath = { 203, 36, 101, 118, 111, 143};

    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();
        if(face.vertices.Length > 0)
        {
            chinPosition = face.vertices[chinIndex];
            facePosition = chinPosition - Vector3.up * 0.5f;
            MoveHandsToFace();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveHandsToFace()
    {
        rightHandPosition.DOMove(facePosition, 0.5f).OnComplete(() =>
        {
            FollowPath(rightHandPosition, GetPath(rightPath), rightPathOffset, rightPath);
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
                hand.position = Vector3.Lerp(initialPos, path[i].position + pathOffset, t);
                yield return null;
            }

        }
    }

    List<Vector3> GetPath(int[] indeces)
    {
        List<Vector3> path = new List<Vector3>();

        for (int i = 0; i < indeces.Length; i++)
        {
            path.Add(face.vertices[indeces[i]]);
        }

        return path;
    }

}
