using System;

using YanlzFramework;



public class TestOneModule : BaseModule
{
    public TestOneModule()
    {
    }

    protected override void OnLoad()
    {
        MessageCenter.Instance.AddListener(MessageType.Net_MessageTestOne, NetUpdateGold);
        base.OnLoad();
    }

    protected override void OnRelease()
    {

        MessageCenter.Instance.RemoveListener(MessageType.Net_MessageTestOne, NetUpdateGold);
        base.OnRelease();
    }

    //模拟网络消息更新
    private void NetUpdateGold(Message msg)
    {
        int gold = (int)msg["gold"];
        if (gold >= 0)
        {
            Message message = new Message(MessageType.UpdatePlayerCoin, this);
            message["gold"] = gold;
            message.Send();
        }
    }

    public string getData()
    {
        return "得到公共方法数据";
    }
}
