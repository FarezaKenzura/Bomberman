using UnityEngine;

public class DestroyedBrick : LevelObject
{
    public void DestroyBrick()
    {
        LevelGrid.Instance.RemoveObjectOnGridPosition(gridPosition);
        SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        int chance = Random.Range(0, 100);
        if (chance < 10) LevelGrid.Instance.SpawnRandomItem(transform.position);
    }
}
