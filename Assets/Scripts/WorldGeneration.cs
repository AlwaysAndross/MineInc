using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    GameObject[,] blocks;
    [SerializeField] BlockPrefabElements[] blockPrefabElements;
    [SerializeField] OreTypePrefab[] orePrefabElements;
    [SerializeField] GameObject Ores;
    OreGeneration oreComponent;

    [SerializeField] int height;
    [SerializeField] int width;

    GameObject child;

    int currentX;
    int currentY;

    bool oreSpawned = false;

    int type = 0;

    private void Start()
    {
        for (int y = 0; y < height; y++)
        {
            currentY++;

            for (int x = 0; x < width; x++)
            {
                oreSpawned = false;
                currentX++;
                BlockType();
                GameObject createdBlock = Instantiate(blockPrefabElements[type].prefab, new Vector3((x + 0.5f), (y + 0.5f), 0), Quaternion.identity, transform);
                oreComponent = createdBlock.AddComponent<OreGeneration>();
                OreGen();
            }

        }
        for (int x = 0; x < width; x++)
        {
            Instantiate(blockPrefabElements[0].prefab, new Vector3(x, height, 0), Quaternion.identity, transform);
        }

    }

    void BlockType()
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

    void OreGen()
    {
        int rand = Reroll();
        
        if (currentY < (height * 0.99) && currentY > (height * 0.80) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[0]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.95) && currentY > (height * 0.77) && !oreSpawned && rand < 2) 
        {
            oreComponent.SetUpOre(orePrefabElements[1]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.90) && currentY > (height * 0.70) && !oreSpawned && rand < 2) 
        {
            oreComponent.SetUpOre(orePrefabElements[2]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.81) && currentY > (height * 0.60) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[3]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.75) && currentY > (height * 0.50) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[4]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.62) && currentY > (height * 0.35) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[5]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.55) && currentY > (height * 0.24) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[6]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.45) && currentY > (height * 0.13) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[7]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.30) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[8]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }
        if (currentY < (height * 0.10) && !oreSpawned && rand < 2)
        {
            oreComponent.SetUpOre(orePrefabElements[9]);
            oreSpawned = true;
        }
        else
        {
            Reroll();
        }

    }

    static int Reroll()
    {

        int rand = UnityEngine.Random.Range(0, 20);
        return rand;
    }


}

[Serializable]
public struct BlockPrefabElements
{
    public string name;
    public GameObject prefab;
    public int hardness;
}

public enum OreType
{
    COPPER,
    IRON,
    SILVER,
    GOLD,
    RUBY,
    SAPPHIRE,
    EMERALD,
    DIAMOND,
    MULTI,
    COAL,
}

[Serializable]
public struct OreTypePrefab
{
    public OreType type;
    public Sprite oreSprite;
}