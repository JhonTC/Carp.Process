----Process Library----
The process library is a different way of programming systems in Unity.
 
Instead of using many Monobehaviours, you would have only a few, each 
with a GroupProcess variable. This GroupProcess enqueues other processes 
and the dequeues and invokes them one at a time until there are none left.


--Demo Details--
In the demo, the ProcessDemo script is the controller Monobehaviour with
the GroupProcess variable. Two new sub-process are created and enqueued.

Sub-process
- singleColliderSelectionProcess is a SelectionProcess<Collider> which waits 
  for the user to select one collider.

- multiTransformSelectionProcess is a SelectionProcess<Transform> which 
  waits for the user to select two transforms.

SelectionProcess is simply an example of the Processes that can be built 
and enqueued. 


--Code Details--
AbstractProcess 
- The class that all other processes inherit from.

GroupProcess
- Holds a list of other processes and invokes them one at a time.

ActionProcess
- Allows for an Action to be passed and invoked when the process is. This is 
  used mainly for creating inline processes.  

SelectionProcess<T>
- A example template class that takes a type and a number of objects to select.
  It then waits for the user to click on the that number of 'T' typed objects, 
  returning them in its OnComplete action.   


--My Useage--
I've used this in my Timeline Rumble deck building game to handle displaying the 
cards in and out of your hand.


--Updates To Come--
- Process multi-threading for processes that don't have the waitForResult set 
  to true.
- Process timeouts incase you have a process that has the waitForResult set 
  to true and the result is never received.


