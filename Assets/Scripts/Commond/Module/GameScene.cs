using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YanlzFramework;
public class GameScene : BaseScene
{
    protected override void OnLoad()
    {
        Debug.Log("加载游戏场景");

        for (int i = 0; i < 10; i++)
        {
            BaseActor baseActor = new BaseActor(EnumActorType.Monster);
            baseActor.AddProperty(EnumPropertyType.HP, 10 * i);
            baseActor.AddProperty(EnumPropertyType.Coin, 1000 * i);
            baseActor.AddProperty(EnumPropertyType.HPMax, 100 * i);
            baseActor.AddProperty(EnumPropertyType.Level, 1 * i);
            baseActor.AddProperty(EnumPropertyType.RoleName, "GameDitto" + i);
            baseActor.AddProperty(EnumPropertyType.Sex, 0);
            baseActor.AddProperty(EnumPropertyType.RoleID, 1 + i);

            AddActor(baseActor);
        }

    }
    protected override void OnRelease()
    {
        Debug.Log("游戏场景退出了");
    }
}
