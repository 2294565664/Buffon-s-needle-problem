using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    /// <summary>
    /// 碰撞到平行线回调
    /// </summary>
    public Action hitLineCb;
    /// <summary>
    /// 碰撞到地面回调
    /// </summary>
    public Action hitPlaneCb;

    private Rigidbody rig;
    /// <summary>
    /// 延迟回收
    /// </summary>
    private float delayHideTimer;

    /// <summary>
    /// 是否碰到了平行线
    /// </summary>
    private bool isHitedLine = false;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        delayHideTimer = 0f;
        rig.isKinematic = false;
        isHitedLine = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (isHitedLine) return;
        // 碰到了平行线
        if ("line" == collision.gameObject.tag)
        {
            isHitedLine = true;
            hitLineCb?.Invoke();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 碰到了桌面
        if ("plane" == collision.gameObject.tag)
        {
            // 1秒后回收
            delayHideTimer = 1f;
        }
    }

    private void Update()
    {
        if (delayHideTimer > 0)
        {
            delayHideTimer -= Time.deltaTime;
            if (delayHideTimer <= 0)
            {
                hitPlaneCb?.Invoke();
            }
        }
    }
//————————————————
//版权声明：本文为CSDN博主「林新发」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/linxinfa/article/details/119815354
}
