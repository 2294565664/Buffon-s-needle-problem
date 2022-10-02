using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    /// <summary>
    /// ��ײ��ƽ���߻ص�
    /// </summary>
    public Action hitLineCb;
    /// <summary>
    /// ��ײ������ص�
    /// </summary>
    public Action hitPlaneCb;

    private Rigidbody rig;
    /// <summary>
    /// �ӳٻ���
    /// </summary>
    private float delayHideTimer;

    /// <summary>
    /// �Ƿ�������ƽ����
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
        // ������ƽ����
        if ("line" == collision.gameObject.tag)
        {
            isHitedLine = true;
            hitLineCb?.Invoke();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // ����������
        if ("plane" == collision.gameObject.tag)
        {
            // 1������
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
//��������������������������������
//��Ȩ����������ΪCSDN���������·�����ԭ�����£���ѭCC 4.0 BY-SA��ȨЭ�飬ת���븽��ԭ�ĳ������Ӽ���������
//ԭ�����ӣ�https://blog.csdn.net/linxinfa/article/details/119815354
}
