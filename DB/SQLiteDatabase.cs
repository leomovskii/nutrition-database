using Microsoft.Data.Sqlite;

internal class SQLiteDatabase : IDatabase {

	public readonly static string FileName = Path.Combine(AppContext.BaseDirectory, "products.db");
	private readonly static string ConnectionString = $"Data Source={FileName}";

	public DatabaseStats GetDatabaseStats() {
		var stats = new DatabaseStats { IsValid = true };

		try {
			using var connection = new SqliteConnection(ConnectionString);
			connection.Open();

			var command = connection.CreateCommand();
			command.CommandText = "SELECT COUNT(*) FROM Products";
			stats.ProductCount = (int) (long) command.ExecuteScalar()!;

			if (stats.ProductCount > 0) {
				command.CommandText = "SELECT MIN(LastUpdated), MAX(LastUpdated) FROM Products";
				using var reader = command.ExecuteReader();
				if (reader.Read()) {
					stats.OldestProduct = reader.GetString(0);
					stats.NewestProduct = reader.GetString(1);
				}
			}
		} catch (Exception ex) {
			stats.IsValid = false;
			stats.ErrorMessage = ex.Message;
		}

		return stats;
	}

	public void EnsureExists() {
		if (!File.Exists(FileName)) {
			using var connection = new SqliteConnection(ConnectionString);
			connection.Open();

			var pragma = connection.CreateCommand();
			pragma.CommandText = "PRAGMA encoding = 'UTF-8'";
			pragma.ExecuteNonQuery();

			var command = connection.CreateCommand();
			command.CommandText = @"
				CREATE TABLE IF NOT EXISTS Products (
					Id INTEGER PRIMARY KEY AUTOINCREMENT,
					Title TEXT NOT NULL,
					Proteins REAL,
					Fats REAL,
					Carbs REAL,
					Calories REAL,
					Salt REAL,
					LastUpdated TEXT NOT NULL
				);
			";
			command.ExecuteNonQuery();
		}
	}

	public ProductInfo[] TryFindProducts(string[] args) {
		string searchTitle = string.Join(" ", args).ToLowerInvariant();
		var results = new List<ProductInfo>();

		using var connection = new SqliteConnection(ConnectionString);
		connection.Open();
		var command = connection.CreateCommand();
		command.CommandText = @"
			SELECT Id, Title, Proteins, Fats, Carbs, Calories, Salt, LastUpdated
			FROM Products
		";

		using var reader = command.ExecuteReader();
		while (reader.Read()) {
			string dbTitle = reader.GetString(1);
			if (dbTitle.Contains(searchTitle, StringComparison.InvariantCultureIgnoreCase)) {
				results.Add(new ProductInfo {
					Id = reader.GetInt32(0),
					Title = dbTitle,
					Proteins = reader.IsDBNull(2) ? null : reader.GetDouble(2),
					Fats = reader.IsDBNull(3) ? null : reader.GetDouble(3),
					Carbs = reader.IsDBNull(4) ? null : reader.GetDouble(4),
					Calories = reader.IsDBNull(5) ? null : reader.GetDouble(5),
					Salt = reader.IsDBNull(6) ? null : reader.GetDouble(6),
					LastUpdated = DateTime.Parse(reader.GetString(7))
				});
			}
		}

		return [.. results];
	}

	public bool TryFindProductById(int productId, out ProductInfo productInfo) {
		using var connection = new SqliteConnection(ConnectionString);
		connection.Open();
		var command = connection.CreateCommand();
		command.CommandText = @"
			SELECT Id, Title, Proteins, Fats, Carbs, Calories, Salt, LastUpdated
			FROM Products 
			WHERE Id = @id
		";
		command.Parameters.AddWithValue("@id", productId);

		using var reader = command.ExecuteReader();
		if (reader.Read()) {
			productInfo = new ProductInfo {
				Id = reader.GetInt32(0),
				Title = reader.GetString(1),
				Proteins = reader.IsDBNull(2) ? null : reader.GetDouble(2),
				Fats = reader.IsDBNull(3) ? null : reader.GetDouble(3),
				Carbs = reader.IsDBNull(4) ? null : reader.GetDouble(4),
				Calories = reader.IsDBNull(5) ? null : reader.GetDouble(5),
				Salt = reader.IsDBNull(6) ? null : reader.GetDouble(6),
				LastUpdated = DateTime.Parse(reader.GetString(7))
			};
			return true;
		}

#pragma warning disable 8625
		productInfo = null;
#pragma warning restore 8625
		return false;
	}

	public int AddOrUpdateProduct(ProductInfo product) {
		using var connection = new SqliteConnection(ConnectionString);
		connection.Open();
		var command = connection.CreateCommand();

		product.LastUpdated = DateTime.Now;

		if (product.Id == -1) {
			command.CommandText = @"
				INSERT INTO Products (Title, Proteins, Fats, Carbs, Calories, Salt, LastUpdated)
				VALUES (@title, @proteins, @fats, @carbs, @calories, @salt, @lastUpdated)
			";
		} else {
			command.CommandText = @"
				UPDATE Products 
				SET Title = @title, Proteins = @proteins, Fats = @fats, 
					Carbs = @carbs, Calories = @calories, Salt = @salt, LastUpdated = @lastUpdated
				WHERE Id = @id
			";
			command.Parameters.AddWithValue("@id", product.Id);
		}

		command.Parameters.AddWithValue("@title", product.Title);
		command.Parameters.AddWithValue("@proteins", (object?) product.Proteins ?? DBNull.Value);
		command.Parameters.AddWithValue("@fats", (object?) product.Fats ?? DBNull.Value);
		command.Parameters.AddWithValue("@carbs", (object?) product.Carbs ?? DBNull.Value);
		command.Parameters.AddWithValue("@calories", (object?) product.Calories ?? DBNull.Value);
		command.Parameters.AddWithValue("@salt", (object?) product.Salt ?? DBNull.Value);
		command.Parameters.AddWithValue("@lastUpdated", product.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

		return command.ExecuteNonQuery();
	}

	public int DeleteProduct(int productId) {
		using var connection = new SqliteConnection(ConnectionString);
		connection.Open();
		var command = connection.CreateCommand();
		command.CommandText = "DELETE FROM Products WHERE Id = @id";
		command.Parameters.AddWithValue("@id", productId);
		return command.ExecuteNonQuery();
	}
}