using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Progress;

public class TerrainGeneration : MonoBehaviour
{
    int posX = 0;
    int posY = 0;

    [SerializeField] int height = 0;
    [SerializeField] int width = 0;
    [SerializeField] int endX = 0;
    [SerializeField] int endY = 0;

    //[SerializeField] Tile option1;

    Vector3Int startPosition;


    BoundsInt worldBounds;
    BoundsInt dirtBounds;
    BoundsInt stoneBounds;
    public TileBase defaultTile;
    public TileBase stoneTile;
    public Tilemap tileSet;

    int startSize;
    void Start()
    {
        
        //Sets Vector for Boxfill
        Vector3Int startPosition = new Vector3Int(posX, posY, 0);

        //Sets start position for terrain gen
        tileSet.size = new Vector3Int(endX, endY, 0);

        
        

        //Generates grid
        tileSet = GetComponent<Tilemap>();

        //Get world boundaries
        BoundsInt worldBounds = tileSet.cellBounds;

        SplitWorld();
        BoxFill(tileSet);
        


    }

    public void SplitWorld()
    {
        BoundsInt dirtBounds = new BoundsInt(new Vector3Int(worldBounds.x, (worldBounds.y /2) , 0), new Vector3Int(worldBounds.x, (worldBounds.yMax)));
        BoundsInt stoneBounds = new BoundsInt(new Vector3Int(worldBounds.x, (worldBounds.y / 2), 0), new Vector3Int(worldBounds.x, (dirtBounds.yMin)));


    }

    public void BoxFill(Tilemap tileSet)
    {
        BoundsInt worldBoundsTemp = worldBounds; 
        //DirtGen
        for (int i = 0; i < dirtBounds.x; i++);
        {
            tileSet.BoxFill(startPosition, defaultTile, height, width, endX, endY);
        }
        
        /*for (int i = 0; i < stoneBounds.x; i++);
        {
            tileSet.BoxFill(startPosition, stoneTile, height, width, endX, endY);
        }
        worldBounds = worldBoundsTemp;*/


    }
}