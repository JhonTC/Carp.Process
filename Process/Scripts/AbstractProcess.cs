using System;

namespace Carp.Process
{
    public abstract class AbstractProcess
    {
        public Action<AbstractProcess, object> onComplete { get; set; }
        public bool waitForResult { get; set; }
        public int waitTimeout { get; set; }
        public string name { get; set; }

        public AbstractProcess(bool _waitForResult = true, int _waitTimeout = 0, string _name = "")
        {
            waitForResult = _waitForResult;
            waitTimeout = _waitTimeout;
            name = _name != "" ? _name : ToString();
        }

        public abstract void InvokeProcess();

        public virtual void Update() { }
    }
}
