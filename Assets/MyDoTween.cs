using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EaseState{
    Linear,
    Quard_easeIn,
    Quard_easeOut,
    Quard_easeInOut,
    Back_easeIn,
    Back_easeOut,
    Back_easeInOut,
    None
}
public class MyDoTween : MonoBehaviour
{
    private const float EPSINON = 0.000001f;
    private GameObject target;
    
    [Header("输入指定位置，规模，时间，指定Ease方式")]
    [Header("目标位置")]
    public Vector3 destination;
    [Header("指定规模")]
    public Vector3 targetScale;
    [Header("指定时间")]
    public float duratoin;


    [Header("指定kill,alpha1是KillMove,alph2是KillScale")] 
    public KeyCode killMoveKey=KeyCode.Alpha1;
    public KeyCode killScaleKey=KeyCode.Alpha2;
    
    public Coroutine moveRoutine;
    public Coroutine scaleRoutine;
    public EaseState curState;
    
    public void Attach(GameObject t)
    {
        this.target = t;
    }
    
    private void Awake()
    {
        Attach(this.gameObject);
    }
    public void Start()
    {
        //一开始就自动调用doMove 和 doKill
        moveRoutine  = StartCoroutine(DoMove(destination, duratoin,curState));
        scaleRoutine  = StartCoroutine(DoScale(targetScale, duratoin,curState));
    }

