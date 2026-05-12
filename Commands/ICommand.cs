internal interface ICommand {
	string Key { get; }
	string Help { get; }
	string? DetailedHelp { get; }
	string? Run(string[] args);
}