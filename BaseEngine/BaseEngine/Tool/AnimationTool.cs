﻿using UnityEngine;
using System.Collections;
using BaseEngine;
/// <summary>
/// 动画控制器
/// </summary>
public sealed class AnimationTool : MetaScriptableHWQ
{

    private System.Action<AnimationClip> animationFinishEvent;//动画播放完成时间
    private AnimationState curState; // 当前动画
    private float animationTime; //
    private bool once; //
    private float animationSpeed;// 动画速度
    private string animationName;//动画名称
    private WrapMode warpMode;//循环模式
    private Animation _a;//动画组件
    private bool isPlay;//播放

    private AnimationTool()
    {

    }
    


    private void Awake()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="a"></param>
    public void Init(Animation a)
    {
        _a = a;
        _a.Stop();
    }

    /// <summary>
    /// 绑定动画结束事件
    /// </summary>
    /// <param name="e"></param>
    public void BindEvent(System.Action<AnimationClip> e)
    {
        animationFinishEvent = e;
    }

    private void PlayFinish(AnimationClip ac)
    {
        if (animationFinishEvent != null)
        {
            animationFinishEvent(ac);
        }
    }


    /// <summary>
    /// 验证时间
    /// </summary>
    /// <param name="percent"></param>
    /// <returns></returns>
    public bool CheckAnimationTime(float percent)
    {
        if (curState != null)
        {
            return animationTime > curState.length * percent;
        }
        return false;
    }

    /// <summary>
    /// 改变动画
    /// </summary>
    /// <param name="name">动画名称</param>
    /// <param name="wm">循环模式</param>
    /// <param name="speed">播放速度</param>
    /// <param name="resetTime">重置时间</param>
    public void ChangeAnimation(string name, WrapMode wm, float speed, bool resetTime)
    {
        if (_a == null)
            return;
        once = false;
        animationSpeed = speed;
        warpMode = wm;
        if (resetTime || (!string.IsNullOrEmpty(name) && !string.Equals(animationName, name)))
            animationTime = 0;
        if (_a[name] == null)
        {
            return;
        }
        animationName = name;
        _a.Play(animationName);
        _a.Stop();
        curState = _a[animationName];
    }

    /// <summary>
    /// 改变播放速度
    /// </summary>
    /// <param name="speed">速度</param>
    public void ChangePlaySpeed(float speed)
    {
        animationSpeed = speed;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    public void RePlay()
    {
        isPlay = true;
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void Pause()
    {
        isPlay = false;
    }

    /// <summary>
    /// 运行动画
    /// </summary>
    public void RunAnimation()
    {
        if (_a == null || curState == null)
        {
            return;
        }

        curState.enabled = true;

        animationTime += (isPlay ? Time.deltaTime : 0) * animationSpeed;
        curState.time = animationTime;
        _a.Sample();
        curState.enabled = false;
        switch (warpMode)
        {
            case WrapMode.Loop:
                if (curState.length <= curState.time)
                {
                    PlayFinish(curState.clip);
                    animationTime = 0;
                }
                break;
            case WrapMode.Default:
            case WrapMode.Once:
                if (!once && curState.length <= curState.time)
                {
                    animationTime = curState.length;
                    once = true;
                    PlayFinish(curState.clip);
                }
                break;
            case WrapMode.ClampForever:
                if (curState.length <= curState.time)
                    animationTime = curState.length;
                break;
                
        }

    }

    /// <summary>
    /// 获得当前动画长度
    /// </summary>
    /// <returns></returns>
    public float GetStateLength()
    {
        if (curState == null)
            return 0;
        return curState.length;
    }


    public static AnimationTool Create(Animation animation, System.Action<AnimationClip> e)
    {
        if (animation != null)
        {
            AnimationTool ao = Create();
            ao.Init(animation);
            ao.BindEvent(e);
            ao.RePlay();
            return ao;

        }
      
        return null;
    }

    public static AnimationTool Create()
    {
        return AnimationTool.CreateInstance<AnimationTool>();
    }
}
