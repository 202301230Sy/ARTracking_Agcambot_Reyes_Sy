using UnityEngine;

public class ObjectModificationView : ScreenView
{
    [SerializeField] private GameObject _colorButton1;
    [SerializeField] private GameObject _colorButton2;
    [SerializeField] private GameObject _colorButton3;
    //SerializeField] private GameObject _instructionText;

    public override void ShowView()
    {
        base.ShowView();

        _colorButton1.SetActive(true);
        _colorButton2.SetActive(true);
        _colorButton3.SetActive(true);
        //_instructionText.SetActive(true);
    }

    public override void UpdateView()
    {
        if (ModelInteractionManager.Instance == null) return;

        bool hasModel = ModelInteractionManager.Instance.CurrentSelected != null;

        _colorButton1.SetActive(hasModel);
        _colorButton2.SetActive(hasModel);
        _colorButton3.SetActive(hasModel);
    }

    public override void HideView()
    {
        base.HideView();
    }
}