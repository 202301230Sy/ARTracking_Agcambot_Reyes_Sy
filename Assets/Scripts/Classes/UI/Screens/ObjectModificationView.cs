using UnityEngine;

public class ObjectModificationView : ScreenView
{
    [SerializeField] private GameObject _colorButtons;
    [SerializeField] private GameObject _instructionText;

    public override void ShowView()
    {
        base.ShowView();

        _colorButtons.SetActive(false);
        _instructionText.SetActive(true);
    }

    public override void UpdateView()
    {
        if (ModelInteractionManager.Instance == null) return;

        bool hasModel = ModelInteractionManager.Instance.CurrentSelected != null;

        _colorButtons.SetActive(hasModel);
        _instructionText.SetActive(!hasModel);
    }

    public override void HideView()
    {
        base.HideView();
    }
}