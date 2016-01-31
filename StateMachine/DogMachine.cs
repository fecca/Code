using UnityEngine;

namespace Assets
{
	public class DogMachine : StateMachine<Dog>
	{
		public class IdleState : State<Dog>
		{
			public override void Enter(Dog owner)
			{
				Debug.Log("#asdf# " + "IdleState.Enter");
			}

			public override void Execute(Dog owner)
			{
				Debug.Log("#asdf# " + "IdleState.Execute");
			}

			public override void Exit(Dog owner)
			{
				Debug.Log("#asdf# " + "IdleState.Exit");
			}
		}

		public class ChaseState : State<Dog>
		{
			public override void Enter(Dog owner)
			{
				Debug.Log("#asdf# " + "ChaseState.Enter");
			}

			public override void Execute(Dog owner)
			{
				Debug.Log("#asdf# " + "ChaseState.Execute");
			}

			public override void Exit(Dog owner)
			{
				Debug.Log("#asdf# " + "ChaseState.Exit");
			}
		}
	}
}