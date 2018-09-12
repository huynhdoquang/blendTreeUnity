using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelParse : MonoBehaviour {
    public string levelName;
    
    string[,] levelDataString;//level array by string
    int rows;
    int cols;

    //tile values for different tile types
    public string invalidTileStr;  // -1
    public string targetTileStr; //o
    public string playerTileStr; // P
    public string crateTileStr; // *
    public string wallTileStr; // #
    public string crateOnTarget; //@
    

    //sprites for different tiles
    public Sprite backgroundSprite;
    
    //list of tilemap (ở đây sẽ định nghĩa các layer luôn)
    public Tilemap background;
    public Tilemap forground;
    //tilebase
    public TileBase tileBackground;
    //prefabs
    public GameObject playgroundParent;
    public GameObject skeletonPrefabs;
    public GameObject cratePrefabs;
    public GameObject targetPrefabs;
    public GameObject wallPrefabs;
    

    void Start()
    {
        background.ClearAllTiles();
        forground.GetComponent<TilemapRenderer>().sortingOrder = 1;
        
        ParseLevelWithout();
        DrawWithout();
    }
    
    void ParseLevelWithout()
    {
        TextAsset textFile = Resources.Load(levelName) as TextAsset;
        string[] lines = textFile.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);//split by new line, return
        rows = lines.Length;//number of rows
        cols = lines[0].Length;//number of columns
        levelDataString = new string[rows, cols];

        
        for (int i = 0; i < rows; i++)
        {
            string st = lines[i];
            string[] nums = new string[st.Length];
            for (int k = 0; k < st.Length; k++)
            {
                nums[k] = st[k].ToString();
            }

            for (int j = 0; j < cols; j++)
            {
                levelDataString[i, j] = nums[j];
            }
        }
    }

    void DrawWithout()
    {
        Vector3Int[] positions = new Vector3Int[cols * rows];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = j + (i * cols);
                string val = levelDataString[i, j];
                if (val != invalidTileStr)
                {
                    positions[index] = new Vector3Int( j, (rows-1) -i, 0);
                    tileArray[index] = tileBackground;

                    // for the target
                    if (val == targetTileStr)
                    {//if it is a destination tile, give different color
                        Debug.Log("targetTile");
                        var target = Instantiate(targetPrefabs);
                        target.transform.position = forground.GetCellCenterWorld(positions[index]);
                        //forground.SetTile(positions[index], tileB);
                    }
                    else
                    {
                        if (val == playerTileStr)
                        {
                            var hero = Instantiate(skeletonPrefabs);
                            hero.transform.position = forground.GetCellCenterWorld(positions[index]);
                        }
                        else if (val == crateTileStr)
                        {
                            var crate = Instantiate(cratePrefabs);
                            crate.transform.position = forground.GetCellCenterWorld(positions[index]);
                        }
                        else if(val == wallTileStr)
                        {
                            var wall = Instantiate(wallPrefabs);
                            wall.transform.position = forground.GetCellCenterWorld(positions[index]);
                        }
                        else if (val == crateOnTarget)
                        {
                            var crate = Instantiate(cratePrefabs);
                            crate.transform.position = forground.GetCellCenterWorld(positions[index]);
                            var target = Instantiate(targetPrefabs);
                            target.transform.position = forground.GetCellCenterWorld(positions[index]);
                        }
                    }
                }
            }
        }
        background.SetTiles(positions, tileArray);
    }
}
