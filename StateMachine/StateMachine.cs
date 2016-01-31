namespace Assets
{
	public class StateMachine<T>
	{
		private T _owner;
		private State<T> _currentState;

		public void Config(T owner, State<T> state)
		{
			_owner = owner;
			_currentState = state;
			ChangeState(_currentState);
		}

		public void ChangeState(State<T> state)
		{
			if (_currentState != null)
			{
				_currentState.Exit(_owner);
			}
			_currentState = state;
			_currentState.Enter(_owner);
		}

		public void Update()
		{
			if (_currentState != null)
			{
				_currentState.Execute(_owner);
			}
		}
	}
}