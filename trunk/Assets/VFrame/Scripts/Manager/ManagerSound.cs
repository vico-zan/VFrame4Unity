using UnityEngine;
using System.Collections;

public enum SoundType {
    dead_enemy,
    dead_player,
    hit,

    gamestart,
}

public enum MusicType {
    bg1,
}

public class ManagerSound : MonoBehaviour {
    public AudioClip[] m_audios;
    public AudioClip[] m_musics;
    private AudioSource m_sound;
    private AudioSource m_music;

    public static float SoundVolume;
    public static float MusicVolume;

    private static ManagerSound instance;
    public static ManagerSound Ins {
        get {
            if (!instance) {
                instance = (new GameObject("ManagerSound", typeof(ManagerSound))).GetComponent<ManagerSound>();
                instance.m_sound = instance.gameObject.AddComponent<AudioSource>();
                instance.m_music = instance.gameObject.AddComponent<AudioSource>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(this);

        //if (!PlayerPrefs.HasKey("SoundVolume"))
        //    PlayerPrefs.SetFloat("SoundVolume", 1f);
        //setSoundVolume(PlayerPrefs.GetFloat("SoundVolume"));
        //SoundVolume = PlayerPrefs.GetFloat("SoundVolume");

        //if (!PlayerPrefs.HasKey("MusicVolume"))
        //    PlayerPrefs.SetFloat("MusicVolume", 1f);
        //setMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        //MusicVolume = PlayerPrefs.GetFloat("MusicVolume");

    }

    void Start() {
        //		if (TGS_Saver.checkVaild()) {
        //			m_music.volume = TGS_Saver.getMusicVol();
        //			m_sound.volume = TGS_Saver.getSoundVol();
        //		}
        //		else {
        //			m_music.volume = 1;
        //			m_sound.volume = 1;
        //		}
    }

    public void PlaySound(int clipID) {
        if (clipID != 0)
            m_sound.PlayOneShot((AudioClip)Resources.Load("Sound/" + clipID));
    }

    public void PlaySound(string clip) {
        m_sound.PlayOneShot((AudioClip)Resources.Load("Sound/" + clip));
    }

    public void PlaySound(AudioClip clip) {
        m_sound.PlayOneShot(clip);
    }

    public void PlaySound(SoundType type) {
        m_sound.PlayOneShot(m_audios[(int)type]);
    }

    public void StopSound() {
        m_sound.Stop();
    }

    public void StopMusic() {
        m_music.Stop();
    }

    public void PlayMusic(string clip, bool loop = true) {
        if (loop) {
            m_music.clip = (AudioClip)Resources.Load("Sound/" + clip);
            m_music.loop = loop;
            m_music.Play();
        }
    }

    public void PlayMusic(int clipID, bool loop = true) {
        if (clipID != 0) {
            m_music.clip = (AudioClip)Resources.Load("Sound/" + clipID);
            m_music.loop = loop;
            m_music.Play();
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true) {
        m_music.clip = clip;
        m_music.loop = loop;
        m_music.Play();
    }

    public void PlayMusic(MusicType type, bool loop = true) {
        m_music.clip = m_musics[(int)type];
        m_music.loop = loop;
        m_music.Play();
    }

    public void setSoundVolume(float sound) {
        m_sound.volume = sound;
    }

    public void setMusicVolume(float music) {
        m_music.volume = music;
    }

    public float getSoundVolume() {
        return m_sound.volume;
    }

    public float getMusicVolume() {
        return m_music.volume;
    }

    public void attachTo(Transform dock) {
        if (dock != null) {
            transform.parent = dock;
            transform.localPosition = Vector3.zero;
        }
        else {
            transform.parent = null;
            transform.localPosition = Vector3.zero;
        }
    }

}