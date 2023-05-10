using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpwaner : MonoBehaviour
{
    [SerializeField] Eagle eagle;
    [SerializeField] Duck duck;
    [SerializeField] float initialTimer = 10;

    float timer;

    void Start()
    {
        timer = initialTimer;
        eagle.gameObject.SetActive(false);
    }

    void Upate ()
    {
        if (timer <= 0 && eagle.gameObject.activeInHierarchy ==  false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = duck.transform.position + new Vector3 (0, 0, 13);
            duck.SetMoveable(false);
        }
            timer -= Time.deltaTime;
        
    }

    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
