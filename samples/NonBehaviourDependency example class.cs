using UnityEngine;
using SceneJect;

//You'll see that this class does not require the [Injectee] attribute.
//In fact, at the time of writing this example you should not expect the SceneInjector to
//inject depedencies into this class. I'd recommend, until announced, you avoid doing so.
public class ExampleClass : NonBehaviourDepdency //Must inherit from this class
{
	public override void Register(IServiceRegister register)
	{
		//Let's supply an instance of some made-up Resource management class that is a singleton.
		//get flags is a protected method that supplies the inspector set flags enum for registering.
		register.Register(ResourceLoader.instance, this.getFlags()); //This is all that is needed.

		//The below is an example of another way to register a depedency. It is not another line that is required when the above exists.

		//Optionally you may want to register it like this, indicating specifically the Type to register the service as.
		register.Register(ResourceLoader.instance, this.getFlags(), typeof(ICharacterModelService));

		//The below is an example of another way to register a depedency. It is not another line that is required when the above exists.

		//Alternatively you can register a Type, without an instance, as a depedency. This will be created by the container on-demand.
		//If you set a flag to indicate one per dependency or only a single instance it will adhere to those flags.
		register.Register<SkyBoxController>(this.getFlags(), typeof(IWeatherService)); //This type parameter is optional as well. Without a type or a proper flag it will assume you want to register AsSelf.
	}
}