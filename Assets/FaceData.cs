using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceData : MonoBehaviour
{
    [field: SerializeField] public Transform rightHandPosition {  get; private set; }
    [field: SerializeField] public Transform leftHandPosition { get; private set; }
}

