using UnityEngine;
using UnityEngine.EventSystems;

public class ModelInteractionManager : Singleton<ModelInteractionManager>
{
    [SerializeField] private GameObject _colorButtonsParent;
    [SerializeField] private Camera _arCamera;

    private IModelInteractable _currentSelected;

    public IModelInteractable CurrentSelected => _currentSelected;

    private void Start()
    {
        if (_colorButtonsParent != null)
            _colorButtonsParent.SetActive(false);
    }

    private void Update()
    {
        HandleTouch();
    }

    private void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        var touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        Ray ray = _arCamera.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out IModelInteractable interactable))
            {
                SelectModel(interactable);
                return;
            }
        }

        DeselectCurrent();
    }

    public void SelectModel(IModelInteractable model)
    {
        _currentSelected?.Deselect();

        _currentSelected = model;
        _currentSelected.Select();

        if (_colorButtonsParent != null)
            _colorButtonsParent.SetActive(true);

        UIManager.Instance.ShowObjectModification();
    }

    public void DeselectCurrent()
    {
        _currentSelected?.Deselect();
        _currentSelected = null;

        if (_colorButtonsParent != null)
            _colorButtonsParent.SetActive(false);
    }

    public void SetRed() => _currentSelected?.SetColor(Color.red);
    public void SetGreen() => _currentSelected?.SetColor(Color.green);
    public void SetBlue() => _currentSelected?.SetColor(Color.blue);

    public void ResetInteraction()
    {
        DeselectCurrent();
    }

    public bool HasSelection() => _currentSelected != null;
}