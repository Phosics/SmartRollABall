using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public string playOnStart = "Theme";

    private static AudioManager _instance;
    private static Sound[] _allSounds;

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        _allSounds = sounds;
    }

    private void Start()
    {
        Play(playOnStart);
    }

    // Update is called once per frame
    public static void Play(string name)
    {
        var s = _allSounds.SingleOrDefault(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Error: Sound " + name + " not found");
            return;
        }
        
        s.source.Play();
    }

    public static void Pause(string name)
    {
        var s = _allSounds.SingleOrDefault(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Error: Sound " + name + " not found");
            return;
        }

        s.source.Pause();
    }
    
    public static void UnPause(string name)
    {
        var s = _allSounds.SingleOrDefault(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Error: Sound " + name + " not found");
            return;
        }

        s.source.UnPause();
    }
}