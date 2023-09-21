using System.Collections.Generic;
using UnityEngine;

namespace Carp.Process
{
    public class GroupProcess : AbstractProcess
    {
        protected List<AbstractProcess> processQueue = new List<AbstractProcess>();
        public AbstractProcess waitingForProcessValue = null;
        protected bool isWaitingForProcess = false;
        public bool quitWhenQueueIsEmpty = true;

        public GroupProcess(bool _quitWhenQueueIsEmpty = true) : base(true, 0)
        {
            quitWhenQueueIsEmpty = _quitWhenQueueIsEmpty;
        }

        public GroupProcess(string _name, bool _quitWhenQueueIsEmpty = true) : base(true, 0, _name)
        {
            quitWhenQueueIsEmpty = _quitWhenQueueIsEmpty;
        }

        public override void InvokeProcess() { }

        public bool RequestProcess(AbstractProcess _action)
        {
            if (_action == null) return false;

            processQueue.Add(_action);

            return true;
        }

        public void InvokeNextProcess()
        {
            var actionRequest = processQueue[0];
            processQueue.RemoveAt(0);

            Debug.Log($"Invoking: {actionRequest}");

            if (actionRequest != null)
            {
                isWaitingForProcess = actionRequest.waitForResult;

                if (isWaitingForProcess)
                {
                    waitingForProcessValue = actionRequest;
                    actionRequest.onComplete += OnWaitingActionComplete;
                }

                actionRequest.InvokeProcess();

                if (!isWaitingForProcess)
                {
                    if (quitWhenQueueIsEmpty && processQueue.Count <= 0)
                    {
                        onComplete?.Invoke(this, null);
                    }
                }
            }
        }

        public void OnWaitingActionComplete(AbstractProcess sender, object value)
        {
            isWaitingForProcess = false;
            waitingForProcessValue = null;

            OnActionComplete(sender, value);
        }

        public void OnActionComplete(AbstractProcess sender, object value)
        {
            if (quitWhenQueueIsEmpty && processQueue.Count <= 0)
            {
                onComplete?.Invoke(this, null);
            }
        }

        public override void Update()
        {
            if (processQueue.Count > 0 && !isWaitingForProcess)
            {
                InvokeNextProcess();
            }

            if (isWaitingForProcess)
            {
                if (waitingForProcessValue != null)
                {
                    waitingForProcessValue.Update();
                }

                //todo: handle timeout
            }
        }
    }
}
