internal class DatabaseStats {
	public int ProductCount { get; set; }
	public string? OldestProduct { get; set; }
	public string? NewestProduct { get; set; }
	public bool IsValid { get; set; }
	public string? ErrorMessage { get; set; }
}