using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{

    [Header("Resource Prefabs")]
    public GameObject resource1Prefab;
    public GameObject resource2Prefab;
    public GameObject icePrefab;

    [Header("Resource Counts")]
    public int resource1Count = 15;
    public int resource2Count = 10;
    public int iceCount = 20;

    [Header("Resource Spawn Distance")]
    public float resource1MinRadius = 10f;
    public float resource1MaxRadius = 45f;
    public float resource2MinRadius = 50f;
    public float resource2MaxRadius = 90f;
    public float mapHalfSize = 100f;
    public float edgePadding = 3f;

    public Vector3 basePosition = new Vector3(15f, 0f, 0f);
    public float baseExclusionHalfWidth = 9f;

    public float minimumResourceDistance = 3f;

    private class SpawnedResource
    {
        public GameObject resourceObject;
        public ResourceType resourceType;
    }

    private List<SpawnedResource> spawnedResources = new List<SpawnedResource>();

    private List<Vector3> spawnedPositions = new List<Vector3>();

    private Transform resourceContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNewResources()
    {
        if (!PrefabsAssigned())
        {
            return;
        }

        ClearResources();
        CreateResourceContainer();

        SpawnRadialResources(resource1Prefab, ResourceType.Resource1, resource1Count, resource1MinRadius, resource1MaxRadius);
        SpawnRadialResources(resource2Prefab, ResourceType.Resource2, resource2Count, resource2MinRadius, resource2MaxRadius);
        SpawnRandomIce();

    }

    public List<ResourceSaveData> GetRemainingResources()
    {
        List<ResourceSaveData> remainingResources = new List<ResourceSaveData>();

        foreach (SpawnedResource resource in spawnedResources)
        {
            if (resource.resourceObject == null)
            {
                continue;
            }

            ResourceSaveData data = new ResourceSaveData();

            data.resourceType = resource.resourceType;
            data.position = resource.resourceObject.transform.position;

            remainingResources.Add(data);
        }

        return remainingResources;
    }

    public void RestoreResources(List<ResourceSaveData> savedResources)
    {
        ClearResources();
        CreateResourceContainer();

        if (savedResources == null)
        {
            return;
        }
        
        foreach (ResourceSaveData data in savedResources)
        {
            GameObject prefab = GetPrefab(data.resourceType);

            if (prefab == null)
            {
                continue;
            }

            SpawnResource(prefab, data.resourceType, data.position);

        }
    }

    public void RemoveResource(GameObject resourceObject)
    {
        for (int i = spawnedResources.Count - 1; i >= 0; i--)
        {
            if (spawnedResources[i].resourceObject == resourceObject)
            {
                spawnedResources.RemoveAt(i);
                break;
            }
        }

        if (resourceObject != null)
        {
            Destroy(resourceObject);
        }
    }

    private bool PrefabsAssigned()
    {
        if (resource1Prefab == null || resource2Prefab == null || icePrefab == null)
        {
            return false;
        }

        return true;
    }

    private void CreateResourceContainer()
    {
        GameObject container = new GameObject("GeneratedResources");

        container.transform.SetParent(transform);
        container.transform.localPosition = Vector3.zero;

        resourceContainer = container.transform;

    }

    public void ClearGeneratedResources()
    {
        ClearResources();
    }


    private void ClearResources()
    {
        if (resourceContainer != null)
        {
            Destroy(resourceContainer.gameObject);
        }

        spawnedResources.Clear();
        spawnedPositions.Clear();
        resourceContainer = null;
    }

    private void SpawnRadialResources(GameObject prefab, ResourceType resourceType, int count, float minRadius, float maxRadius)
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = count * 50;

        while (spawned < count && attempts < maxAttempts)
        {
            attempts++;
            float angle = Random.Range(0f, Mathf.PI * 2f);

            float radius = Mathf.Sqrt(Random.Range(minRadius * minRadius, maxRadius * maxRadius));

            float x = basePosition.x + Mathf.Cos(angle) * radius;
            float z = basePosition.z + Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(x, prefab.transform.localScale.y / 2f, z);

            if (!IsValidPosition(position))
            {
                continue;
            }

            SpawnResource(prefab, resourceType, position);
            spawned++;
        }

    }

    private void SpawnRandomIce()
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = iceCount * 50;

        while (spawned < iceCount && attempts < maxAttempts)
        {
            attempts++;

            float x = Random.Range(-mapHalfSize + edgePadding, mapHalfSize - edgePadding);
            float z = Random.Range(-mapHalfSize + edgePadding, mapHalfSize - edgePadding);

            Vector3 position = new Vector3(x, icePrefab.transform.localScale.y / 2f, z);

            if (!IsValidPosition(position))
            {
                continue;
            }

            SpawnResource(icePrefab, ResourceType.Ice, position);
            spawned++;
        }

    }

    private void SpawnResource(GameObject prefab, ResourceType resourceType, Vector3 position)
    {
        GameObject resourceObject = Instantiate(prefab, position, Quaternion.identity, resourceContainer);

        SpawnedResource resource = new SpawnedResource();

        resource.resourceObject = resourceObject;
        resource.resourceType = resourceType;

        spawnedResources.Add(resource);

        spawnedPositions.Add(new Vector3(position.x, 0f, position.z));

    }

    private GameObject GetPrefab(ResourceType resourceType)
    {
        switch(resourceType)
        {
            case ResourceType.Resource1:
                return resource1Prefab;

            case ResourceType.Resource2:
                return resource2Prefab;

            case ResourceType.Ice:
                return icePrefab;

            default:
                return null;
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        if (position.x < -mapHalfSize + edgePadding || position.x > mapHalfSize - edgePadding || position.z < -mapHalfSize + edgePadding || position.z > mapHalfSize - edgePadding)
        {
            return false;
        }

        if (Mathf.Abs(position.x - basePosition.x) < baseExclusionHalfWidth && Mathf.Abs(position.z - basePosition.z) < baseExclusionHalfWidth)
        {
            return false;
        }

        foreach (Vector3 existingPosition in spawnedPositions)
        {
            float distance = Vector2.Distance(new Vector2(position.x, position.z), new Vector2(existingPosition.x, existingPosition.z));

            if (distance < minimumResourceDistance)
            {
                return false;
            }
        }

        return true;
    }
}
