using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroers : MonoBehaviour
{
    public float deadTime;
    void Start()
    {
        Destroy(gameObject, deadTime);
    }
}
