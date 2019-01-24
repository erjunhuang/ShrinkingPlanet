using System;

namespace YanlzFramework
{
    public class BaseModule
    {
        private EnumObjectState _state = EnumObjectState.None;
        public event StateChangeEvent StateChanged;
        public EnumObjectState State
        {
            get
            {
                return this._state;
            }
            set
            {
                if (this._state == value)
                {
                    return;
                }

                EnumObjectState oldState = this._state;
                this._state = value;
                if (null != StateChanged)
                {
                    StateChanged(this, this._state, oldState);
                }
                OnStateChanged(this._state, oldState);
            }
        }
        protected virtual void OnStateChanged(EnumObjectState newState, EnumObjectState oldState)
        {
        }


        private EnumRegisterMode registerMode = EnumRegisterMode.NotRegister;

        public bool HasRegistered
        {
            get
            {
                return registerMode == EnumRegisterMode.AlreadyRegister;
            }
        }


        public void Load()
        {
            State = EnumObjectState.Initial;

            // register
            ModuleManager.Instance.Register(this);
            registerMode = EnumRegisterMode.AlreadyRegister;

            State = EnumObjectState.Loading;
            OnLoad();

            State = EnumObjectState.Ready;
        }

        protected virtual void OnLoad()
        {
        }

        public void Release()
        {

            if (State != EnumObjectState.Closing)
            {
                State = EnumObjectState.Closing;
                //...
                if (registerMode == EnumRegisterMode.AlreadyRegister)
                {
                    //unregister
                    ModuleManager.Instance.UnRegister(this);
                    registerMode = EnumRegisterMode.NotRegister;
                }
                State = EnumObjectState.None;
                OnRelease();
            }
        }

        protected virtual void OnRelease()
        {
        }
    }
}
