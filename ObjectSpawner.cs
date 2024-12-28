using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnDelay = 2.5f;
    public float objectSpeed = 4f;
    public float heightAboveGround = 8f;
    public float spawnAreaSize = 12f;

    private void BeginSpawning()
    {
        StartCoroutine(GenerateObjects());
    }

    private IEnumerator GenerateObjects()
    {
        while (true)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-spawnAreaSize, spawnAreaSize), heightAboveGround, Random.Range(-spawnAreaSize, spawnAreaSize));
            GameObject newObject = Instantiate(objectPrefab, spawnPoint, Quaternion.identity);
            StartCoroutine(MoveObjectTowardsTarget(newObject));
     yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator MoveObjectTowardsTarget(GameObject spawnedObject)
    {
        Vector3 destination = new Vector3(0, 0, 0); // Target position at the center of the scene
        while (spawnedObject != null && spawnedObject.transform.position != destination)
        {
            spawnedObject.transform.position = Vector3.Lerp(spawnedObject.transform.position, destination, objectSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(spawnedObject); // Destroy object once it reaches the target
    }

    private void OnEnable()
    {
        BeginSpawning();
    }
}