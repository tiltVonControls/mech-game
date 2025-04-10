using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class bodySelect : MonoBehaviour
{

    [SerializeField]
    private bodyType bodyType;
    [SerializeField]
    private Transform bodyParent;
    [SerializeField]
    private List<bodyMain> bodys;

    [Space]
    [Header("runtime filled")]
    public bodyMain bodyActive;

    private void Start()
    {
        Debug.Log("Start method called in bodySelect");
        if (bodys == null || bodys.Count == 0)
        {
            Debug.LogError("bodys list is empty or null");
            return;
        }
        bodyActive = bodys.Find(b => b.type == bodyType);
        if (bodyActive != null)
        {
            bodyActive.Spawn(bodyParent, this);
            Debug.Log("Body found: " + bodyActive.Name);
        }
        else
        {
            Debug.LogError("No body found for bodyType");
        }
    }
}
