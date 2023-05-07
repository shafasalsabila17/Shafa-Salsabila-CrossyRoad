using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePreFabList;
    [SerializeField, Range(min: 0, max: 1)] float treeProbability;
    
    public void SerTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(value: newProbability);
    }
    public override void Generate(int size)
    {
        base.Generate(size: size);
        
        var limit = Mathf.FloorToInt(f: (float) size / 2);
        var treeCount = Mathf.FloorToInt(f: (float) size * treeProbability);

        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(item: i);
        }

        for (int i = 0; i < treeCount; i++)
        {
            var randomIndex = Random.Range(minInclusive: 0, maxExclusive: emptyPosition.Count);
            var pos = emptyPosition[index: randomIndex];

            emptyPosition.RemoveAt(index: randomIndex);

            SpawnRandomTree (pos);
        }

        SpawnRandomTree (-limit - 1);
        SpawnRandomTree (limit + 1);
    }

    private void SpawnRandomTree(int xPos)
    {
            var randomIndex = Random.Range(minInclusive: 0, maxExclusive: treePreFabList.Count);
            var prefab = treePreFabList[index: randomIndex];

            var tree = Instantiate(original: prefab, parent: transform);
            tree.transform.localPosition = new Vector3(x: xPos, y: 0, z: 0);

    }
    
}
