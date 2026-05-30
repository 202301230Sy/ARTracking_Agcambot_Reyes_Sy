using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class ModelInteractionManager : Singleton<ModelInteractionManager>
{
    [SerializeField] private GameObject _colorButtonsParent;
    [SerializeField] private Camera _arCamera;

    private IModelInteractable _currentSelected;

    public IModelInteractable CurrentSelected => _currentSelected;

    public bool IsObjectSelected { get; private set; } = false;

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
        if (Touch.activeTouches.Count == 0)
            return;

        var touch = Touch.activeTouches[0];

        Debug.Log("[Input] Touch detected");

        if (touch.phase != TouchPhase.Began)
            return;

        Ray ray = _arCamera.ScreenPointToRay(touch.screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"HIT: {hit.collider.name}");

            if (hit.collider.TryGetComponent(out IModelInteractable interactable))
            {
                Debug.Log("INTERACTABLE FOUND");

                SelectModel(interactable);
                return;
            }

            Debug.Log("NO INTERACTABLE FOUND");
        }

        DeselectCurrent();
    }

    public void SelectModel(IModelInteractable model)
    {
        Debug.Log("Model Selected");

        _currentSelected?.Deselect();

        _currentSelected = model;
        _currentSelected.Select();

        IsObjectSelected = true;

        if (_colorButtonsParent != null)
            _colorButtonsParent.SetActive(true);

        UIManager.Instance.ShowObjectModification();
    }

    public void DeselectCurrent()
    {
        _currentSelected?.Deselect();
        _currentSelected = null;

        IsObjectSelected = false;

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