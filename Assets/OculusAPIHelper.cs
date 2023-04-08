using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class OculusAPIHelper : MonoBehaviour
{

    void Start()
    {
		Oculus.Platform.Core.Initialize("9279204032120254");

	}
}
