using UnityEngine;
using SceneJect;

//This attribute must target the class. It's critical that it does or else no dependencies will be injected.
[Injectee]
public class ExampleClass : MonoBehaviour
{
	//This inject attribute should target a depedency within the class that
	//needs to be injected.
	[Inject]
	private IMovementHandler movementHandler;

	//The injector won't touch this because it's not marked with the [Inject] attribute.
	public GameObject SomeObject;

	//This also works with properties.
	[Inject]
	public IIventoryService Inventory { get; private set;}

	void Awake()
	{
		//It is best NOT to reference any of the above dependencies in Awake. Unless you modify the script execution order
		//it cannot be determined if these depedencies will have been injected before this method body is executed.
	}

	void Start()
	{
		//All dependencies that could be injected and resolved will have been by this point.
	}
}
