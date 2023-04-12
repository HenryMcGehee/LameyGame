using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLook : MonoBehaviour
{
    [SerializeField] GameObject Lane1;
    [SerializeField] GameObject Jamie1;
    [SerializeField] GameObject Jamie2;
    [SerializeField] Renderer JamieRender;
    [SerializeField] Material JamieMat;
    public bool jamie;
    public void UpdateLook()
    {
        if(jamie)
        {
            Lane1.SetActive(false);
            Jamie1.SetActive(true);
            Jamie2.SetActive(true);
            JamieRender.material = JamieMat;
        }
        else{
            Lane1.SetActive(true);
            Jamie1.SetActive(false);
            Jamie2.SetActive(false);
        }
    }
}
