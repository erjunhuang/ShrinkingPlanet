using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YanlzFramework;

public class MeteorSpawner : MonoBehaviour {

	public GameObject [] meteorPrefabs;
	public float distance = 20f;
    public float intervalTime = 0f;

    void Start ()
	{
        List<BaseActor> Roles = SceneManager.Instance.CurrentScene.GetActorByID(EnumActorType.Monster);

        //foreach (BaseActor Role in Roles)
        Debug.Log("Roles:" + Roles.Count);
        for(int i=0;i<Roles.Count;i++)
        {
            BaseActor actor = Roles[i];
            if (actor != null)
            {
                 
                StartCoroutine(SpawnMeteor(i+ intervalTime, actor));
            }
        }
    }

	IEnumerator SpawnMeteor(float intervalTime,BaseActor actor)
	{
        yield return new WaitForSeconds(intervalTime);

        Vector3 pos = Random.onUnitSphere * distance;
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
		GameObject Monster = Instantiate(meteorPrefabs[randomIndex], pos, Quaternion.identity);
        Meteor meteor = Monster.GetComponent<Meteor>();
        if (meteor) {
            Debug.Log("Role:" + actor.GetProperty(EnumPropertyType.RoleName).Content);
            meteor.MeteorName = actor.GetProperty(EnumPropertyType.RoleName).Content.ToString();
        }
		//StartCoroutine(SpawnMeteor());
	}

}
