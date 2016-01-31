using Assets;
using UnityEngine;

public class Dog : MonoBehaviour
{
	private readonly DogMachine _machine = new DogMachine();

	private void Start()
	{
		_machine.Config(this, new DogMachine.IdleState());
	}

	private void Update()
	{
		_machine.Update();
	}
}