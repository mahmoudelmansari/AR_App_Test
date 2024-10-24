using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Hands : MonoBehaviour
{
    ARFace face;

    [SerializeField] int[] rightPathIndices;

    [SerializeField] Transform faceModel;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();

        if(face.vertices.Length > 0 )
        {
                
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = faceModel.position + offset; 
    }
}
