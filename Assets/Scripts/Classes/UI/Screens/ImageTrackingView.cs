using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingView : ScreenView
{
    [SerializeField] 
    private GameObject _scanInstruction;
    [SerializeField] 
    private GameObject _imageDetectedInstruction;
    [SerializeField] 
    private ARTrackedImageManager _imageManager;

    private bool _detected;

    public override void ShowView()
    {
        base.ShowView();

        _detected = false;

        _scanInstruction.SetActive(true);
        _imageDetectedInstruction.SetActive(false);
    }

    public override void UpdateView()
    {
        if (_imageManager == null) return;

        foreach (var img in _imageManager.trackables)
        {
            if (img.trackingState == TrackingState.Tracking)
            {
                _detected = true;
                break;
            }
        }

        if (_detected)
        {
            _scanInstruction.SetActive(false);
            _imageDetectedInstruction.SetActive(true);
        }
    }

    public override void HideView()
    {
        base .HideView();
    }
}