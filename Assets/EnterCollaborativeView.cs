using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;

public class EnterCollaborativeView : MonoBehaviour
{
    // Start is called before the first frame update
    public void Update()
    {
        bool triggerRight = OVRInput.Get(OVRInput.Button.Two);

        if (triggerRight)
        {
            Load();
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(1);
    }
}
