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
    private List<gunMian> guns;

    [Space]
    [Header("runtime filled")]
    public gunMian gunActiveL;
    public gunMian gunActiveR;

    private void Start()
    {
        Debug.Log("Start method called in gunSelect");

        if (guns == null || guns.Count == 0)
        {
            Debug.LogError("guns list is empty or null");
            return;
        }

        gunActiveL = guns.Find(g => g.type == gunTypeL);
        if (gunActiveL != null)
        {
            gunActiveL.Spawn(gunParentL, this);
            Debug.Log("Gun found: " + gunActiveL.Name);
        }
        else
        {
            Debug.LogError("No gun found for gunTypeL");
        }

        gunActiveR = guns.Find(g => g.type == gunTypeR);
        if (gunActiveR != null)
        {
            gunActiveR.Spawn(gunParentR, this);
            Debug.Log("Gun found: " + gunActiveR.Name);
        }
        else
        {
            Debug.LogError("No gun found for gunTypeR");
        }
    }
}
