using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundModule : MonoBehaviour
{
    [HideInInspector]
    public AudioSource source;

    private List<int> ShuffleList;
    private int LastPlayed = 0;
    private int clipToPlay = 0;
    private int loopCounter = 0;
    //private float loopTimer = 0f;
    private bool cancelLoop = false;

    [Header("Add Audio Clips")]
    public AudioClip[] clips;
    [Header("Multiple Clip Instances")]
    public bool OneShot = true;
    [Header("Playback Type")]
    public PlayType playType = PlayType.RAND;
    [Header("Loop Settings")]
    [Tooltip("Don't use LOOP_SOUND with OneShot active")]
    public LoopMode loopMode = LoopMode.NONE;
    
    [Tooltip("Setting NumLoops to 0 = infinity")]
    public int NumLoops = 0;
    [Tooltip("When LOOP_MODULE is selected sets min & max silence between triggering clips")]
    public float MinSilence = 0;
    [Tooltip("When LOOP_MODULE is selected sets min & max silence between triggering clips")]
    public float MaxSilence = 0;

    [HideInInspector]
    //public float volume = 0.75f;

    public enum PlayType 
    {
        RAND,
        SHUFFLE,
        SEQ,
    }

    public enum LoopMode 
    {
        NONE,
        LOOP_MODULE,
        LOOP_SOUND,
    }

    void Start()
    {
        if (GetComponent<AudioSource>() == null) gameObject.AddComponent<AudioSource>();
        
        source = GetComponent<AudioSource>();
        ShuffleList = new List<int>();

        //Initialize
        LoadAllClips();
        ResetShuffleList();
    }

    void Update()
    {
        
    }

    //Loop sound
    //Loop module
    //Pause

    //Wrapper Functions-------------------------------------------------------

    private float PlaySound() 
    {
        if (clips.Length <= 0) 
        {
            Debug.Log("Error: No clips added to module");
            throw new System.Exception("Error: No clips added to module");
        }

        if(clips.Length == 1) 
        {
            if (OneShot)
            {
                source.PlayOneShot(clips[0]); //took away volume param
                return clips[0].length;
            }
            else
            {
                source.clip = clips[0];
                //source.volume = volume;
                source.Play();
                return clips[0].length;
            }
        }

        switch (playType)
        {
            case PlayType.RAND:
                clipToPlay = Random.Range(0, clips.Length);
                if (OneShot)
                {
                    source.PlayOneShot(clips[clipToPlay]); //took away volume param
                    return clips[clipToPlay].length;
                }
                else
                {
                    source.clip = clips[clipToPlay];
                    //source.volume = volume;
                    source.Play();
                    return clips[clipToPlay].length;
                }

            case PlayType.SHUFFLE:
                Debug.Log(ShuffleList.Count);
                clipToPlay = ShuffleList[Random.Range(0, ShuffleList.Count-1)];
                Debug.Log("num " + clipToPlay);
                RemoveShuffleClip(clipToPlay);
                if (OneShot)
                {
                    source.PlayOneShot(clips[clipToPlay]);  //took away volume param
                    return clips[clipToPlay].length;
                }
                else
                {
                    source.clip = clips[clipToPlay];
                    //source.volume = volume;
                    source.Play();
                    return clips[clipToPlay].length;
                }

            case PlayType.SEQ:
                if (OneShot)
                {
                    source.PlayOneShot(clips[LastPlayed]); //took away volume param
                    NextInSeq();
                    return clips[LastPlayed].length;
                }
                else
                {
                    source.clip = clips[LastPlayed];
                    //source.volume = volume;
                    source.Play();
                    NextInSeq();
                    return clips[LastPlayed].length;
                }
        }
        return 0f; //Should never happen
    }

    public void PlayModule() 
    {
        if (loopMode == LoopMode.NONE) PlaySound();
        else if(loopMode == LoopMode.LOOP_SOUND) 
        {
            if (OneShot) throw new System.Exception("You cannot use LOOP_SOUND with OneShot active");
            //Loop the sound that gets played
            GetComponent<AudioSource>().loop = true;
            float length = PlaySound();
            if(NumLoops > 0) Invoke("LoopTimer", length * NumLoops);
        }
        else
        {
            cancelLoop = false;
            PlayLoopModule();
        }
    }

    public void StopModule() 
    {
        //Add fade param
        source.loop = false;
        cancelLoop = true;
        loopCounter = 0;
        CancelInvoke();
        source.Stop();
    }

    public void PlayLoopModule() 
    {
        if ((NumLoops == 0 || loopCounter < NumLoops) && !cancelLoop)
        {
            float length = PlaySound();
            float silence = 0f;
            if (MinSilence > 0f && MaxSilence > 0f) silence = Random.Range(MinSilence, MaxSilence);
            else if (MinSilence > 0f) silence = MinSilence;
            else if (MaxSilence > 0f) silence = MaxSilence;
            Invoke("PlayLoopModule", length + silence);
            loopCounter++;
        }
        else loopCounter = 0;
        //Debug.Log(loopCounter);
    }


    //Internal Logic--------------------------------------------------

    void ResetShuffleList() 
    {
        ShuffleList.Clear();
        for(int i = 0; i < clips.Length; i++) 
        {
            ShuffleList.Add(i);
        }
    }
    void RemoveShuffleClip(int _remove) 
    {
        if (ShuffleList.Count > 1) ShuffleList.Remove(_remove);
        else ResetShuffleList();
    }

    int NextInSeq() 
    {
        LastPlayed++;
        if (LastPlayed >= clips.Length) LastPlayed = 0;
        
        return LastPlayed;
    }


    private void LoopTimer() 
    {
        //Add fade
        source.Stop();
    }


    private void LoadAllClips() 
    {
        foreach(AudioClip _clip in clips) 
        {
            if (_clip.loadState != AudioDataLoadState.Loaded) _clip.LoadAudioData();
        }
    }

    //Unload?

}


//Detect end of clip
//https://forum.unity.com/threads/detecting-end-of-audio-clip.60897/

//On Filter Read (Generative Metro)
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnAudioFilterRead.html