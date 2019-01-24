using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YanlzFramework;

public class TestOne : Panel
{
    protected override void OnAwake()
    {
        base.OnAwake();

        EventTriggerListener.Get(transform.Find("TestUIOneBtn").gameObject).onPointerClick = new TouchHandle(OnClickBtn);

        MessageCenter.Instance.AddListener(MessageType.UpdatePlayerCoin, UpdateGold);
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void OnLoadData()
    {
        base.OnLoadData();
    }

     
    protected override void OnUpdate(float delatTime)
    {
        base.OnUpdate(delatTime);
    }

    protected override void OnClose()
    {
        MessageCenter.Instance.RemoveListener(MessageType.UpdatePlayerCoin, UpdateGold);
        base.OnClose();
    }

    public void OnClickBtn(GameObject _sender, object _args, params object[] _params)
    {
        Close();
    }
    private void UpdateGold(Message message)
    {
        int gold = (int)message["gold"];
        Debug.Log("TestOne UpdateGold:" + gold);
    }
}
