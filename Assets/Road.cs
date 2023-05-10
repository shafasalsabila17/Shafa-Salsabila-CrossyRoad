using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
  [SerializeField] Car carPreFab;
  [SerializeField] float minCarSpawnInterval;
  [SerializeField] float maxCarSpaenInterval;

  float timer;

  Vector3 CarspawnPosition;
  Quaternion carRotation;

  private void Start()
  {
    if (Random.value > 0.5f)
    {
    CarspawnPosition = new Vector3(
        horizontalSize / 2 + 10,
        0,
        this.transform.position.z); 

        carRotation = Quaternion.Euler(0, -90, 0);
    }
    else
    {
    CarspawnPosition = new Vector3(
        -(horizontalSize / 2 + 10),
        0,
        this.transform.position.z);

        carRotation = Quaternion.Euler(0, 90, 0);
    }
  }

  private void Update()
  {
    if (timer <= 0 )
    {
        timer =  Random.Range(
            minCarSpawnInterval, 
            maxCarSpaenInterval);

        var car = Instantiate(
            carPreFab,
            CarspawnPosition,
            carRotation);

        car.SetUpDistanceLimit(horizontalSize + 30);

            return;
    }

    timer -= Time.deltaTime;
  }
}
