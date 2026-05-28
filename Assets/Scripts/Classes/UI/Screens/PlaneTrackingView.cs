using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneTrackingView : ScreenView
{
    [SerializeField]
    private GameObject _moveAroundInstructions;
    [SerializeField]
    private GameObject _tapToSpawnInstructions;
    [SerializeField]
    private ARPlaneManager _planeManager;

    public override void ShowView()
    {
        base.ShowView();

        _moveAroundInstructions.SetActive(true);
        _tapToSpawnInstructions.SetActive(false);
    }

    public override void UpdateView()
    {
        if (_planeManager == null) return;

        bool hasPlane = _planeManager.trackables.count > 0;

        _moveAroundInstructions.SetActive(!hasPlane);
        _tapToSpawnInstructions.SetActive(hasPlane);
    }

    public override void HideView()
    {
        base.HideView();
    }
}
