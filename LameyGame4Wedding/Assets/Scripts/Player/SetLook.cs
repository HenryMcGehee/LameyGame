using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLook : MonoBehaviour
{
    [SerializeField] GameObject Lane1;
    [SerializeField] GameObject Jamie1;
    [SerializeField] GameObject Jamie2;
    [SerializeField] Renderer JamieRender;
    [SerializeField] Renderer catRender;
    [SerializeField] Renderer catear1Render;
    [SerializeField] Renderer catear2Render;
    [SerializeField] Renderer catTailRender;
    [SerializeField] Material JamieMat;
    [SerializeField] Material squirlMat;
    [SerializeField] Material bertMat;
    public bool jamie;
    public void UpdateLook()
    {
        if(jamie)
        {
            Lane1.SetActive(false);
            Jamie1.SetActive(true);
            Jamie2.SetActive(true);
            JamieRender.material = JamieMat;
            catRender.material = squirlMat;
            catear1Render.material = squirlMat;
            catear2Render.material = squirlMat;
            catTailRender.material = squirlMat;
        }
        else{
            Lane1.SetActive(true);
            Jamie1.SetActive(false);
            Jamie2.SetActive(false);
            catRender.material = bertMat;
            catear1Render.material = bertMat;
            catear2Render.material = bertMat;
            catTailRender.material = bertMat;
        }
    }
}
