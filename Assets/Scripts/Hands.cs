using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Hands : MonoBehaviour
{

    [SerializeField] Transform facePosition;
    [SerializeField] Transform rightHandPosition;
    [SerializeField] Transform leftHandPosition;

    [SerializeField] List<Transform> rightPath = new List<Transform>();
    [SerializeField] List<Transform> leftPath = new List<Transform>();
    [SerializeField] Vector3 rightPathOffset;
    [SerializeField] Vector3 leftPathOffset;

    // Start is called before the first frame update
    void Start()
    {
        MoveHandsToFace();
    }


    void MoveHandsToFace()
    {
        rightHandPosition.DOMove(facePosition.position, 0.5f).OnComplete(() =>
        {
            StartCoroutine(FollowPath(rightHandPosition,rightPath, rightPathOffset));
        });

        leftHandPosition.DOMove(facePosition.position, 0.5f).OnComplete(() =>
        {
            StartCoroutine(FollowPath(leftHandPosition, leftPath, leftPathOffset));
        });
    }

    IEnumerator FollowPath(Transform hand, List<Transform> path, Vector3 pathOffset)
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

        OnAnimationEnd();
    }

    void OnAnimationEnd()
    {
        rightHandPosition.DOMove(facePosition.position, 0.25f);
        leftHandPosition.DOMove(facePosition.position, 0.25f);

        StartCoroutine(FollowPath(rightHandPosition, rightPath, rightPathOffset));
        StartCoroutine(FollowPath(leftHandPosition, leftPath, leftPathOffset));
    }

}
