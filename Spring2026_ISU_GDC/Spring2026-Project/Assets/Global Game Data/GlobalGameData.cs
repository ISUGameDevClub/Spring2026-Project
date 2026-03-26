using UnityEngine;

/// <summary>
/// Global, read-only static class to allow any script to fetch data from the MasterGameData scriptable object.
/// All design relevant variables should be accessed from here
/// </summary>
public static class GlobalGameData
{
    private static GameData _data;

    public static GameData Data
    {
        get
        {
            if (_data == null)
                _data = Resources.Load<GameData>("MasterGameData");
            return _data;
        }
    }
}
