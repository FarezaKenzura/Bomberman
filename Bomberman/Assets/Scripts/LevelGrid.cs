using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Transform groundPrefab;
    [SerializeField] private Transform groundShadowPrefab;
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform brickPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Transform brickContainer;

    [Header("Grid Size")]
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float gridCellSize = 1f;

    [Header("Item")]
    [SerializeField] private Transform blastRadiusPrefab;
    [SerializeField] private Transform extraBombPrefab;
    [SerializeField] private Transform speedIncreasePrefabs;

    private GridSystem<GridInfo> gridSystem;

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("There's more one LevelGrid!" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem<GridInfo>(gridWidth, gridHeight, gridCellSize,
            (GridSystem<GridInfo> g, GridPosition gridPosition ) => new GridInfo(g, gridPosition));
    }

    public void SpawnGrid()
    {
        for(int x = -1; x <= gridWidth; x++)
        {
            for(int y = -1; y <= gridHeight; y++)
            {
                Vector3 position = new Vector3(x, y, 2) * gridCellSize;
                Transform prefab; 

                if (position == new Vector3(0, 0, 0) ||
                    position == new Vector3(1, 0, 0) ||
                    position == new Vector3(0, 1, 0)) continue;
                
                if (x == -1 || y == -1 || x == gridWidth || y == gridHeight)
                {
                    prefab = blockPrefab;
                }
                else if (y == gridHeight - 1)
                {
                    prefab = groundShadowPrefab;
                }
                else if (y % 2 != 0 && x % 2 != 0)
                {
                    prefab = blockPrefab;
                }
                else if (y % 2 == 0 && x % 2 != 0)
                {
                    prefab = groundShadowPrefab;
                }
                else prefab = groundPrefab;

                Instantiate(prefab, position, Quaternion.identity, gridContainer);
            }
        }
    }

    public void SpawnRandomBrick()
    {
        for(int x = 0; x <= gridWidth; x++)
        {
            for(int y = 0; y <= gridHeight; y++)
            {
                Vector3 position = new Vector3(x, y, 1) * gridCellSize;
                Transform prefab = brickPrefab;

                if (position == new Vector3(0, 0, 1) ||
                    position == new Vector3(1, 0, 1) ||
                    position == new Vector3(0, 1, 1)) continue;
                else if (y % 2 != 0 && x % 2 != 0) continue;
                else if (x == gridWidth || y == gridHeight) continue;

                int chance = Random.Range(0, 2);
                if (chance == 0) continue;
                Instantiate(prefab, position, Quaternion.identity, brickContainer);
            }
        }
    }

    public void SpawnRandomItem(Vector3 itemPosition)
    {
        int randomChance = Random.Range(0, 3);

        switch(randomChance)
        {
            case 0: 
                Instantiate(blastRadiusPrefab, itemPosition, Quaternion.identity);
                break;
            case 1: 
                Instantiate(extraBombPrefab, itemPosition, Quaternion.identity);
                break;
            case 2: 
                Instantiate(speedIncreasePrefabs, itemPosition, Quaternion.identity);
                break;
        }
    }

    public void RemoveAllGrid()
    {
        while(gridContainer.childCount > 0)
        {
            DestroyImmediate(gridContainer.GetChild(0).gameObject);
        }

        while(brickContainer.childCount > 0)
        {
            DestroyImmediate(brickContainer.GetChild(0).gameObject);
        }
    }

    public void AddObjectOnGridPosition(GridPosition gridPosition, LevelObject levelObject)
    {
        GridInfo gridInfo = gridSystem.GetGridInfo(gridPosition);
        gridInfo.AddObject(levelObject);
    }

    public void RemoveObjectOnGridPosition(GridPosition gridPosition)
    {
        GridInfo gridInfo = gridSystem.GetGridInfo(gridPosition);
        gridInfo.RemoveObject();
    }

    public LevelObject GetObjectOnGridPosition(GridPosition gridPosition)
    {
        GridInfo gridInfo = gridSystem.GetGridInfo(gridPosition);
        return gridInfo.GetObject();
    }

    public bool HasObjectOnGridPosition(GridPosition gridPosition)
    {
        GridInfo gridInfo = gridSystem.GetGridInfo(gridPosition);
        return gridInfo.HashObject();
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();
}
