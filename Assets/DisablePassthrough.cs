using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePassthrough : MonoBehaviour
{
    public GameObject MRTK_Quest_OVRCameraRig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnPostRender()
    {
        MRTK_Quest_OVRCameraRig.GetComponent<OVRManager>().isInsightPassthroughEnabled = false; 
        Debug.Log("Passthrough disabled");
        //OVRManager.instance.isInsightPassthroughEnabled = false;
    }
}
