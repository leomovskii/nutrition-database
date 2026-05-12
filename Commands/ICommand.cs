internal interface ICommand {
	string GetKey();
	string GetHelp();
	string? GetDetailedHelp();
	string? Run(string[] args);
}