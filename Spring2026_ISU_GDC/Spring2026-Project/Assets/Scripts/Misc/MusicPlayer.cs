using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
using STOP_MODE = FMODUnity.STOP_MODE;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference musicEvent;

    public enum MusicTrack
    {
        MainMenu = 0,
        Tutorial = 1,
        Level1 = 2,
        Level2 = 3,
        Level3 = 4,
        FinalBoss = 5,
        Credits = 6,
        GameOver = 7
    }

    [SerializeField] private MusicTrack startingTrack = MusicTrack.MainMenu;
    [SerializeField] private bool playOnStart = true;

    private EventInstance musicInstance;
    
    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
    
    
    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);

        if (playOnStart)
        {
            musicInstance.start();
            SetTrack(startingTrack);
        }
    }

    public void SetTrack(MusicTrack track)
    {
        musicInstance.setParameterByName("MusicSwitch", (float)track);
    }

    public void StopMusicFromExternal()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}