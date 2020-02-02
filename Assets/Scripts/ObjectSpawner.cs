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

    //
    private List<int> randomSequence = new List<int>();

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

        int idx = GetNewRandom(); //Random.Range(0, prefabs.Count);

        // Only spawn those once we have spawned at least three other objects
        while (prefabs[idx].GetComponent<WutBehavior>() && totalSpawnCount <= 3) {
            idx = Random.Range(0, prefabs.Count);
        }

        var prefab = prefabs[idx];

        ++totalSpawnCount;
        return Instantiate(prefab, pos, prefab.transform.localRotation).gameObject;
    }

    private int GetNewRandom() {
        if (this.randomSequence.Count == 0) {
            this.randomSequence = new List<int>();
            for (int i = 0; i < this.prefabs.Count; i++) {
                this.randomSequence.Add(i);
            }

            Shuffle(this.randomSequence, 20);
            //myList.Sort((a, b) => 1 - 2 * Random.Range(0, 1)); //myList.Shuffle();//OrderBy(x => Random.value).ToList();
        }
        var randomIndex = Random.Range(0, this.randomSequence.Count);
        var randomObjectId = this.randomSequence[randomIndex];
        this.randomSequence.RemoveAt(randomIndex);

        return randomObjectId;

    }

    private void Shuffle(List<int> in_list, int steps) {
        for (int i = 0; i < steps; i++) {
            var fromIndex = Random.Range(0, in_list.Count);
            var toIndex = Random.Range(0, in_list.Count);

            var fromValue = in_list[fromIndex];
            var toValue = in_list[toIndex];

            in_list[fromIndex] = toValue;
            in_list[toIndex] = fromValue;
        }
    }
}
