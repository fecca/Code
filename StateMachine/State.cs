namespace Assets
{
	public abstract class State<T>
	{
		public abstract void Enter(T owner);
		public abstract void Execute(T owner);
		public abstract void Exit(T owner);
	}
}