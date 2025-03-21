using UnityEngine;

[CreateAssetMenu(fileName = "trail config", menuName = "guns/trail", order = 3)]
public class gunTrailShoot : ScriptableObject 
{
    public Material material;
    public AnimationCurve curve;
    public float duration = 1f;
    public float minVertexDis = 0f;
    public Gradient Gradient;

    public float missDis = 100f;
    public float simSpeed = 100f;
}
