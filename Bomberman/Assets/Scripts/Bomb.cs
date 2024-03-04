using Unity.Mathematics;
using UnityEngine;

public class Bomb : LevelObject
{
    [SerializeField] private Transform explosionStart;
    [SerializeField] private Transform explosionMiddle;
    [SerializeField] private Transform explosionEnd;

    [Header("Status")]
    [SerializeField] private PlayerController player;
    [SerializeField] private int maxPower;
    [SerializeField] private int timer = 3;
    [SerializeField] private float currentTime = 0;

    [Header("Collider")]
    [SerializeField] private CircleCollider2D physicCollider;
    [SerializeField] private CircleCollider2D eventCollider;

    private void Update() {
        currentTime += Time.deltaTime;
        if(currentTime >= timer) Destroy(gameObject);
    }

    private void OnDestroy() {
        LevelGrid.Instance.RemoveObjectOnGridPosition(gridPosition);
        player.DecreaseCurrentBomb();

        SpawnExplosion();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.CompareTag("Player")) return;
        physicCollider.enabled = true;
        eventCollider.enabled = false;
    }

    public void SetUp(PlayerController player, int maxPower)
    {
        this.player = player;
        this.maxPower = maxPower;
    }

    private void SpawnExplosion()
    {
        Instantiate(explosionStart, transform.position, Quaternion.identity);

        bool leftIsValid = true;
        bool rightIsValid = true;
        bool upIsValid = true;
        bool downIsValid = true;

        for(int i = 1; i <= maxPower; i++)
        {
            Transform prefab = explosionMiddle;

            if(i == maxPower) prefab = explosionEnd;

            GridPosition testGridPosition = new GridPosition(gridPosition.x + i, gridPosition.y);
            if(rightIsValid)
            {
                if(LevelGrid.Instance.IsValidGridPosition(testGridPosition) && !LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    InstantiateExplosion(prefab, testGridPosition, Quaternion.identity);
                }
                else if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    LevelObject levelObject = LevelGrid.Instance.GetObjectOnGridPosition(testGridPosition);
                    if(levelObject is Brick)
                    {
                        Brick brick = (Brick)levelObject;
                        brick.DestroyBrick();
                    }
                    rightIsValid = false;
                }
                else rightIsValid = false;
            }

            testGridPosition = new GridPosition(gridPosition.x - i, gridPosition.y); 
            if(leftIsValid)
            {
                if(LevelGrid.Instance.IsValidGridPosition(testGridPosition) && !LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    InstantiateExplosion(prefab, testGridPosition, Quaternion.Euler(0, 0, 180f));
                }
                else if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    LevelObject levelObject = LevelGrid.Instance.GetObjectOnGridPosition(testGridPosition);
                    if(levelObject is Brick)
                    {
                        Brick brick = (Brick)levelObject;
                        brick.DestroyBrick();
                    }
                    leftIsValid = false;
                }
                else leftIsValid = false;
            }

            testGridPosition = new GridPosition(gridPosition.x, gridPosition.y + i); 
            if(upIsValid)
            {
                if(LevelGrid.Instance.IsValidGridPosition(testGridPosition) && !LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    InstantiateExplosion(prefab, testGridPosition, Quaternion.Euler(0, 0, 90f));
                }
                else if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    LevelObject levelObject = LevelGrid.Instance.GetObjectOnGridPosition(testGridPosition);
                    if(levelObject is Brick)
                    {
                        Brick brick = (Brick)levelObject;
                        brick.DestroyBrick();
                    }
                    upIsValid = false;
                }
                else upIsValid = false;
            }

            testGridPosition = new GridPosition(gridPosition.x, gridPosition.y - i); 
            if(downIsValid)
            {
                if(LevelGrid.Instance.IsValidGridPosition(testGridPosition) && !LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    InstantiateExplosion(prefab, testGridPosition, Quaternion.Euler(0, 0, 270f));
                }
                else if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && LevelGrid.Instance.HasObjectOnGridPosition(testGridPosition))
                {
                    LevelObject levelObject = LevelGrid.Instance.GetObjectOnGridPosition(testGridPosition);
                    if(levelObject is Brick)
                    {
                        Brick brick = (Brick)levelObject;
                        brick.DestroyBrick();
                    }
                    downIsValid = false;
                }
                else downIsValid = false;
            }
        }
    }

    private void InstantiateExplosion(Transform prefab, GridPosition gridPosition, Quaternion rotation)
    {
        Vector3 position = LevelGrid.Instance.GetWorldPosition(gridPosition);
        Instantiate(prefab, position, rotation);
    }
}