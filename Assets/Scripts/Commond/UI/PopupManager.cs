using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanlzFramework
{
    public class PopupManager : Singleton<PopupManager>
    {   
      public  class PopupStack
        {
            public GameObject gameObject;
            public object[] _uiParams;

            public PopupStack(GameObject gameObject, params object[] _uiParams) {
                this.gameObject = gameObject;
                this._uiParams = _uiParams;
            }
        }
        private Dictionary<Type, PopupStack> dicOpenUIs = null;
        public override void Init()
        {
            dicOpenUIs = new Dictionary<Type, PopupStack>();
            Debug.Log("UIManager: Singleton < UIManager > Init");
        }

        public void addPopup(Type panel, params object[] _uiParams)
        {
            if (!dicOpenUIs.ContainsKey(panel))
            {
                string _path = UIPathDefines.UI_PREFAB + panel;
                GameObject _uiObject = MonoBehaviour.Instantiate(Resources.Load(_path)) as GameObject;
                PopupStack popupStack = new PopupStack(_uiObject, _uiParams);
                Debug.Log("添加UI成功");
                dicOpenUIs.Add(panel, popupStack);
            }
        }

        #region Close UI Type
        public void removeAllPopup()
        {
            List<Type> _keyList = new List<Type>(dicOpenUIs.Keys);
            foreach (Type gameObject in _keyList)
            {
                this.RemovePopup(gameObject);
            }
            dicOpenUIs.Clear();
        }

        public void RemovePopup(Type panel)
        {
            if (panel == null) return;
            if(dicOpenUIs.ContainsKey(panel))
            {
                Debug.Log("删除成功" );
                PopupStack popupStack;
                if (dicOpenUIs.TryGetValue(panel, out popupStack))
                {
                    dicOpenUIs.Remove(panel);
                    GameObject.Destroy(popupStack.gameObject);
                }
            }
        }
        #endregion
    }
}
