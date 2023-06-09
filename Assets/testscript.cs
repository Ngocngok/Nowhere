using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public GameObject cube;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            for(int j = 0; j < 100; j++)
            {
                for (int k = 0; k < 1; k++)
                {
                    Instantiate(cube, new Vector3(i, j, k), Quaternion.identity);
                }
            }
        }
    }
}
