using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] int width, height;

    [SerializeField] GameObject dirt, grass, stone, deepslate;

    List<int> stoneArr = new List<int>();
    List<int> deepArr = new List<int>();

    void Start()
    {
        Generation();
    }

    void Generation()
    {
        deepGenChance(height, width);
        stoneGenChance(height, width);

        for (int x = 0; x < width; x++)
        {
            

            int chanceDeepSpawn = deepArr[0];
            int maxDeep = deepArr[1];
            int blendDeep = deepArr[2];

            Console.WriteLine("Deep chance : " + chanceDeepSpawn);
            Console.WriteLine("Deep max : " + maxDeep);
            Console.WriteLine("Deep feather : " + blendDeep);

            int chanceStoneSpawn = stoneArr[0];
            int maxStone = stoneArr[1]; 
            int blendStone = stoneArr[0];

            Console.WriteLine("Stone chance : " + chanceStoneSpawn);
            Console.WriteLine("Stone max : " + maxStone);
            Console.WriteLine("Stone feather : " + blendStone);



            for (int y = 0; y < height; y++)
            {
                int rand = UnityEngine.Random.Range(0, 10);
                if (y < chanceDeepSpawn)
                {
                    if (y > maxDeep & rand > 1)
                    {

                        spawnObj(stone, x, y);
                    }
                    else if (y > (maxDeep * 0.9) & rand > 3)
                    {
                        spawnObj(stone, x, y);
                    }
                    else
                    {
                        spawnObj(deepslate, x, y);
                    }

                }
                else if (y < chanceStoneSpawn)
                {

                    if (y > maxStone & rand > 3)
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

    public void stoneGenChance(int height, int width)
    {

        double CalminSpawn = (height / 1.5) + (height * 0.2);
        double CalmaxSpawn = height / 1.5;
        double CalblendSpawn = (height / 2) * 0.8;

        int minSpawn = (int)CalminSpawn;
        int maxSpawn = (int)CalmaxSpawn;

        int totalStoneSpawn = UnityEngine.Random.Range(maxSpawn, minSpawn); //Randomizing Ints
        stoneArr.Add(totalStoneSpawn);
        int stoneMaxSpawn = (int)CalmaxSpawn; //Max Stone Value
        stoneArr.Add(stoneMaxSpawn);
        int stoneBlendSpawn = (int)CalblendSpawn; //Feathering
        stoneArr.Add(stoneBlendSpawn);
    }


    public void deepGenChance(int height, int width)
    {

        double CalminSpawn = (height / 4) + (height * 0.1);
        double CalmaxSpawn = height / 4;
        double CalblendSpawn = (height / 4) * 0.8;

        int minSpawn = (int)CalminSpawn;
        int maxSpawn = (int)CalmaxSpawn;

        int totalDeepSpawn = UnityEngine.Random.Range(maxSpawn, minSpawn); //Randomizing Ints
        deepArr.Add(totalDeepSpawn);
        int deepMaxSpawn = (int)CalmaxSpawn; //Max Deep Value
        deepArr.Add(deepMaxSpawn);
        int deepBlendSpawn = (int)CalblendSpawn; //Feathering
        deepArr.Add(deepBlendSpawn);
    }
}
