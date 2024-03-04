using UnityEngine;

public class Brick : LevelObject 
{
    [SerializeField] private Transform destroyedBrick;

    public void DestroyBrick()
    {
        Instantiate(destroyedBrick, transform.position, Quaternion.identity);
        LevelGrid.Instance.RemoveObjectOnGridPosition(gridPosition);
        Destroy(gameObject);
    }    
}