using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] BlockPrefabElements[] blockPrefabElements;
    [SerializeField] OreGenerationLocationData[] oreGenerationLocationData;
    [SerializeField] GameObject playerPrefab;
    GameObject border;
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] InventoryData inventoryData;

    [SerializeField] int height;
    [SerializeField] int width;


    int currentX;
    int currentY;

    System.Random random;

    int type = 0;

    private void Start()
    {
        random = new System.Random(1234);
        for (int y = 0; y < height; y++)
        {
            currentY++;

            for (int x = 0; x < width; x++)
            {
                currentX++;
                BlockType();
                GameObject createdBlock = Instantiate(blockPrefabElements[type].prefab, new Vector3((x + 0.5f), (y + 0.5f), 0), Quaternion.identity, transform);
                OreTypePrefab oreGenData;
                if (OreGen(out oreGenData))
                {
                    OreGeneration oreComponent = createdBlock.AddComponent<OreGeneration>();
                    oreComponent.SetUpOre(oreGenData);
                };

            }

        }
        for (int x = 0; x < width; x++)
        {
            Instantiate(blockPrefabElements[0].prefab, new Vector3((x + 0.5f), (height + 0.5f), 0), Quaternion.identity, transform);
        }
        GameObject player = Instantiate(playerPrefab, new Vector3(0, (height + 1.5f), 0), Quaternion.identity);
        player.transform.GetChild(0).GetComponent<PlayerDrilling>().inventoryData = inventoryData;
        camera.Follow = player.transform;
        CreateBorders();
    }

    void CreateBorders()
    {
        CreateBorderCollider(-0.25f, height / 2 + 1, 0.5f, height + 4);
        CreateBorderCollider(width + 0.25f, height / 2 + 1, 0.5f, height + 4);
        CreateBorderCollider(width / 2 - 1, -0.25f, width + 4, 0.5f);
        CreateBorderCollider(width / 2 - 1, height + 2.25f, width + 4, 0.5f);
    }

    void CreateBorderCollider(float posX, float posY, float scaleX, float scaleY)
    {
        border = new GameObject();
        BoxCollider2D collider = border.AddComponent<BoxCollider2D>();
        collider.transform.position = new Vector3(posX, posY);
        collider.transform.localScale = new Vector3(scaleX, scaleY);

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

    bool OreGen(out OreTypePrefab result)
    {
        result = oreGenerationLocationData[0].oreGenerationData; // need to assign something

        foreach (OreGenerationLocationData data in oreGenerationLocationData)
        {
            int rand = Reroll();
            if (
                currentY < (height * data.maxHeightPercentage) &&
                currentY > (height * data.minHeightPercentage) &&
                rand < data.chance)
            {
                result = data.oreGenerationData;
                return true;
            }
        }
        return false;

    }

    int Reroll()
    {
        return random.Next(20);
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

public enum BlockType
{
    GRASS,
    DIRT,
    DARKDIRT,
    HARDDIRT,
    STONE,
    DEEPSLATE,
    BEDROCK,
}

[Serializable]
public struct OreTypePrefab
{
    public OreType type;
    public Sprite oreSprite;
}

[Serializable]
public struct OreGenerationLocationData
{
    public float minHeightPercentage;
    public float maxHeightPercentage;
    public int chance;
    public OreTypePrefab oreGenerationData;
}
