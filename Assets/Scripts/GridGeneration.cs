using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] int width, height;

    [SerializeField] GameObject dirt, grass, stone, deepslate;

    ArrayList stoneArr = new ArrayList();
    ArrayList deepArr = new ArrayList();

    void Start()
    {
        Generation();
        
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            deepArr = deepGenChance(height, width);
            stoneArr = stoneGenChance(height, width);
            
            int chanceDeepSpawn = int.Parse(deepArr[0].ToString());
            int maxDeep = int.Parse(deepArr[1].ToString());
            int blendDeep = int.Parse(deepArr[2].ToString());

            Console.WriteLine("Deep chance : " + chanceDeepSpawn);
            Console.WriteLine("Deep max : " + maxDeep);
            Console.WriteLine("Deep feather : " + blendDeep);

            int chanceStoneSpawn = int.Parse(stoneArr[0].ToString());
            int maxStone = int.Parse(stoneArr[1].ToString());
            int blendStone = int.Parse(stoneArr[2].ToString());

            Console.WriteLine("Stone chance : " + chanceStoneSpawn);
            Console.WriteLine("Stone max : " + maxStone);
            Console.WriteLine("Stone feather : " + blendStone);

            

            for (int y = 0; y < height; y++)
            {
                int rand = UnityEngine.Random.Range(0, 10);
                if (y < chanceDeepSpawn)
                {
                    if ( y > maxDeep & rand > 1)
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

                    if ( y > maxStone & rand > 3)
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

    ArrayList stoneGenChance(int height, int width)
    {
        ArrayList stoneArr = new ArrayList();

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

        return stoneArr;
    }
    ArrayList deepGenChance(int height, int width)
    {
        ArrayList deepArr = new ArrayList();

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

        return deepArr;
    }
}
