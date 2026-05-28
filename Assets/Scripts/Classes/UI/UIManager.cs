using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public IScreenView CurrentScreen => _currentScreen;

    [SerializeField]
    private List<ScreenView> _allScreens;

    private readonly Dictionary<UIScreenType, IScreenView> _screens = new();
    private IScreenView _currentScreen;

    protected override void Awake()
    {
        base.Awake();

        foreach (var screenView in _allScreens)
        {
            _screens.TryAdd(screenView.ScreenType, screenView);

            // idk if there was a better way to go about this
            if (screenView.ScreenType != UIScreenType.ModeSelection)
                screenView.HideView();
        }

        ShowScreen(UIScreenType.ModeSelection);
    }

    public void ShowScreen(UIScreenType targetScreenType)
    {
        Debug.Log("Switching to: " + targetScreenType);

        // hide all screens first
        foreach (var screen in _screens.Values)
        {
            screen.HideView();
        }

        if (_screens.TryGetValue(targetScreenType, out IScreenView targetScreen))
        {
            _currentScreen = targetScreen;
            _currentScreen.ShowView();
        }
    }

    public void HideScreen(UIScreenType screenType)
    {
        if (_screens.TryGetValue(screenType, out IScreenView targetScreen))
        {
            _currentScreen = targetScreen;
            _currentScreen.HideView();
        }
    }

    // button functions
    public void ShowPlaneTracking()
    {
        ShowScreen(UIScreenType.PlaneTracking);
        ARTrackingManager.Instance.SetMode(ARTrackingMode.PlaneTracking);
    }

    public void ShowImageTracking()
    {
        ShowScreen(UIScreenType.ImageTracking);
        ARTrackingManager.Instance.SetMode(ARTrackingMode.ImageTracking);
    }

    public void ShowModeSelection()
    {
        ShowScreen(UIScreenType.ModeSelection);

        // idk if its better to create a new enum or just leave it as none
        ARTrackingManager.Instance.SetMode(ARTrackingMode.None);
    }

    public void ShowObjectModification()
    {
        ShowScreen(UIScreenType.ObjectModification);
    }
}
