# SceneJect

  SceneJect is a Unity3D scene-based dependency injection library based on AutoFac. Using AutoFac as its Inversion of Control container it provides a way for you to supply dependencies to other MonoBehaviours that exist within the scene at editor time. These dependencies can be both either MonoBehaviours that exist within the scene or non-MonoBehaviour inheriting objects that are registered via a provided registration class.

## What do I do?

### Basic Classes

  There are only a handful of important classes in SceneJect that you will have to use to be able to inject dependencies into your scene's MonoBehaviours.

- **_SceneJector_**: This is a MonoBehaviour that must be added as a component in the scene. It is the driving force behind the library and is the bare minimum that must exist within the scene to supply dependencies to other MonoBehaviours in the scene.
  - Must exist in the scene
  - There must be only one SceneJector in the scene.
  

- **_InjecteeAttribute_**: This is a C# attribute class that can target only classes. For a resource on understanding attributes please refer to this MSDN page: [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx). This particular attribute is simplistic and its purpose is for efficiency but it cannot be ignored regardless whether you'd prefer this efficiency or not. The *InjecteeAttribute* should target MonoBehaviours that have dependencies that need injecting. Without this attribute the SceneJector will not find nor supply the dependencies to your MonoBehaviours
  - Must target MonoBehaviour derived classes that require dependencies
  - Used to find the object in the scene
  - Is not optional

- **_InjectAttribute_**: Like *InjecteeAttribute* this is also a C# attribute. Refer to [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx) for information on what an attribute is. This attribute, like *InjecteeAttribute* is also very simple. It can only target properties or fields and must target the properties or fields you wish *SceneJector* to inject dependencies into.
  - Must target fields and/or properties you wish *SceneJector* to inject dependencies into.
  - Used to locate the fields in a *InjecteeAttribute* targeted class that requires dependency injection
  - Is not optional

## What if the dependency isn't a MonoBehaviour?
  
  SceneJect also covers cases for when your scene's MonoBehaviours require a dependency but the dependency you've written, and want to supply, isn't a MonoBehaviour derived object itself and thus cannot be rigged up directly in the editor via SceneJector. Although it's not difficult to register this sort of dependency it is still more complicated to do than dragging a MonoBehaviour in the scene onto the list and setting the register Type. One benefit of doing this is you're given more control over how this dependency is registered with the underlying AutoFac IoC container and dependencies that aren't logically MonoBehaviours don't have to be forced to derive from MonoBehaviour.
  
### Classes

  There are only two classes that aren't apart of Mono/.Net that you need to fully understand in SceneJect to register a non-MonoBehaviour deriving dependencies.

- **_NonBehaviourDepedency_**: Although the name might imply otherwise this is actually a MonoBehaviour itself. *NonBehaviourDependency* is an abstract class that derives from MonoBehaviour. It has only 2 parts that must be understood to utilize. It contains an abstract Register method and a protected getFlags method that returns a flags enum. Register is called by Scenejector before it attempts to resolve any dependencies in the scene. This method provides, as a parameter, an *IServiceRegister* that can be used to register the dependency.

  The idea is you should inherit from *NonBehaviourDepedency* on a MonoBehaviour that you will add to your scene and drag-and-drop unto the secondary list of *NonBehaviourDepdency*s on the *SceneJector* component. Once you inherit from this Type on your wrapper-esque class you will be required to implement the abstract Register method. Inside of this method the bare minimum that must be done is to call the register method on the IServiceRegister parameter supplying a dependency instance as well as the getFlags() return value. getFlags is a protected method that builds a value that corresponds to a flags enum that is setup in the Unity3D inspector on the SceneJector component. Additionally, you can provide an instance of a Type object in the last defaulted parameter. This indicates to the registeration service that you want to register the service as that type. Once this is done all that is left to be done is to attach this MonoBehaviour somewhere in the scene and add it to the bottom list on the *SceneJector*. Additionally you should setup the registration enum in the editor too which is implemented as a list of enums. For information on which flags to use refer to this file in the repo: [here](https://github.com/HelloKitty/SceneJect/blob/master/src/SceneJect/Registeration/RegisterationType.cs).
  
  If these steps are followed your dependency, which is a non-MonoBehaviour, will be registered and injected into your scene's MonoBehaviours just described in the first and much simpler process. (Note that the attributes mentioned in the first process are not required to register the dependency **BUT** an InjectAttribute must still be targeting the destination field/property to be injected into.)
