using System;
using Steamworks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Integrations.Steam
{
  public class SteamManager : MonoBehaviour
  {
    public bool IsInitialized { get; private set; }

    [field: Header("Settings")]
    [field: SerializeField]
    public uint AppId { get; private set; }

    [field: Tooltip("Restart the app if not launched through Steam.")]
    [field: SerializeField]
    public bool RestartAppIfNecessary { get; private set; } = true;

    private void Awake()
    {
	    ValidateAppId();

      PerformPacksizeTest();
      PerformDllCheck();

      if (RestartAppIfSteamIsNotRunning())
        return;

      InitializeSteamApi();
    }

    private void Update()
    {
      if (!IsInitialized)
      {
        return;
      }

      SteamAPI.RunCallbacks();
    }

    private void OnDestroy()
    {
      if (!IsInitialized)
      {
        return;
      }

      SteamAPI.Shutdown();
    }

    private void InitializeSteamApi()
    {
      IsInitialized = SteamAPI.Init();

      if (!IsInitialized)
      {
        Debug.LogError(
          "SteamAPI_Init() failed! This can have several reason, please consult the API docs first: https://partner.steamgames.com/doc/sdk/api#initialization_and_shutdown\n\n" +
          "* The Steam client isn't running. A running Steam client is required to provide implementations of the various Steamworks interfaces.\n" +
          "* The Steam client couldn't determine the App ID of game. If you're running your application from the executable or debugger directly then you must have a steam_appid.txt in your game directory next to the executable, with your app ID in it and nothing else. Steam will look for this file in the current working directory. If you are running your executable from a different directory you may need to relocate the steam_appid.txt file.\n" +
          "* Your application is not running under the same OS user context as the Steam client, such as a different user or administration access level.\n" +
          "* Ensure that you own a license for the App ID on the currently active Steam account. Your game must show up in your Steam library.\n" +
          "* Your App ID is not completely set up, i.e. in Release State: Unavailable, or it's missing default packages.",
          this);
      }
    }

    private bool RestartAppIfSteamIsNotRunning()
    {
      try
      {
        if (SteamAPI.RestartAppIfNecessary(new(AppId)))
        {
          Application.Quit();
        }
      }
      catch (DllNotFoundException dllNotFoundException)
      {
        Debug.LogError(
          "Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to https://github.com/rlabrecque/Steamworks.NET/blob/master/README.md for more details.",
          this);
        Debug.LogException(dllNotFoundException, this);

        Application.Quit();
        return true;
      }
      return false;
    }

    private void PerformDllCheck()
    {
      if (!DllCheck.Test())
      {
        Debug.LogError("DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
      }
    }

    private void PerformPacksizeTest()
    {
      if (!Packsize.Test())
      {
        Debug.LogError("Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
      }
    }

    private void ValidateAppId()
    {
      if (AppId == 0)
      {
        throw new ArgumentOutOfRangeException(nameof(AppId), AppId, $"{nameof(AppId)} is not set.");
      }
    }
  }
}
