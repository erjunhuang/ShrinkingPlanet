using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YanlzFramework;

public class GameController : DDOLSingleton<GameController>
{
    // Use this for initialization
    void Awake () {
        //注册公共方法
        ModuleManager.Instance.RegisterAllModules();
        //场景注册管理
        SceneManager.Instance.RegisterAllScene();
        //显示UI
        new TestOne().showPanel_();
        //事件模拟
        StartCoroutine(NetUpdateGold());
        //获取公共方法
        Debug.Log(ModuleManager.Instance.Get<TestOneModule>().getData());
        //加载资源
        //TestResManager();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator NetUpdateGold()
    {
        int gold = 0;
        while (true)
        {
            gold++;
            yield return new WaitForSeconds(1.0f);
            Message message = new Message(MessageType.Net_MessageTestOne, this);
            message["gold"] = gold;
            message.Send();
        }
    }

    void TestResManager()
    {

        float time = Environment.TickCount;
        for (int i = 1; i < 1000; i++)
        {

            GameObject go = null;
            //直接加载
            //go = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"));  //1
            //go.transform.position = UnityEngine.Random.insideUnitSphere * 20;

            //2正常加载
            //,()=> {Debug.Log("加载进度成功");}
            //go = Instantiate(ResManager.Instance.LoadInstance("Prefabs/Cube")) as GameObject;
            //go.transform.position = UnityEngine.Random.insideUnitSphere * 20;

            //3、异步加载
            ResManager.Instance.LoadAsync("Prefabs/Cube", (_obj) =>
            {
                go = Instantiate(_obj) as GameObject;
                go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
            }, (_progress) =>
            {
                Debug.Log("加载进度" + _progress);
            });

            ////4、协程加载
            //ResManager.Instance.LoadCoroutine("Prefabs/Cube", (_obj) =>
            //{
            //    go = Instantiate(_obj) as GameObject;
            //    go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
            //});
        }
    }

}
