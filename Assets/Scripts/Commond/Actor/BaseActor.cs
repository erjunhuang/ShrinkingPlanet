using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanlzFramework
{

    public class PropertyItem
    {
        public EnumPropertyType actorType { get; set; }
        private object content;
        public IDynamicProperty Owner = null;    //owner
        public object Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (value != this.content)
                {
                    object oldContent = this.content;
                    this.content = value;
                    if (Owner != null)
                    {
                        Owner.DoChangeProperty(actorType, oldContent, this.content);   //立钻哥哥
                    }
                }
            }
        }

        public PropertyItem(EnumPropertyType actorType, object content)
        {
            this.actorType = actorType;
            Content = content;
        }
    }

    public class BaseActor : IDynamicProperty
    {
        protected Dictionary<EnumPropertyType, PropertyItem> dicProperty;
        public event PropertyChangedHandle PropertyChanged;
        public EnumActorType actorType { set; get; }

        private BaseScene currentScene;
        public BaseScene CurrentScene
        {
            set
            {
                currentScene = value;
            }
            get
            {
                return currentScene;
            }
        }

        public virtual void AddProperty(EnumPropertyType propertyType, object content)
        {

            PropertyItem propertyItem = new PropertyItem(propertyType, content);

            if (null == dicProperty)
            {
                dicProperty = new Dictionary<EnumPropertyType, PropertyItem>();
            }

            if (!dicProperty.ContainsKey(propertyType))
            {
                propertyItem.Owner = this;
                dicProperty.Add(propertyType, propertyItem);
            }
            else {
                dicProperty[propertyType] = propertyItem;
            }
        }
        
        public void RemoveProperty(EnumPropertyType propertyType)
        {
            if (null != dicProperty && dicProperty.ContainsKey(propertyType))
            {
                dicProperty.Remove(propertyType);
            }
        }

        public void ClearProperty()
        {
            if (null != dicProperty)
            {
                dicProperty.Clear();
                dicProperty = null;
            }
        }

        protected virtual void OnPropertyChanged(EnumPropertyType propertyType, object oldValue, object newValue)
        {
            //add update ....
            //if(id == (int)EnumPropertyType.HP)
        }

        public  PropertyItem GetProperty(EnumPropertyType propertyType)
        {
            if (null == dicProperty)
            {
                return null;
            }

            if (dicProperty.ContainsKey(propertyType))
            {
                return dicProperty[propertyType];
            }
            Debug.LogWarning("Actor dicProperty non Property ID:" + propertyType);
            return null;
        }

        #region IDynamicProperty implementation
        public void DoChangeProperty(EnumPropertyType propertyType, object oldValue, object newValue)
        {
            OnPropertyChanged(propertyType, oldValue, newValue);
            if (null != PropertyChanged)
            {
                PropertyChanged(this, propertyType, oldValue, newValue);
            }
        }

        #endregion
        public BaseActor(EnumActorType actorType)
        {
            this.actorType = actorType;
        }
    }

}
