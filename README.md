# SceneJect

  SceneJect is a Unity3D scene-based dependency injection library based on AutoFac. Using AutoFac as its Inversion of Control container provides a way for you to supply dependencies to other MonoBehaviours that exist within the scene at editor time. These dependencies can be both other MonoBehaviours that exist within the scene or non-MonoBehaviour inheriting objects that are registers via a provided wrapper class.

## What do I do?

### Basic Classes

  There are only a handful of important classes in SceneJect that you will have to use to be able to inject dependencies into your scene's MonoBehaviours.

- **_SceneJector_**: This is a MonoBehaviour that must be added as a component in the scene. It is the driving force behind the library and is the bare minimum that must exist within the scene to supply dependencies to other MonoBehaviours in the scene.
  - Must exist in the scene
  - There must be only one SceneJector in the scene.
  

- **_InjecteeAttribute_**: This is a C# attribute class that can target only classes. For a resource on understanding attributes please refer to this MSDN page: [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx). This particular attribute is simplistic and its purpose is for effiency but it cannot be ignored regardless whether you'd prefer this effiency or not. The *InjecteeAttribute* should target MonoBehaviours that have dependencies that need injecting. Without this attribute the SceneJector will not find nor supply the dependencies to your MonoBehaviours
  - Must target MonoBehaviour derived classes that require dependencies
  - Used to find the object in the scene
  - Is not optional

- **_InjectAttribute_**: Like *InjecteeAttribute* this is also a C# attribute. Refer to [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx) for information on what an attribute is. This attribute, like *InjecteeAttribute* is also very simple. It can only target properties or fields and must target the properties or fields you wish *SceneJector* to inject dependencies into.
  - Must target fields and/or properties you wish *SceneJector* to inject dependencies into.
  - Used to locate the fields in a *InjecteeAttribute* targeted class that require dependency injection
  - Is not optional

## What if the dependency isn't a MonoBehaviour?
  
  SceneJect also covers cases for when your scene's MonoBehaviours require a dependency but the one you've written and want to supply isn't a MonoBehaviour itself and thus cannot be rigged up directly in the editor via SceneJector. Although it's not difficult to register this sort of dependency it is still more complicated to do than dragging a MonoBehaviour in the scene onto the list and setting the type. One benefit of doing this is you're given more control over how this depdency is registered with the underlying AutoFac IoC container.
  
### Classes

  There are only two classes that aren't apart of Mono/.Net that you need to know about in SceneJect to register these non-MonoBehaviour dependencies.

- **_NonBehaviourDepedency_**: Although the name might imply otherwise this is actually a MonoBehaviour itself. *NonBehaviourDependency*. It is an abstract class that derives from MonoBehaviour. It has only 2 parts that must be understood to utilize. It contains an abstract Register method and a getFlags method. Register is called by Scenejector before it attempts to resolve any dependencies. It provides, as a parameter, an *IServiceRegister* that can be used to register the depedency. 

  The idea is you should inherit from *NonBehaviourDepedency* on a wrapper MonoBehaviour that you will add to your scene and drag-and-drop unto the secondary list of *NonBehaviourDepdency*s on the *SceneJector* component. Once you inherit from this class on your wrapper-esque class you will be required to implement the abstract Register method. Inside of this method the bare minium that must be done is to call the register method on the IServiceRegister parameter supplying a dependency instance as well as the getFlags() return value. getFlags is a protected method that builds a value that corresponds to a flags enum that is setup in the scene. Additionally you can provide an instance of a Type object in the last defaulted parameter. This indicates to the registeration service that you want to register the service as that type. Once this is done all that is left to be done is to attach this MonoBehaviour somewhere in the scene and add it to the bottom list on the *SceneJector*. Additionally you should setup the registeration enum in the editor too which is implemented as a list of enums. For information on which flags to use refer to this file in the repo: [here](https://github.com/HelloKitty/SceneJect/blob/master/src/SceneJect/Registeration/RegisterationType.cs).
  
  If these steps are followed your dependency, which is a non-MonoBehaviour, will be registered and injected into your scene's MonoBehaviour just like the above's much simplier process describes. (Note that the attributes mentioned above are not required to for the depedency **BUT** an InjectAttribute must still be targeting the destination field/property to be injected into.)