    //kill
    #region MyRegion    
    public void Update()
    {
        if (Input.GetKeyDown(killMoveKey))
        {
            killMove();
        }
        else if (Input.GetKeyDown(killScaleKey))
        {
            killScale();
        }
    }
    public void killMove()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }

    public void killScale()
    {
        if (scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
            scaleRoutine = null;
        }
    }

    public void KillDoMove()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }

    public void KillDoScale()
    {
        if (scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
            scaleRoutine = null;
        }
    }
    #endregion
    
    //Move&Kill
    #region MyRegion
    //扩展x，z的话自行扩展
    private IEnumerator DoMoveY(float destinationY,float dutation,EaseState es)
    {
        float beginTime = 0;
        float startY = transform.position.y;
        while (beginTime <=dutation)
        {
            beginTime += Time.deltaTime;
            switch (es)
            {
                case EaseState.Linear:
                    transform.position=new Vector3(transform.position.x,Linear(beginTime, startY, destinationY - startY,dutation),transform.position.z);
                    break;
               case EaseState.Quard_easeIn:
                   transform.position=new Vector3(transform.position.x,Quard_easeIn(beginTime, startY, destinationY - startY,dutation),transform.position.z);
                   break;
               case EaseState.Quard_easeOut:
                   transform.position=new Vector3(transform.position.x,Quard_easeOut(beginTime, startY, destinationY - startY,dutation),transform.position.z);
                   break;
               case EaseState.Quard_easeInOut:
                   transform.position=new Vector3(transform.position.x,Quard_easeInOut(beginTime, startY, destinationY - startY,dutation),transform.position.z);
                   break;
               case EaseState.Back_easeIn:
                   transform.position=new Vector3(transform.position.x,Back_easeIn(beginTime, startY, destinationY - startY,dutation,1),transform.position.z);
                   break;
                case EaseState.Back_easeOut:
                    transform.position=new Vector3(transform.position.x,Back_easeOut(beginTime, startY, destinationY - startY,dutation,1),transform.position.z);
                    break;
                case EaseState.Back_easeInOut:
                    transform.position=new Vector3(transform.position.x,Back_easeInOut(beginTime, startY, destinationY - startY,dutation,1),transform.position.z);
                    break;
            }
            yield return null;
        }
    }
    
    
    private IEnumerator DoMove(Vector3 destination,float dutation,EaseState es)
    {
        float beginTime = 0;
        float startX = transform.position.x;
        float startY = transform.position.y;
        float startZ = transform.position.z;
        while (beginTime <=dutation)
        {
            beginTime += Time.deltaTime;
            switch (es)
            {
                case EaseState.Linear:
                    transform.position=new Vector3(Linear(beginTime, startX, destination.x - startX,dutation),
                        Linear(beginTime, startY, destination.y - startY,dutation),
                        Linear(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeIn:
                    transform.position=new Vector3(Quard_easeIn(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeIn(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeIn(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeOut:
                    transform.position=new Vector3(Quard_easeOut(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeOut(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeOut(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeInOut:
                    transform.position=new Vector3(Quard_easeInOut(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeInOut(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeInOut(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Back_easeIn:
                    transform.position=new Vector3(Back_easeIn(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeIn(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeIn(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                case EaseState.Back_easeOut:
                    transform.position=new Vector3(Back_easeOut(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeOut(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeOut(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                case EaseState.Back_easeInOut:
                    transform.position=new Vector3(Back_easeInOut(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeInOut(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeInOut(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                default:
                    transform.position=new Vector3(Linear(beginTime, startX, destination.x - startX,dutation),
                        Linear(beginTime, startY, destination.y - startY,dutation),
                        Linear(beginTime, startZ, destination.z - startZ,dutation));
                    break;
            }
            yield return null;
        }
    }
    
   
    
    private IEnumerator DoScale(Vector3 destination,float dutation,EaseState es)
    {
        float beginTime = 0;
        float startX = transform.localScale.x;
        float startY = transform.localScale.y;
        float startZ = transform.localScale.z;
        while (beginTime <=dutation)
        {
            beginTime += Time.deltaTime;
            switch (es)
            {
                case EaseState.Linear:
                    transform.localScale=new Vector3(Linear(beginTime, startX, destination.x - startX,dutation),
                        Linear(beginTime, startY, destination.y - startY,dutation),
                        Linear(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeIn:
                    transform.localScale=new Vector3(Quard_easeIn(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeIn(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeIn(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeOut:
                    transform.localScale=new Vector3(Quard_easeOut(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeOut(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeOut(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Quard_easeInOut:
                    transform.localScale=new Vector3(Quard_easeInOut(beginTime, startX, destination.x - startX,dutation),
                        Quard_easeInOut(beginTime, startY, destination.y - startY,dutation),
                        Quard_easeInOut(beginTime, startZ, destination.z - startZ,dutation));
                    break;
                case EaseState.Back_easeOut:
                    transform.localScale=new Vector3(Back_easeOut(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeOut(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeInOut(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                case EaseState.Back_easeIn:
                    transform.localScale=new Vector3(Back_easeIn(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeIn(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeIn(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                case EaseState.Back_easeInOut:
                    transform.localScale=new Vector3(Back_easeInOut(beginTime, startX, destination.x - startX,dutation,1),
                        Back_easeInOut(beginTime, startY, destination.y - startY,dutation,1),
                        Back_easeInOut(beginTime, startZ, destination.z - startZ,dutation,1));
                    break;
                default:
                    transform.position=new Vector3(Linear(beginTime, startX, destination.x - startX,dutation),
                        Linear(beginTime, startY, destination.y - startY,dutation),
                        Linear(beginTime, startZ, destination.z - startZ,dutation));
                    break;
            }
            yield return null;
        }
    }
    #endregion
    
    
    public float myLerp(float start,float end,float weight)
    {
        return start + (end - start) * weight;
    }

    public float Linear(float t, float b, float c, float d)  
    {
        return c * t / d + b;
    }
    
    //t:current time 当前时间
    //b:beginning value 初始值
    //c：change invalue 变化量
    //d：duration 持续时间
    float  Quard_easeIn(float t, float b, float c, float d)
    {
        return c*(t/=d)*t + b;
    }
    float Quard_easeOut(float t, float b, float c, float d)
    {
        return -c *(t/=d)*(t-2) + b;
    }
    float Quard_easeInOut(float t, float b, float c, float d)
    {
        if ((t/=d/2) < 1) return c/2*t*t + b;
        return -c/2 * ((--t)*(t-2) - 1) + b;
    }

    bool EQUAL_ZERO(float s)
    {
        return s > -EPSINON && s < EPSINON;
    }
    
    float Back_easeIn(float t, float b, float c, float d, float s/* = 0.0f*/)
    {
        if (EQUAL_ZERO(s))
            s = 1.70158f;
        return c * (t /= d) * t * ((s + 1) * t - s) + b;
    }
    
    float Back_easeOut(float t, float b, float c, float d, float s/* = 0.0f*/)
    {
        if (EQUAL_ZERO(s))
            s = 1.70158f;
        return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
    }
 
    float Back_easeInOut(float t, float b, float c, float d, float s/* = 0.0f*/)
    {
        if (EQUAL_ZERO(s))
            s = 1.70158f;
        if ((t /= d / 2) < 1)
            return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
        return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
    }
   
    
}
