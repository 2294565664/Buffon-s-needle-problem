using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedlePool 
{
    private Queue<GameObject> objPool = new Queue<GameObject>();
    /// <summary>
    /// 入池
    /// </summary>
    public void Enqueue(GameObject obj)
    {
        objPool.Enqueue(obj);
    }

    /// <summary>
    /// 出池
    /// </summary>
    public GameObject Dequeue()
    {
        if (objPool.Count == 0) return null;
        return objPool.Dequeue();
    }

    
//————————————————
//版权声明：本文为CSDN博主「林新发」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/linxinfa/article/details/119815354
}
