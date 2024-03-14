using PlayerStuff;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDrilling : MonoBehaviour
{
    float timer = 1f;
    [SerializeField] float defaultTime = 1f;
    string blockTag = null;
    float hardness = 1f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        blockTag = collision.tag;
        GameObject collisionObj = collision.gameObject;
        BlockStrength(blockTag);
        if (Input.anyKey)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer < 0)
            {
                DestroyObj(collisionObj);
                timer = 1f;
            }
        }
        else
        {
            timer = 1f;
        }

    }

    public static float BlockStrength(string blockTag)
    {
        string tag = blockTag.ToLower();

        switch (tag)
        {
            case "Grass":
                return 1f;

            case "Dirt":
                return 1f;

            case "DarkDirt":
                return 1f;

            case "HardDirt":
                return 1f;

            case "Stone":
                return 1f;

            case "DeepSlate":
                return 1f;

            case "Bedrock":
                return 1f;

            case null:
                return 1f;



        }
        return 1f;
    }

    static void DestroyObj(GameObject collision)
    {
        Destroy(collision.gameObject);
    }

}

