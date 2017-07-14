using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DynamicScale : MonoBehaviour
{
    private Vector3 lastPosition;

	void Start ()
	{
	    lastPosition = transform.position;
	}
	
	
	void LateUpdate ()
	{
	    Vector3 delta = transform.position - lastPosition;
	    transform.localRotation = Quaternion.LookRotation(delta + Vector3.forward * 0.001f);
	    float l = 1f + delta.magnitude;
	    float wh = Mathf.Sqrt(1f / 1);
        transform.localScale = new Vector3(wh,wh,l);
	    lastPosition = transform.position;
	}
}
