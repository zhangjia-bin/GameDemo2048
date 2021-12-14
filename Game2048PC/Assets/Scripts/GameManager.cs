using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    private Text[,] Texts;
    public Text Score;
    public Text EndGame;
    public Text TimeEnd;
    public GameObject GameOver;
    private Dictionary<int, string> ShowTexts;
    Core core;
    private float time = 60;

    public Button ResGame;

    public VideoPlayer PlayervVideo;
    public List<VideoClip> listVideo = new List<VideoClip>();
    void Start()
    {
        ResGame.onClick.AddListener(() =>
        {
            //刷新所有
            InitGame();
            GameOver.transform.localPosition = new Vector3(768, 0, 0);
            Time.timeScale = 1;
            time = 60;

            //暂停视频
            PlayervVideo.Pause();
        });
        InitGame();
    }

    private void InitGame()
    {
        Texts = new Text[4, 4];
        for (int i = 0; i < transform.childCount; i++)
        {
            Texts[i / 4, i % 4] = transform.GetChild(i).GetComponentInChildren<Text>();
        }

        ShowTexts = new Dictionary<int, string>();
        core = new Core(4, 4);

        //刷星界面
        RefreshView();
    }

    private List<Color> colors = new List<Color>() {Color.red,Color.yellow,Color.blue,Color.green,
        Color.cyan,Color.grey,Color.gray,new Color(125,125,0),
        new Color(234,85,32), new Color(237,109,0), new Color(227,204,169), new Color(34,174,230),
        new Color(196,215,0), new Color(21,174,103), new Color(194,115,127), new Color(0,128,119),
    };
    void RefreshView()
    {
        //2 4 8 16 32 64 128 256 512 1024 2048
        for (int i = 0; i < Texts.GetLength(0); i++)
        {
            for (int j = 0; j < Texts.GetLength(1); j++)
            {

                MatchTheColor(Texts[i, j], core.GetValue(i, j));
                Texts[i, j].text = core.GetValue(i, j).ToString();
            }
        }
    }

    private void MatchTheColor(Text text,int shu)
    {
        //基础颜色
        if (shu==0)
        {
            text.color = Color.black;
        }
        if (shu==2)
        {
            text.color = colors[0];
        }else if (shu==4)
        {
            text.color = colors[1];
        }else if (shu==8)
        {
            text.color = colors[2];
        }else if (shu==16)
        {
            text.color = colors[3];
        }else if (shu==32)
        {
            text.color = colors[4];
        }else if (shu==64)
        {
            text.color = colors[5];
        }else if (shu==128)
        {
            text.color = colors[6];
        }else if (shu==256)
        {
            text.color = colors[7];
        }else if (shu==512)
        {
            text.color = colors[8];
        }else if (shu==1024)
        {
            text.color = colors[9];
        }else if (shu==2048)
        {
            text.color = colors[10];
        }else if (shu==4096)
        {
            text.color = colors[11];
        }
    }

    //临时位置
    private Vector2 dragBeginPostion;
    // Update is called once per frame
    void Update()
    {
        //判断输赢
        GetResult();

        //滑动屏幕输入
        InPutMove();

        //按键输入
        InputKeyDown();

        
    }
    //判断输赢条件
    void GetResult()
    {
        time -= Time.deltaTime;
        TimeEnd.text = "距离游戏结束还剩" + Math.Round(time, 0)+"秒";
        if (time<=0)
        {
            GameOver.transform.localPosition = Vector3.zero;
            EndGame.text = "你这个菜鸡，祝艳丽你是真的菜！！\r\n,你最后的成绩是" + core.GetSocre();
            Time.timeScale = 0;
            PlayerVideo();
        }
        //不停地调用！！输入啦
        if (core.IsGameOver())
        {
            GameOver.transform.localPosition = Vector3.zero;
            EndGame.text = "你这个菜鸡，祝艳丽你是真的菜！！\r\n,你最后的成绩是" + core.GetSocre();
            Time.timeScale = 0;
            PlayerVideo();
        }
        //检测到游戏赢了
        if (core.GetWin())
        {
            GameOver.transform.localPosition = Vector3.zero;
            EndGame.text = "可以呀,赢啦，本游戏由章佳彬制作，\r\n" + "你最后的成绩是" + core.GetSocre();
            Time.timeScale = 0;
            PlayerVideo();
        }
    }

    private int index = 0;
    void PlayerVideo()
    {
        ItemMusic.Instacne.item.Pause();
        PlayervVideo.clip=listVideo[index];
        PlayervVideo.Play();
        if ((long)PlayervVideo.frameCount- PlayervVideo.frame<=5)
        {
            index++;
            PlayervVideo.clip = listVideo[index];
            if (index>= listVideo.Count-1)
            {
                index = 0;
            }

        }
    }
    void InPutMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragBeginPostion = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPostion = Input.mousePosition;
            var delta_y = endPostion.y - dragBeginPostion.y;
            var delta_x = endPostion.x - dragBeginPostion.x;
            if (Mathf.Abs(delta_x) < 50 && Mathf.Abs(delta_y) < 50)
                return;

            if (Mathf.Abs(delta_x) > Mathf.Abs(delta_y))
            {
                if (delta_x > 0)
                {
                    core.Move(Director.RIGHT);

                }

                else
                {
                    core.Move(Director.LEFT);
                }
                RefreshView();
                Score.text = core.GetSocre().ToString();
            }
            else
            {
                if (delta_y > 0)
                {
                    core.Move(Director.UP);
                }
                else
                {
                    core.Move(Director.UP);
                }
                RefreshView();
                Score.text = core.GetSocre().ToString();
            }
        }
    }

    private void InputKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            core.Move(Director.LEFT);
            RefreshView();
            Score.text = core.GetSocre().ToString();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            core.Move(Director.RIGHT);
            RefreshView();
            Score.text = core.GetSocre().ToString();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            core.Move(Director.UP);
            RefreshView();
            Score.text = core.GetSocre().ToString();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            core.Move(Director.DOWN);
            RefreshView();
            Score.text = core.GetSocre().ToString();
        }
    }
}
