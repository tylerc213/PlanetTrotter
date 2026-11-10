
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Resource1, Resource2, Ice
}

[Serializable]
public class ResourceSaveData
{
    public ResourceType resourceType;
    public Vector3 position;
}

[Serializable]
public class SaveData
{
    public int version = 1;

    public string sceneName;
    public string savedAt;

    public Vector3 playerPosition;
    public Quaternion playerRotation;

    public float oxygen;
    public bool isInBase;

    public float health;
    public float suitIntegrity;
    public float timeOfDay;

    public List<ResourceSaveData> resources = new List<ResourceSaveData>();
}

