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
    public GameObject bullet;

    public DamageConfig DamageConfig;
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

    public void DoShoot(bool isLeftGun)
    {
        if (Time.time > config.fireRate + lastShootTime)
        {
            lastShootTime += Time.time;
            Transform gunEndTransform = gunObject.transform.Find("gun_shoot");
            if (gunEndTransform == null)
            {
                Debug.LogError("Gun end transform not found!");
                return;
            }

            Vector3 shootDir = gunEndTransform.forward;
            shootDir.Normalize();

            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = gunEndTransform.position;
            newBullet.transform.rotation = gunEndTransform.rotation;
            newBullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.5f);
            Rigidbody rb = newBullet.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = shootDir * config.bulletSpeed;

            activeMonoBehaviour.StartCoroutine(DestroyBulletAfterTime(newBullet, 2.0f));

            if (isLeftGun)
            {
                ShootFromGun(gunObject.transform.Find("LeftGun"));
            }
            else
            {
                ShootFromGun(gunObject.transform.Find("RightGun"));
            }
        }
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

/*    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootFromGun(gunObject.transform.Find("LeftGun"));
        }
        if (Input.GetMouseButtonDown(1))
        {
            ShootFromGun(gunObject.transform.Find("RightGun"));
        }
    }*/

    private void ShootFromGun(Transform gunTransform)
    {
        ParticleSystem gunParticleSystem = gunTransform.GetComponentInChildren<ParticleSystem>();
        if (gunParticleSystem == null)
        {
            Debug.LogError("No ParticleSystem found in gunTransform!");
            return;
        }

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
                SpawnMesh(
                    gunParticleSystem.transform.position,
                    hit.point,
                    hit
                    ));
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out getDamage damageable))
                {
                    damageable.TakeDamage(DamageConfig.GetDamage());
                }
            }
        }
        else
        {
            SpawnMesh(
            gunParticleSystem.transform.position,
            gunParticleSystem.transform.position + (shootDir * gunTrailShoot.missDis),
            new RaycastHit()
                );
        }
    }

    private IEnumerator SpawnMesh(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        GameObject meshObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        meshObject.transform.position = startPoint;
        meshObject.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(startPoint, endPoint));
        meshObject.transform.LookAt(endPoint);

        yield return new WaitForSeconds(gunTrailShoot.duration);

        Destroy(meshObject);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out getDamage damageable))
            {
                damageable.TakeDamage(DamageConfig.GetDamage());
            }
        }
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
