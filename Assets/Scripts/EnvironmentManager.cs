using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject rampPrefab;
    public GameObject wallPrefab;
    public GameObject bumpPrefab;

    public int numberOfRamps = 2;
    public int numberOfWalls = 4;
    public int numberOfBumps = 3;

    public Vector3 areaSize = new Vector3(20, 5, 20);

    void Start()
    {
        GenerateEnvironment();
    }

    void GenerateEnvironment()
    {
        //  Ramps
        for (int i = 0; i < numberOfRamps; i++)
        {
            Vector3 position = GetRandomPosition();
            GameObject ramp = Instantiate(rampPrefab, position, Quaternion.Euler(0, Random.Range(0, 360), 20));
            ramp.transform.localScale = new Vector3(5, 1, 10);
        }

        //  Walls
        for (int i = 0; i < numberOfWalls; i++)
        {
            Vector3 position = GetRandomPosition();
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            wall.transform.localScale = new Vector3(10, 5, 1);
        }

        // Bumps
        for (int i = 0; i < numberOfBumps; i++)
        {
            Vector3 position = GetRandomPosition();
            GameObject bump = Instantiate(bumpPrefab, position, Quaternion.identity);
            bump.transform.localScale = new Vector3(2, 0.5f, 2);
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            0,
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );
    }
}
