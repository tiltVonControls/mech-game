using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class gunSelect : MonoBehaviour
{
    [SerializeField]
    private gunType gunTypeL;
    [SerializeField]
    private gunType gunTypeR;
    [SerializeField]
    private Transform gunParentL;
    [SerializeField]
    private Transform gunParentR;
    [SerializeField]
    private gunMian gunsL;
    [SerializeField]
    private gunMian gunsR;

    [Space]
    [Header("runtime filled")]
    public gunMian gunActiveL;
    public gunMian gunActiveR;

    private void Start()
    {
        Debug.Log("Start method called in gunSelect");

        if (gunsL == null)
        {
            Debug.LogError("gunsL list is empty or null");
            return;
        }
        if (gunsR == null)
        {
            Debug.LogError("gunsL list is empty or null");
            return;
        }


        Debug.Log("Gun found: " + gunsL.Name);
        gunActiveL = gunsL;
        gunsL.Spawn(gunParentL, this);
        Debug.Log("Gun found: " + gunsR.Name);
        gunActiveR = gunsR;
        gunsR.Spawn(gunParentR, this);
    }
}
