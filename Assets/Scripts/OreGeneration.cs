using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OreGeneration : MonoBehaviour
{
    GameObject oreSprite;
    OreTypePrefab oreTypeSet;

    public OreTypePrefab GetOreType()
    {
        return oreTypeSet;
    }

    public void SetUpOre(OreTypePrefab prefab)
    {
        oreSprite = new GameObject();
        oreSprite.transform.parent = transform;
        oreSprite.transform.localPosition = Vector3.zero;
        SpriteRenderer addSprite = oreSprite.AddComponent<SpriteRenderer>();
        oreSprite.transform.tag = "Ore";
        oreSprite.transform.name = prefab.type.ToString() + "_Ore";
        addSprite.sprite = prefab.oreSprite;
        addSprite.sortingLayerName = "Ore";
        oreTypeSet = prefab;
       
    }
}
