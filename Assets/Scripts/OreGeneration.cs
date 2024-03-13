using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OreGeneration : MonoBehaviour
{
    GameObject oreSprite;

    public void SetUpOre(OreTypePrefab prefab)
    {
        oreSprite = new GameObject();
        oreSprite.transform.parent = transform;
        oreSprite.transform.localPosition = Vector3.zero;
        SpriteRenderer addSprite = oreSprite.AddComponent<SpriteRenderer>();
        addSprite.sprite = prefab.oreSprite;
        addSprite.sortingLayerName = "Ore";
    }
}
