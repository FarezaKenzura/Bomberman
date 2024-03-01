using UnityEngine;

public class LevelObject : MonoBehaviour 
{
    protected GridPosition gridPosition;

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if(!LevelGrid.Instance.IsValidGridPosition(gridPosition)) return;
        LevelGrid.Instance.AddObjectOnGridPosition(gridPosition, this);
    }   
}