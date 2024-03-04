using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    GameObject[,] blocks;
    [SerializeField] BlockPrefabElements[] blockPrefabElements;
    //[SerializeField]  blockPrefabElements ;

    [SerializeField] int height;
    [SerializeField] int width;

    GameObject child;

    int currentX;
    int currentY;

    int type = 0;

    private void Start()
    {
        for (int y = 0; y < height; y++)
        {
            currentY++;

            for (int x = 0; x < width; x++)
            {
                currentX++;
                BlockType(currentX);
                Instantiate(blockPrefabElements[type].prefab, new Vector3(x, y, 0), Quaternion.identity, transform);
            }

        }
        for (int x = 0; x < width; x++)
        {
            Instantiate(blockPrefabElements[0].prefab, new Vector3(x, height, 0), Quaternion.identity, transform);
        }

    }

    void BlockType(int currentHeight)
    {
        int rand = UnityEngine.Random.Range(0, 10);

        //Bedrock
        if (currentY < (height * 0.30))
        {
            if (currentY < (height * 0.30) && currentY >= (height * 0.26) && rand >= 6)
            {
                type = 5;
            }
            else if (currentY < (height * 0.30) && currentY >= (height * 0.29) && rand >= 2)
            {
                type = 5;
            }
            else
            {
                type = 6;
            }
        }
        //Deepslate
        else if (currentY < (height * 0.50))
        {
            if (currentY < (height * 0.50) && currentY >= (height * 0.46) && rand >= 6)
            {
                type = 4;
            }
            else if (currentY < (height * 0.50) && currentY >= (height * 0.48) && rand >= 2)
            {
                type = 4;
            }
            else
            {
                type = 5;
            }
        }
        //Stone
        else if (currentY < (height * 0.70))
        {
            if (currentY < (height * 0.70) && currentY >= (height * 0.66) && rand >= 6)
            {
                type = 3;
            }
            else if (currentY < (height * 0.70) && currentY >= (height * 0.68) && rand >= 2)
            {
                type = 3;
            }
            else
            {
                type = 4;
            }
        }
        //HardDirt
        else if (currentY < (height * 0.80))
        {
            if (currentY < (height * 0.80) && currentY >= (height * 0.76) && rand >= 6)
            {
                type = 2;
            }
            else if (currentY < (height * 0.80) && currentY >= (height * 0.78) && rand >= 2)
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
        }
        //DarkDirt
        else if (currentY < (height * 0.90))
        {
            if (currentY < (height * 0.90) && currentY >= (height * 0.86) && rand >= 6)
            {
                type = 1;
            }
            else if (currentY < (height * 0.90) && currentY >= (height * 0.88) && rand >= 2)
            {
                type = 1;
            }
            else
            {
                type = 2;
            }
        }
        //Dirt
        else
        {
            type = 1;
        }
    }

}

[Serializable]
public struct BlockPrefabElements
{
    public string name;
    public GameObject prefab;
    public int hardness;
}

