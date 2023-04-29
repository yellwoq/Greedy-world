using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video2 : MonoBehaviour
{
    public string moviePath= "/奖励视频.mp4";
    private void Start()
    {
        Play();
    }
    private void Play()
    {
        // 获取VideoPlayer组件
        VideoPlayer mediaPlayer = GetComponentInChildren<VideoPlayer>();
        // 动态生成一个renderTexture，分别赋值给VideoPlayer和RewImage
        RenderTexture render = new RenderTexture(1920, 1080, 0);
        mediaPlayer.targetTexture = render;
        mediaPlayer.gameObject.GetComponent<RawImage>().texture = render;

        //// linux平台下，直接用读取视频的方式不行，需要用videoclip方式播放
        //// 其他平台直接读取本地MP4视频

       mediaPlayer.url = Application.streamingAssetsPath + "/" + moviePath;

        //// 由于视频和音频分开的，此处要加载视频对应的音频
        //object resourcesUtil = ResourcesUtil;
        //AudioClip clip = resourcesUtil.LoadAudio(movieMusicPath) as AudioClip;

        //// 播放完成事件
        mediaPlayer.loopPointReached += delegate (VideoPlayer _mediaPlayer)
        {
            _mediaPlayer.Stop();
            // 释放movietexture
            if (_mediaPlayer.targetTexture != null)
            {
                _mediaPlayer.targetTexture.Release();
            }
            //if (action != null)
            //{
            //    action();
            //}
        };

        // 开始播放事件
        mediaPlayer.prepareCompleted += delegate (VideoPlayer _mediaPlayer)
        {
            //AudioManager.PlayAudio(clip, screen);
            _mediaPlayer.Play();
        };

        // 用Prepare方式开始play,准备完成后调用视频播放和音频播放，以达到同步效果，
        // 如果直接播放，有可能视频加载时间和音频加载时长不一样，播放不同步
        mediaPlayer.Prepare();
    }
}
