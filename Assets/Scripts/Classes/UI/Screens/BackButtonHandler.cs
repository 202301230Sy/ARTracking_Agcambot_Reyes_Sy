using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BackButtonHandler : MonoBehaviour
{
    [SerializeField] private ARSession _session;
    //[SerializeField] private GameObject _spawnRoot;

    public void GoBack()
    {
        UIManager.Instance.ShowModeSelection();

        ModelInteractionManager.Instance.ResetInteraction();

        _session.Reset();

        ARTrackingManager.Instance.SetMode(ARTrackingMode.None);
    }
}