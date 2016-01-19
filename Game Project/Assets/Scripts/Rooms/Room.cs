using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public GameObject Flooring10x10Tile;
    public GameObject Wall10x1BlockX;
    public GameObject Wall10x1BlockZ;
    
    public int dimensionsX = 1;
    public int dimensionsZ = 1;
    public int dimensionsY = 1;
    private int _tileSize = 10;
    private int _wallBlockHeight = 1;
    private int _wallThickness = 1;
    
    public enum TILEPOS { UP, DOWN, LEFT, RIGHT};
	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Initializing
    void Initialize()
    {
        InitializeFloors();
        InitializeWalls();
    }

    void InitializeFloors()
    {
        float expectedSize_X = dimensionsX * _tileSize;
        float expectedSize_Z = dimensionsZ * _tileSize;

        //centering of field: shift (0.5 * tilesize) to make bottom left 0,0, then offset by expectedsize/2
        float offset_x = (expectedSize_X / 2) - (0.5f *_tileSize); 
        float offset_z = (expectedSize_Z / 2) - (0.5f *_tileSize);
        for(int i = 0; i < dimensionsX; i++)
        {
            for(int j = 0; j <dimensionsZ; j++)
            {
                InitializeTile(i, j, offset_x, offset_z);
            }
        }
        
    }

    void InitializeWalls()
    {
        //int start_index_x = 0;
        //int end_index_x = dimensionsX;
        //int start_index_z = 0;
        //int end_index_z = dimensionsZ;

        //for(int i = start_index_x; i <end_index_x; i++)
        //{
        //    int coordinate_z = start_index_z;
        //    InitializeWallZ(i, coordinate_z);
        //}
        //for (int i = start_index_x; i < end_index_x; i++)
        //{
        //    int coordinate_z = start_index_z;
        //    InitializeWallZ(i, coordinate_z);
        //}
        //for (int i = start_index_z; i < end_index_z; i++)
        //{
        //    int coordinate_x = start_index_z;
        //    InitializeWallX(i, coordinate_x);
        //}
        //for (int i = start_index_z; i < end_index_z; i++)
        //{
        //    int coordinate_x = start_index_z;
        //    InitializeWallX(i, coordinate_x);
        //}

        float expectedSize_X = dimensionsX * _tileSize;
        float expectedSize_Z = dimensionsZ * _tileSize;
        float expectedSize_Y = dimensionsY *_wallBlockHeight;
        //centering of field: shift (0.5 * tilesize) to make bottom left 0,0, then offset by expectedsize/2
        float offset_x = (expectedSize_X / 2) - (0.5f * _tileSize);
        float offset_z = (expectedSize_Z / 2) - (0.5f * _tileSize);
        float offset_y = -(0.5f *_wallBlockHeight);
        for (int i = 0; i < dimensionsX; i++)
        {
            for(int j = 0; j < dimensionsZ; j++)
            {
                for(int k = 0; k <dimensionsY; k++)
                {
                    if (i == 0)
                    {
                        InitializeWall(i, j, k, TILEPOS.LEFT, offset_x, offset_z, offset_y, Wall10x1BlockZ);
                    }
                    else if (i == (dimensionsX - 1))
                    {
                        InitializeWall(i, j, k, TILEPOS.RIGHT, offset_x, offset_z, offset_y, Wall10x1BlockZ);
                    }
                    else if (j == 0)
                    {
                        InitializeWall(i, j, k, TILEPOS.DOWN, offset_x, offset_z, offset_y, Wall10x1BlockX);
                    }
                    else if (j == (dimensionsZ - 1))
                    {
                        InitializeWall(i, j, k, TILEPOS.UP, offset_x, offset_z, offset_y, Wall10x1BlockX);
                    }
                }
                
            }
        }
    }

    void InitializeTile(int index_x, int index_z, float offset_x, float offset_z)
    {
        if(index_z >= 0 && index_x >= 0)
        {
            
            GameObject tile = GameObject.Instantiate(Flooring10x10Tile);
            float tile_position_x = (index_x) * _tileSize - offset_x;
            float tile_position_z = (index_z) * _tileSize - offset_z;
            tile.transform.position = new Vector3(tile_position_x, 0, tile_position_z);
        }
        else
        {
            Debug.Log("Invalid tile index in Room.cs");
        }
        
    }

    void InitializeWall(int index_x, int index_z, int index_y, TILEPOS position, float offset_x, float offset_z, float offset_y, GameObject WallBlock)
    {
        float positionOffsetHalfTileUD = 0;
        float positionOffsetHalfTileLR = 0;
        if (position == TILEPOS.UP)
        {
            positionOffsetHalfTileUD = 0.5f * _tileSize - (0.5f *_wallThickness);
        }
        else if (position == TILEPOS.DOWN)
        {
            positionOffsetHalfTileUD = -(0.5f * _tileSize - (0.5f * _wallThickness));
        }
        else if (position == TILEPOS.LEFT)
        {
            positionOffsetHalfTileLR = -(0.5f * _tileSize - (0.5f * _wallThickness));
        }
        else if (position == TILEPOS.RIGHT)
        {
            positionOffsetHalfTileLR = 0.5f * _tileSize - (0.5f * _wallThickness);
        }
        GameObject wall = GameObject.Instantiate(WallBlock);
        float wall_position_x = (index_x) * _tileSize - offset_x + positionOffsetHalfTileLR;
        float wall_position_z = (index_z) * _tileSize - offset_z + positionOffsetHalfTileUD;
        float wall_position_y = (index_y) * _wallBlockHeight - offset_y;
        wall.transform.position = new Vector3(wall_position_x, wall_position_y, wall_position_z);
    }
  
    #endregion
}
