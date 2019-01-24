using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanlzFramework
{
    public class SceneManager : Singleton<SceneManager>
    {
        #region SceneInfoData class
        public class SceneInfoData
        {
            public BaseScene SceneType { get; private set; }
            public string SceneName { get; private set; }
            public object[] Params { get; private set; }

            public SceneInfoData(string _sceneName, BaseScene _sceneType, params object[] _params)
            {
                this.SceneType = _sceneType;
                this.SceneName = _sceneName;
                this.Params = _params;
            }
        }
        #endregion

        private Dictionary<EnumSceneType, SceneInfoData> dicSceneInfos = null;
        private BaseScene currentScene = new BaseScene();
        public BaseScene CurrentScene
        {
            get
            {
                return currentScene;
            }
            set
            {
                if (currentScene != value) {
                    currentScene = value;
                    if (null != currentScene)
                    {
                        currentScene.Load();
                    }
                }
            }
        }
        public EnumSceneType LastSceneType { get; set; }
        public EnumSceneType currentSceneType { get; private set; }

        public override void Init()
        {
            dicSceneInfos = new Dictionary<EnumSceneType, SceneInfoData>();
        }

        public void RegisterAllScene()
        {
            RegisterScene(EnumSceneType.HallScene, "Hall", new HallScene(), null);
            RegisterScene(EnumSceneType.LoadingScene, "LoadingScene", null, null);
            RegisterScene(EnumSceneType.GameScene, "Game", new GameScene(), null);

            CurrentScene = GetSceneInfo(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name).SceneType;
        }

        public void RegisterScene(EnumSceneType _sceneID, string _sceneName, BaseScene _sceneType, params object[] _params)
        {
            //if (_sceneType == null || _sceneType.BaseType != typeof(BaseScene))
            //{
            //    throw new Exception("立钻哥哥：Register scene type must not null and extends BaseScene");
            //}
            if (!dicSceneInfos.ContainsKey(_sceneID))
            {
                SceneInfoData sceneInfo = new SceneInfoData(_sceneName, _sceneType, _params);
                dicSceneInfos.Add(_sceneID, sceneInfo);
            }
        }

        public void UnRegisterScene(EnumSceneType _sceneID)
        {
            if (dicSceneInfos.ContainsKey(_sceneID))
            {
                dicSceneInfos.Remove(_sceneID);
            }
        }

        public bool IsRegisterScene(EnumSceneType _sceneID)
        {
            return dicSceneInfos.ContainsKey(_sceneID);
        }

        //internal BaseScene InstanceScene(EnumSceneType _sceneType)
        //{
        //    Debug.Log("GetBaseScene sceneId = " +_sceneType.ToString());
        //    SceneInfoData sceneInfo = GetSceneInfo(_sceneType);
        //    if (sceneInfo == null || sceneInfo.SceneType == null)
        //    {
        //        return null;
        //    }
        //    BaseScene scene = System.Activator.CreateInstance(sceneInfo.SceneType) as BaseScene;
        //    return scene;
        //}

        public SceneInfoData GetSceneInfo(EnumSceneType _sceneID)
        {
            if (dicSceneInfos.ContainsKey(_sceneID))
            {
                return dicSceneInfos[_sceneID];
            }
            Debug.LogError("This Scene is not register!ID: " +_sceneID.ToString());
            return null;
        }

        public SceneInfoData GetSceneInfo(string _sceneName)
        {
            List<SceneInfoData> SceneInfoDatas = new List<SceneInfoData>(dicSceneInfos.Values);
            foreach (SceneInfoData info in SceneInfoDatas)
            {
                if (info.SceneName == _sceneName) {
                    return info;
                }
            }
            Debug.LogError("This Scene is not register!name: " + _sceneName);
            return null;
        }

        public string GetSceneName(EnumSceneType _sceneID)
        {
            if (dicSceneInfos.ContainsKey(_sceneID))
            {
                return dicSceneInfos[_sceneID].SceneName;
            }
            Debug.LogError("This Scene is not register!ID: " +_sceneID.ToString());
            return null;
        }

        public void ClearScene()
        {
            dicSceneInfos.Clear();
        }

        #region Change Scene
        public void ChangeSceneDirect(EnumSceneType _sceneType, Action<float> action = null)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Release();
                CurrentScene = null;
            }
            LastSceneType = currentSceneType;
            currentSceneType = _sceneType;
            string sceneName = GetSceneName(_sceneType);
            //change scene
            CoroutineController.Instance.StartCoroutine(AsyncLoadScene(sceneName, action));
        }

        private IEnumerator<AsyncOperation> AsyncLoadScene(string sceneName,Action<float> action)
        {
            AsyncOperation oper = Application.LoadLevelAsync(sceneName);


            while (oper.progress < 0.9)
            {
                Debug.Log("1加载中:" + oper.progress);
                if (action!=null) {
                    action(oper.progress);
                }
                yield return null;
            }
            yield return oper;
            if (oper.isDone) {
                CurrentScene = GetSceneInfo(currentSceneType).SceneType;
            }
        }

        #endregion  
        //先跳转到loading界面在去下一个场景
        public void ChangeScene(EnumSceneType _sceneType)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Release();
                CurrentScene = null;
            }
            LastSceneType = currentSceneType;
            currentSceneType = _sceneType;
            string sceneName = GetSceneName(_sceneType);
            //change loading scene
            CoroutineController.Instance.StartCoroutine(AsyncLoadOtherScene(_sceneType));
        }

        private IEnumerator<AsyncOperation> AsyncLoadOtherScene(EnumSceneType sceneType)
        {
            string sceneName = GetSceneName(EnumSceneType.LoadingScene);
            AsyncOperation oper = Application.LoadLevelAsync(sceneName);
            yield return oper;

            //message send
            if (oper.isDone)
            {
                CurrentScene = GetSceneInfo(EnumSceneType.LoadingScene).SceneType;
                GameObject go = GameObject.Find("LoadingScenePanel");
                LoadingSceneUI loadingSceneUI = go.GetComponent<LoadingSceneUI>();

                //检测是否注册该场景
                if (!SceneManager.Instance.IsRegisterScene(sceneType))
                {
                    Debug.LogError("没有注册此场景：" + sceneType.ToString());
                }
                //加载场景
                loadingSceneUI.Load(sceneType);
            }
        }
    }
}
