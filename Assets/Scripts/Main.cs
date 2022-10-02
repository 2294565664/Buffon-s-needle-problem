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

    // �����
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
            btnStateText.text = isStarted ? "ֹͣ" : "��ʼ";
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �������������
            GenNeedle();
            UpdateText();
        }

        if (isStarted)
        {
            // �Զ�������
            timer += Time.deltaTime;
            if (timer > 0.03f)
            {
                // ��߶����������ӿ��ٶ�
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
    /// ������
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
    /// ����UI
    /// </summary>
    private void UpdateText()
    {
        totalCntText.text = totalCnt.ToString();
        hitedCntText.text = hitedCnt.ToString();
        if (0 != hitedCnt)
            resultText.text = ((float)totalCnt / hitedCnt).ToString("#0.00000");
    }
//��������������������������������
//��Ȩ����������ΪCSDN���������·�����ԭ�����£���ѭCC 4.0 BY-SA��ȨЭ�飬ת���븽��ԭ�ĳ������Ӽ���������
//ԭ�����ӣ�https://blog.csdn.net/linxinfa/article/details/119815354
}
