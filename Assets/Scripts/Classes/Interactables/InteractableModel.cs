using UnityEngine;

public class InteractableModel : MonoBehaviour, IModelInteractable
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void Select()
    {
        // nothing
    }

    public void Deselect()
    {
        // nothing
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}