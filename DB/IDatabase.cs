internal interface IDatabase {
	DatabaseStats GetDatabaseStats();
	void EnsureExists();
	ProductInfo[] TryFindProducts(string[] args);
	bool TryFindProductById(int productId, out ProductInfo productInfo);
	int AddOrUpdateProduct(ProductInfo product);
	int DeleteProduct(int productId);
}