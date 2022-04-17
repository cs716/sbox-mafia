namespace TerryTrials.Actions;

public abstract partial class BaseAction
{
	public virtual string Name { get; set; } = "default";
	public virtual string Message { get; set; }

	public void Execute()
	{
		OnExecute();
		OnExecuted();
	}

	public virtual void OnExecute() { }
	public virtual void OnExecuted() { }
}

