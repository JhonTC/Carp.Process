using UnityEngine;
using Carp.Process;

public class ProcessDemo : MonoBehaviour
{
    private GroupProcess gameProcess = new GroupProcess(false);

    void Start()
    {
        // Single collider selection example
        SelectionProcess<Collider> singleColliderSelectionProcess = new SelectionProcess<Collider>();
        singleColliderSelectionProcess.onComplete += (sender, selectedItem) =>
        {
            Collider collider = selectedItem as Collider;
            Debug.Log("Selected Object...");
            Debug.Log(collider.gameObject.name);
        };
        gameProcess.RequestProcess(singleColliderSelectionProcess);

        // Multiple Transform selection example
        SelectionProcess<Transform> multipleTransformSelectionProcess = new SelectionProcess<Transform>(2);
        multipleTransformSelectionProcess.onComplete += (sender, selectedItems) =>
        {
            Debug.Log("Selected Objects...");
            Transform[] transforms = selectedItems as Transform[];
            foreach (var transform in transforms)
            {
                Debug.Log(transform.gameObject.name);
            }
        };
        gameProcess.RequestProcess(multipleTransformSelectionProcess);
    }

    private void Update()
    {
        gameProcess.Update();
    }
}
