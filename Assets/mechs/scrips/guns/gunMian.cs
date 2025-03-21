using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "guns", menuName = "guns/guns", order = 0)]
public class gunMian : ScriptableObject
{
    public gunType type;
    public string Name;
    public GameObject gunPrefab;
    public Vector3 spawn;
    public Vector3 spawnRotation;

    public shootConfig config;
    public gunTrailShoot gunTrailShoot;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject gunObject;
    private float lastShootTime;
    private ParticleSystem gunParticleSystem;
    private ObjectPool<TrailRenderer> renderersTrails;

    public List<gunMian> Type { get; internal set; }

    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = 0;
        renderersTrails = new ObjectPool<TrailRenderer>(DoTrail);

        if (gunPrefab == null)
        {
            Debug.LogError("gunPrefab is not assigned!");
            return;
        }

        gunObject = Instantiate(gunPrefab);
        if (gunObject == null)
        {
            Debug.LogError("Failed to instantiate gunPrefab!");
            return;
        }

        gunObject.transform.SetParent(Parent, false);
        gunObject.transform.localPosition = spawn;
        gunObject.transform.localRotation = Quaternion.Euler(spawnRotation);

        gunParticleSystem = gunObject.GetComponentInChildren<ParticleSystem>();
        if (gunParticleSystem == null)
        {
            Debug.LogError("No ParticleSystem found in gunPrefab!");
        }
    }

    public void DoShoot()
    {
        if (Time.time > config.fireRate + lastShootTime)
        {
            lastShootTime += Time.time;
            gunParticleSystem.Play();
            Vector3 shootDir = gunParticleSystem.transform.forward;
            shootDir.Normalize();

            if (Physics.Raycast(
                gunParticleSystem.transform.position,
                shootDir,
                out RaycastHit hit,
                float.MaxValue,
                config.hitMask
                ))
            {
                activeMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        gunParticleSystem.transform.position,
                        hit.point,
                        hit
                        ));
            }
            else
            {
                PlayTrail(
                gunParticleSystem.transform.position,
                gunParticleSystem.transform.position + (shootDir * gunTrailShoot.missDis),
                new RaycastHit()
                    );
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        TrailRenderer inst = renderersTrails.Get();
        inst.gameObject.SetActive(true);
        inst.transform.position = startPoint;
        yield return null;
        inst.emitting = true;
        float dist = Vector3.Distance(endPoint, startPoint);
        float remainingDis = dist;
        while (remainingDis > 0)
        {
            inst.transform.position = Vector3.Lerp(
                startPoint, endPoint, Mathf.Clamp01(1 - (remainingDis / dist))
                );
            remainingDis -= gunTrailShoot.simSpeed * Time.deltaTime;
            yield return null;
        }
        inst.transform.position = endPoint;

        yield return new WaitForSeconds(gunTrailShoot.duration);
        yield return null;
        inst.emitting = false;
        inst.gameObject.SetActive(false);
        renderersTrails.Release(inst);
    }

    private TrailRenderer DoTrail()
    {
        GameObject inst = new GameObject("Bullet Trail");
        TrailRenderer trail = inst.AddComponent<TrailRenderer>();
        trail.colorGradient = gunTrailShoot.Gradient;
        trail.widthCurve = gunTrailShoot.curve;
        trail.material = gunTrailShoot.material;
        trail.time = gunTrailShoot.duration;
        trail.minVertexDistance = gunTrailShoot.minVertexDis;
        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return trail;
    }
}
