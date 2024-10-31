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
        while(true)
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
                    hand.position = Vector3.Lerp(initialPos, path[i].position + pathOffset, t);
                    yield return null;
                }
            }

            t = 0;
            duration = 0.2f;
            Vector3 facPos = facePosition.position;
            Vector3 handPos = hand.position;
            
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                hand.position = Vector3.Lerp(handPos, facPos, t);
                yield return null;
            }

        }
    }
}
