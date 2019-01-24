using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanlzFramework
{
    public class BaseScene : BaseModule
    {
        protected List<BaseActor> actorList = null;
        public BaseScene()
        {
            actorList = new List<BaseActor>();
        }

        public void AddActor(BaseActor actor)
        {
            if (null != actor && !actorList.Contains(actor))
            {
                actorList.Add(actor);
                actor.CurrentScene = this;
                actor.PropertyChanged += OnActorPropertyChanged;
            }
        }

        public void RemoveActor(BaseActor actor)
        {
            if (null != actor && actorList.Contains(actor))
            {
                actorList.Remove(actor);
                actor.PropertyChanged -= OnActorPropertyChanged;
                actor = null;
            }
        }

        public  List<BaseActor> GetActorByID(EnumActorType actorType)
        {
            List<BaseActor> baseActors = new List<BaseActor>();
            if (null != actorList && actorList.Count > 0)
            {
                for (int i = 0; i < actorList.Count; i++)
                {
                    if (actorList[i].actorType == actorType)
                    {
                        // return actorList[i];
                        baseActors.Add(actorList[i]);
                    }
                }
            }
            return baseActors;
        }

        protected void OnActorPropertyChanged(BaseActor actor, EnumPropertyType propertyType, object oldValue, object newValue)
        {
        }

        public new void Load()
        {
            State = EnumObjectState.Initial;

            State = EnumObjectState.Loading;
            OnLoad();

            State = EnumObjectState.Ready;
        }
        public new void Release()
        {
            if (State != EnumObjectState.Closing)
            {
                State = EnumObjectState.Closing;
                CoroutineController.Instance.StopAllCoroutines();

                State = EnumObjectState.None;
                OnRelease();
            }
        }
    }

   }
