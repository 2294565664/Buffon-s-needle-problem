using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedlePool 
{
    private Queue<GameObject> objPool = new Queue<GameObject>();
    /// <summary>
    /// ���
    /// </summary>
    public void Enqueue(GameObject obj)
    {
        objPool.Enqueue(obj);
    }

    /// <summary>
    /// ����
    /// </summary>
    public GameObject Dequeue()
    {
        if (objPool.Count == 0) return null;
        return objPool.Dequeue();
    }

    
//��������������������������������
//��Ȩ����������ΪCSDN���������·�����ԭ�����£���ѭCC 4.0 BY-SA��ȨЭ�飬ת���븽��ԭ�ĳ������Ӽ���������
//ԭ�����ӣ�https://blog.csdn.net/linxinfa/article/details/119815354
}
