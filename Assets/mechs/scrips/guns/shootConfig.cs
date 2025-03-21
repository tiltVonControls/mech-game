using UnityEngine;

[CreateAssetMenu(fileName = "shoot config", menuName = "guns/Shoot config" , order = 2)]

public class shootConfig : ScriptableObject
{
    public LayerMask hitMask;
    public float fireRate = 1.0f;
}
