using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text totalCntText;
    public Text hitedCntText;
    public Text resultText;

    public GameObject needleObj;
    public Button startBtn;
    public Text btnStateText;

    public Transform Needlepool;


    private int totalCnt;
    private int hitedCnt;
    private float timer;
    private bool isStarted = false;

    // 对象池
    private NeedlePool pool = new NeedlePool();

    private void Awake()
    {
        needleObj.SetActive(false);
    }

    private void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            isStarted = !isStarted;
            btnStateText.text = isStarted ? "停止" : "开始";
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 鼠标点击，生成针
            GenNeedle();
            UpdateText();
        }

        if (isStarted)
        {
            // 自动生成针
            timer += Time.deltaTime;
            if (timer > 0.03f)
            {
                // 提高丢针数量，加快速度
                for (int i = 0; i < 7; ++i)
                {
                    GenNeedle();
                }
                UpdateText();
                timer = 0;
            }
        }
    }

    /// <summary>
    /// 生成针
    /// </summary>
    private void GenNeedle()
    {
        ++totalCnt;
        var obj = pool.Dequeue();
        if (null == obj)
            obj = Instantiate(needleObj);
        var x = Random.Range(-100f, 100f);
        var y = 0.1f;
        var z = Random.Range(-60f, 60f);
        obj.transform.position = new Vector3(x, y, z);
        obj.transform.rotation = Quaternion.Euler(new Vector3(90, Random.Range(-360f, 360f), 0));
        obj.SetActive(true);
        obj.transform.SetParent(Needlepool);
        var needle = obj.GetComponent<Needle>();
        needle.hitLineCb = () =>
        {
            ++hitedCnt;
            UpdateText();
        };
        needle.hitPlaneCb = () =>
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        };
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    private void UpdateText()
    {
        totalCntText.text = totalCnt.ToString();
        hitedCntText.text = hitedCnt.ToString();
        if (0 != hitedCnt)
            resultText.text = ((float)totalCnt / hitedCnt).ToString("#0.00000");
    }
//————————————————
//版权声明：本文为CSDN博主「林新发」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/linxinfa/article/details/119815354
}
