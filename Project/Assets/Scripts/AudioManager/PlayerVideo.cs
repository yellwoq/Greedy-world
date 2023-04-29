using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerVideo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //图像
    public RawImage image;
    //播放器
    public VideoPlayer videoPlayer;
    //音频
    public AudioSource audioSource;
    //播放器资源
    public VideoClip videoToPlay;
    //路径
    public string url;
    //播放
    public Button btn_Play;
    //暂停
    public Button btn_Pause;
    //前进
    public Button btn_Fornt;
    //后退
    public Button btn_Back;
    //视频控制器
    public Slider sliderVideo;
    //音量控制器
    public Slider sliderSource;
    //音量大小
    public Text soundtext;
    //当前视频时间
    public Text text_Time;
    //视频总时长
    public Text text_Count;

    //需要添加播放器的物体
    public GameObject obj;
    //是否拿到视频总时长
    public bool isShow;
    //前进后退的大小
    public float numBer = 20f;
    //时 分的转换
    private int hour, mint;
    private float time;
    private float time_Count;
    private float time_Current;
    //视频是否播放完成
    private bool isVideo;

    public AudioClip clickClip;
    public Image Controller;
    void Start()
    {
        //唤醒时播放关闭
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        //暂停音频
        audioSource.Pause();
        //初始化
        Init(url);

        btn_Play.onClick.AddListener(delegate { OnClick(0); });
        btn_Pause.onClick.AddListener(delegate { OnClick(1); });
        btn_Fornt.onClick.AddListener(delegate { OnClick(2); });
        btn_Back.onClick.AddListener(delegate { OnClick(3); });

        sliderSource.value = audioSource.volume;
        soundtext.text = string.Format("{0:0}%", audioSource.volume * 100);
        sliderSource.onValueChanged.AddListener(delegate { ChangeSource(sliderSource.value); });
    }

    /// <summary>
    /// 初始化VideoPlayer
    /// </summary>
    /// <param name="videoClip">视频资源</param>
    private void Init(string url)
    {
        isVideo = true;
        isShow = true;
        time_Count = 0;
        time_Current = 0;
        sliderVideo.value = 0;
        //设置为VideoClip模式
        videoPlayer.source = VideoSource.Url;
        //将视频文件赋值给VideoPlayer
        videoPlayer.url = Application.streamingAssetsPath + "/" + url;
        //在视频中嵌入的音频类型
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //把声音组件赋值给VideoPlayer
        videoPlayer.SetTargetAudioSource(0, audioSource);
        //当VideoPlayer全部设置好的时候调用
        videoPlayer.prepareCompleted += Prepared;
        //启动播放器
        videoPlayer.Prepare();
    }

    /// <summary>
    ///     改变音量大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSource(float value)
    {
        audioSource.volume = value;
        soundtext.text = string.Format("{0:0}%", value * 100);
    }

    /// <summary>
    ///     改变视频进度
    /// </summary>
    /// <param name="value"></param>
    public void ChangeVideo(float value)
    {
        //如果准备成功
        if (videoPlayer.isPrepared)
        {
            videoPlayer.time = (long)value;
            Debug.Log("VideoPlayer Time:" + videoPlayer.time);
            time = (float)videoPlayer.time;
            hour = (int)time / 60;
            mint = (int)time % 60;
            text_Time.text = string.Format("{0:D2}:{1:D2}", hour.ToString(), mint.ToString());
        }
    }

    private void OnClick(int num)
    {
        switch (num)
        {
            //播放
            case 0:
                videoPlayer.Play();
                if (Mathf.Abs((int)videoPlayer.time - (int)sliderVideo.maxValue) == 0)
                { Init(url); }
                Time.timeScale = 1;
                break;
            //暂停
            case 1:
                videoPlayer.Pause();
                Time.timeScale = 0;
                break;
            //快进
            case 2:
                sliderVideo.value = sliderVideo.value + numBer;
                break;
            //放慢
            case 3:
                sliderVideo.value = sliderVideo.value - numBer;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        ////如果正在播放，初始化
        if (videoPlayer.isPlaying && isShow)
        {
            //帧数 / 帧速率 = 总时长    如果是本地直接赋值的视频，我们可以通过VideoClip.length获取总时长
            sliderVideo.maxValue = videoPlayer.frameCount / videoPlayer.frameRate;
            //sliderVideo.maxValue = (float)videoToPlay.length;
            time = sliderVideo.maxValue;
            hour = (int)time / 60;
            mint = (int)time % 60;
            text_Count.text = string.Format("/  {0:D2}:{1:D2}", hour.ToString(), mint.ToString());
            sliderVideo.onValueChanged.AddListener(delegate { ChangeVideo(sliderVideo.value); });
            isShow = !isShow;
        }
       

    }
    private void FixedUpdate()
    {
        //如果播放完毕
        if (Mathf.Abs((int)videoPlayer.time - (int)sliderVideo.maxValue) == 0)
        {
            videoPlayer.frame = (long)videoPlayer.frameCount;
            videoPlayer.Stop();
            Debug.Log("播放完成！");
            isVideo = false;
            return;
        }
        //没有播放完
        else if (isVideo && videoPlayer.isPlaying)
        {
            time_Count += Time.deltaTime;
            if ((time_Count - time_Current) >= 1)
            {
                //sliderVideo.value += 1;
            //Debug.Log("value:" + sliderVideo.value);
            time_Current = time_Count;
            }
        }
    }
    void Prepared(VideoPlayer vPlayer)
    {
        Debug.Log("准备播放视频！");
        ////把图像赋给RawImage
        image.texture = videoPlayer.texture;
        //播放
        vPlayer.Play();
    }

    public void OnClickReturnToRank()
    {
        AudioManager._instance.PlaySound(clickClip);
        SceneManager.LoadSceneAsync(2);
    }
    //IPointerEnterHandler接口的方法：当鼠标进入时显示
    public void OnPointerEnter(PointerEventData eventData)
    {
        Controller.gameObject.SetActive(true);
    }
    //PointerExitHandler接口的方法：当鼠标退出时显示
    public void OnPointerExit(PointerEventData eventData)
    {
        Controller.gameObject.SetActive(false);
    }
}
