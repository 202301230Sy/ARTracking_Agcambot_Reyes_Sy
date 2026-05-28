using UnityEngine;

public interface IModelInteractable
{
    void Select();
    void Deselect();
    void SetColor(Color color);
}