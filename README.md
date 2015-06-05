# SceneJect
SceneJect is a Unity3D scene-based dependency injection library based on AutoFac. Using AutoFac as its Inversion of Control container provides a way for you to supply dependencies to other MonoBehaviours that exist within the scene at editor time. These dependencies can be both other MonoBehaviours that exist within the scene or non-MonoBehaviour inheriting objects that are registers via a provided wrapper class.

## What do I do?

### Classes

There are only a handful of important classes in SceneJect that you will have to use to be able to inject dependencies into your scene's MonoBehaviours.

- *SceneJector*: This is a MonoBehaviour that must be added as a component in the scene. It is the driving force behind the library and is the bare minimum that must exist within the scene to supply dependencies to other MonoBehaviours in the scene.
  - Must exist in the scene
  - There must be only one SceneJector in the scene.
  

- *InjecteeAttribute* This is a C# attribute class that can target only classes. For a resource on understanding attributes please refer to this MSDN page: [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx). This particular attribute is simplistic and its purpose is for effiency but it cannot be ignored regardless whether you'd prefer this effiency or not. The *InjecteeAttribute* should target MonoBehaviours that have dependencies that need injecting. Without this attribute the SceneJector will not find nor supply the dependencies to your MonoBehaviours
  - Must target MonoBehaviour derived classes that require dependencies
  - Used to find the object in the scene
  - Is not optional

- *InjectAttribute* Like *InjecteeAttribute* this is also a C# attribute. Refer to [here](https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx) for information on what an attribute is. This attribute, like *InjecteeAttribute* is also very simple. It can only target properties or fields and must target the properties or fields you wish *SceneJector* to inject dependencies into.
  - Must target fields and/or properties you wish *SceneJector* to inject dependencies into.
  - Used to locate the fields in a *InjecteeAttribute* targeted class that require dependency injection
  - Is not optional

## What if the dependency isn't a MonoBehaviour?
  
