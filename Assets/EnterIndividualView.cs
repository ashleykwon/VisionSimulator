using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;

public class EnterIndividualView : MonoBehaviour
{
    // Start is called before the first frame update
    public void Update()
    {
        bool triggerRight = OVRInput.Get(OVRInput.Button.One);

        if (triggerRight)
        {
            Load();
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(2);
    }
}
