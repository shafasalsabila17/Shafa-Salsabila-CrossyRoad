using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Duck duck;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(min: 0, max: 1)] float treeProbability;

    Dictionary<int,Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;

    private void Start()
    {
        
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {  
            var terrain = Instantiate(original: terrainList[0]);
            
            terrain.transform.position =  new Vector3(x: 0, y: 0, z: zPos);

            if (terrain is Grass grass)
             grass.SerTreePercentage(zPos < - 1 ? 1 : 0);

;           terrain.Generate(size: horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }

        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        { 
            var terrain = SpawnRandomTerrain(zPos); 

            terrain.transform.position =  new Vector3(x: 0, y: 0, z: zPos);

;           terrain.Generate(size: horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }

        SpawnRandomTerrain(0);
    }

        private Terrain SpawnRandomTerrain(int zPos)
        {
            Terrain terrainCheck = null;
            int randomIndex;
            Terrain terrain = null;

            for (int z = -1; z >= - 3; z--)
            {
                var checkPos = zPos + z;

                if (terrainCheck == null)
                {
                 terrainCheck = activeTerrainDict[checkPos];
                 continue;
                }
                else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
                {
                    randomIndex = Random.Range(0, terrainList.Count);
                    terrain = Instantiate(terrainList[randomIndex]);
                    terrain.transform.position = new Vector3(0, 0, zPos);
                    return terrain;
                }
                else
                {
                    continue;
                }
            }
            
            var candidateTerrain = new List<Terrain>(terrainList);
            for (int i = 0; i < candidateTerrain.Count; i++)
            {
                if (terrainCheck.GetType() == candidateTerrain[i].GetType())
                {
                    candidateTerrain.Remove(candidateTerrain[i]);
                    break;
                }
            }

            var randomIndex2 = Random.Range(0, candidateTerrain.Count);
            var terrain2 = Instantiate(candidateTerrain[randomIndex2]);
            terrain2.transform.position = new Vector3 (0, 0 ,zPos);
            return terrain2;
        }

        private void Update() {
            if (duck.transform.position.z > travelDistance)
            {
                travelDistance = Mathf.CeilToInt(duck.transform.position.z);
            }

        }
}
