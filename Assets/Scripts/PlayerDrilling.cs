using PlayerStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDrilling : MonoBehaviour
{
    const float damage = 1;
    BlockTypeData currentBlock;
    public InventoryData inventoryData;

    public void Update()
    {
        if (currentBlock == null) return;

        if (Input.anyKey)
        {
            currentBlock.health -= damage * Time.deltaTime;
            if (currentBlock.health < 0)
            {
                OreGeneration oreComponent = currentBlock.GetComponent<OreGeneration>();
                if (oreComponent != null)
                {
                    addOreItemToInventory(oreComponent.GetOreType().type);
                }
                DestroyObj(currentBlock.gameObject);
                currentBlock = null;
            }
        }
    }

    void addOreItemToInventory(OreType oreType)
    {
        if (inventoryData.CanAddOre())
        {
            inventoryData.AddOre(oreType);
        }
        
        inventoryData.printInventory();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        BlockTypeData blockData = collisionObj.GetComponent<BlockTypeData>();
        if (blockData == null) return;
        currentBlock = blockData;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        BlockTypeData blockData = collisionObj.GetComponent<BlockTypeData>();
        if (blockData == null) return;
        currentBlock = null;
    }


    static void DestroyObj(GameObject collision)
    {
        Destroy(collision.gameObject);
    }

}

