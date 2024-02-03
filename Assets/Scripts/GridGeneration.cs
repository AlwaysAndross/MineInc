using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] int width, height;

    [SerializeField] GameObject dirt, grass, stone, deepslate;
    [SerializeField] float stoneChanceMin, stoneChanceMax, stoneChanceB, deepslateMin, deepslateMax, deepslateB;

    List<int> stoneArr = new List<int>();
    List<int> deepArr = new List<int>();

    void Start()
    {
        Generation();
    }

    void Generation()
    {

        for (int x = 0; x < width; x++)
        {

            //getting lint from methods for grid seperation

            //making grid
            for (int y = 0; y < height; y++)
            {
                int rand = UnityEngine.Random.Range(0, 100); //RNG for block

                if (y < (height * deepslateMin)) //DeepSpawn
                {
                    if (y > (height * deepslateMin) & rand > 5)
                    {

                        spawnObj(stone, x, y);
                    }
                    else if (y > (height * deepslateMax) & rand > 20)
                    {
                        spawnObj(stone, x, y);
                    }
                    else
                    {
                        spawnObj(deepslate, x, y);
                    }

                }
                else if (y < (height * stoneChanceMin))
                {
                    if (y > (height * stoneChanceMin) & rand > 5)
                    {
                        spawnObj(dirt, x, y);
                    }

                    else if (y > (height * stoneChanceB) & rand > 10)
                    {
                        spawnObj(dirt, x, y);
                    }
                    else if (y > (height * stoneChanceMax) & rand > 30)
                    {
                        spawnObj(dirt, x, y);
                    }
                    else
                    {
                        spawnObj(stone, x, y);
                    }

                }
                else
                {

                    spawnObj(dirt, x, y);
                }
            }

            spawnObj(grass, x, height);

        }

    }

    void spawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}

