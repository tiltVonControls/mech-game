using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body", menuName = "mechs/Body", order = 0)]
public class bodyMain : ScriptableObject
{
    public bodyType type;
    public string Name;
    public GameObject bodyPrefab;
    public Vector3 spawn;
    public Vector3 spawnRotation;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject bodyObject;

    public List<bodyMain> Type { get; internal set; }
    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        if (bodyPrefab == null)
        {
            Debug.LogError("bodyPrefab is not assigned!");
            return;
        }
        bodyObject = Instantiate(bodyPrefab);
        if (bodyObject == null)
        {
            Debug.LogError("Failed to instantiate bodyPrefab!");
            return;
        }
        bodyObject.transform.SetParent(Parent, false);
        bodyObject.transform.localPosition = spawn;
        bodyObject.transform.localRotation = Quaternion.Euler(spawnRotation);
    }

    // Other existing code



}
