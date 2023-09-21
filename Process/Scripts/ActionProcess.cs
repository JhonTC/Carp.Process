using System;

namespace Carp.Process
{
    public class ActionProcess : AbstractProcess
    {
        public Action processAction;

        public ActionProcess() : base(true, 0) {}

        public override void InvokeProcess()
        {
            processAction?.Invoke();
        }
    }
}
