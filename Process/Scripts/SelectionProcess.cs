using System;
using System.Collections.Generic;
using UnityEngine;

namespace Carp.Process
{
    public class SelectionProcess<T> : AbstractProcess
    {
        protected List<T> selectedItems = new List<T>();
        protected Selector<T> itemSelector;
        protected int numberOfItemsToSelect;

        public SelectionProcess(int _numberOfItemsToSelect = 1, bool _waitForResult = true, int _waitTimeout = 0)
            : base(_waitForResult, _waitTimeout)
        {
            numberOfItemsToSelect = _numberOfItemsToSelect;
        }

        public override void InvokeProcess()
        {
            string quantityPrefix = numberOfItemsToSelect > 1 ? "s" : "";
            Debug.Log($"INSTRUCTION: Select {numberOfItemsToSelect} {typeof(T)}{quantityPrefix}");

            itemSelector = new Selector<T>();
            itemSelector.OnTypeSelected += OnItemSelected;
        }

        protected void OnItemSelected(T _item)
        {
            if (!selectedItems.Contains(_item))
            {
                selectedItems.Add(_item);
                Debug.Log($"Selected: {_item}");
            }
            else
            {
                selectedItems.Remove(_item);
                Debug.Log($"Deselected: {_item}");
            }

            if (IsRequestFulfilled())
            {
                itemSelector.OnTypeSelected -= OnItemSelected;
                itemSelector = null;

                OnComplete();
            }
        }

        protected virtual void OnComplete()
        {
            Debug.Log($"Selection Complete!");

            if (numberOfItemsToSelect == 1)
            {
                onComplete?.Invoke(this, selectedItems[0]);
            } else
            {
                onComplete?.Invoke(this, selectedItems.ToArray());
            }
        }

        private bool IsRequestFulfilled()
        {
            return selectedItems.Count == numberOfItemsToSelect;
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    itemSelector?.OnClickHit(hit);
                }
            }
        }
    }
}
