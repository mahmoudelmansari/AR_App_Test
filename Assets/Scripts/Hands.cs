using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Hands : MonoBehaviour
{
    ARFace face;

    List<Vector3> rightPath = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();

        if(face.vertices.Length > 0 )
        {
            rightPath.Add( face.vertices[423] );
            rightPath.Add( face.vertices[266] );
            rightPath.Add( face.vertices[330] );
            rightPath.Add( face.vertices[449] );
            rightPath.Add( face.vertices[261] );
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
