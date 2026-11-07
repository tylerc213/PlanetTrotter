
using System;
using UnityEngine;

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
}

