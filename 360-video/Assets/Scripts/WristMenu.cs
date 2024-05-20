using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class WristMenu : MonoBehaviour
{
    // Video
    public VideoPlayer videoGameObj;

    public VideoClip firstClip;
    public VideoClip secondClip;
    public VideoClip thirdClip;

    public VideoClip currentClip;

    // UI
    public TMP_Text playbackText;
    public TMP_Text loopText;

    // Playback speed & rewind and fast-forth
    private float previousSpeed;
    private float skipTime = 10f;

    void Start()
    {
        videoGameObj.clip = firstClip;
        currentClip = videoGameObj.clip;
        previousSpeed = videoGameObj.playbackSpeed;

        videoGameObj.loopPointReached += OnVideoFinished;
    }

    void OnDestroy()
    {
        videoGameObj.loopPointReached -= OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (!videoGameObj.isLooping)
        {
            if (videoGameObj.clip == firstClip)
            {
                videoGameObj.clip = secondClip;
            }
            else if (videoGameObj.clip == secondClip)
            {
                videoGameObj.clip = thirdClip;
            }
            else if (videoGameObj.clip == thirdClip)
            {
                videoGameObj.clip = firstClip;
            }
            currentClip = videoGameObj.clip;
            videoGameObj.Play();
        }
        else
        {
            videoGameObj.time = 0;
        }
    }

    // Pause current video
    public void Pause()
    {
        if (videoGameObj.playbackSpeed > 0)
        {
            previousSpeed = videoGameObj.playbackSpeed;
            videoGameObj.playbackSpeed = 0;
        }
        else if (videoGameObj.playbackSpeed == 0)
        {
            videoGameObj.playbackSpeed = previousSpeed;
        }
    }

    // Loop video
    public void Loop()
    {
        if (videoGameObj.isLooping == false)
        {
            loopText.text = "ON";
            videoGameObj.isLooping = true;
        }
        else if (videoGameObj.isLooping == true)
        {
            loopText.text = "OFF";
            videoGameObj.isLooping = false;
        }
    }

    // Ups the video speed by 0.25s, works while paused
    public void PlayBackSpeed()
    {
        // When video is paused
        if (videoGameObj.playbackSpeed == 0)
        {
            if (previousSpeed >= 2)
            {
                previousSpeed = 0.25f;
            }
            else
            {
                previousSpeed += 0.25f;
            }

            videoGameObj.playbackSpeed = previousSpeed;
        }
        // When video is playing
        else
        {
            if (videoGameObj.playbackSpeed >= 2)
            {
                videoGameObj.playbackSpeed = 0.25f;
            }
            else
            {
                videoGameObj.playbackSpeed += 0.25f;
            }

            previousSpeed = videoGameObj.playbackSpeed;
        }

        if (videoGameObj.playbackSpeed % 1 == 0)
        {
            playbackText.text = videoGameObj.playbackSpeed.ToString("0") + "x";
        }
        else
        {
            playbackText.text = videoGameObj.playbackSpeed.ToString("0.00") + "x";
        }
    }

    // Rewind or fast-forward 10 sec of video
    public void Forward()
    {
        Debug.Log("Pressed Forward");
        if (videoGameObj.time + skipTime < videoGameObj.length)
        {
            videoGameObj.time += skipTime;
            Debug.Log("Works forward");
        }
        else
        {
            videoGameObj.time = videoGameObj.length;
            Debug.Log("Works forward x2");
        }
    }

    public void Rewind()
    {
        Debug.Log("Pressed Rewind");
        if (videoGameObj.time - skipTime > 0)
        {
            videoGameObj.time -= skipTime;
            Debug.Log("Works rewind");
        }
        else
        {
            videoGameObj.time = 0;
            Debug.Log("Works rewind x2");
        }
    }

    // Skip to next video or previous video
    public void PreviousTrack()
    {
        Debug.Log("Last video");
        if (videoGameObj.clip == firstClip)
        {
            videoGameObj.clip = thirdClip;
        }
        else if (videoGameObj.clip == secondClip)
        {
            videoGameObj.clip = firstClip;
        }
        else if (videoGameObj.clip == thirdClip)
        {
            videoGameObj.clip = secondClip;
        }
        currentClip = videoGameObj.clip;
    }

    public void NextTrack()
    {
        Debug.Log("Next video");
        if (videoGameObj.clip == firstClip)
        {
            videoGameObj.clip = secondClip;
        }
        else if (videoGameObj.clip == secondClip)
        {
            videoGameObj.clip = thirdClip;
        }
        else if (videoGameObj.clip == thirdClip)
        {
            videoGameObj.clip = firstClip;
        }
        currentClip = videoGameObj.clip;
    }
}
