using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    public virtual void Generate(int size)
    {
        if (size == 0)
            return;

        if ((float) size % 2 == 0)
            size -= 1;

        int moveLimit = Mathf.FloorToInt(f: (float) size / 2);

        for (int i = -moveLimit; i <= moveLimit; i++)
        {
            SpawnTile(xPos: i);
        }

        var leftBoundaryTile = SpawnTile(xPos: -moveLimit -1);
        var rightBoundaryTile = SpawnTile(xPos: moveLimit +1);


        DarkenObject(go: leftBoundaryTile);
        DarkenObject(go: rightBoundaryTile);
    }

        private GameObject SpawnTile(int xPos)
        {
            var go = Instantiate(
                original: tilePrefab,
                parent: transform);

            go.transform.localPosition = new Vector3(x: xPos,y: 0,z: 0);

            return go;
        }
        private void DarkenObject(GameObject go)
        {
            var renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
            foreach (var rend in renderers)
            {
                rend.material.color =  rend.material.color * Color.grey;
            }
        }
    }
