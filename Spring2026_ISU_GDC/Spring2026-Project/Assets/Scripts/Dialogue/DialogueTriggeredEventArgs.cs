namespace ISUGameDev.SpearGame.Dialogue
{
	public readonly struct DialogueTriggeredEventArgs
	{
		public Dialogue Resource => _resource;
		private readonly Dialogue _resource;

		public DialogueTriggeredEventArgs(Dialogue resource)
		{
			_resource = resource;
		}
	}
}