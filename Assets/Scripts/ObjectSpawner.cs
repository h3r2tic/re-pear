using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    public Vector3 offset = new Vector3(0.0f, 5.0f, 0.0f);
    public Vector2 spawnerExtents = new Vector2(2.0f, 2.0f);
    public List<Controllable> prefabs = new List<Controllable>();

    // Spawn a few at the start
    public int targetDisconnectedObjectCount = 3;
    public int disconnectedCount = 0;
    int totalSpawnCount = 0;

    public static ObjectSpawner instance;
    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        while (disconnectedCount < targetDisconnectedObjectCount) {
            spawnRandom();
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(offset, new Vector3(spawnerExtents.x, 0.0f, spawnerExtents.y));
    }

    public GameObject spawnRandom() {
        if (0 == prefabs.Count) {
            return null;
        }

        Vector3 pos = new Vector3(Random.Range(-spawnerExtents.x, spawnerExtents.x), 0.0f, Random.Range(-spawnerExtents.y, spawnerExtents.y)) + this.offset;

        int idx = Random.Range(0, prefabs.Count);

        // Only spawn those once we have spawned at least three other objects
        while (prefabs[idx].GetComponent<WutBehavior>() && totalSpawnCount <= 3) {
            idx = Random.Range(0, prefabs.Count);
        }

        var prefab = prefabs[idx];

        ++totalSpawnCount;
        return Instantiate(prefab, pos, prefab.transform.localRotation).gameObject;
    }
}
