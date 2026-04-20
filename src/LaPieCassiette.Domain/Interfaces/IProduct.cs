using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models; 
public interface IProductRepository
{
    List<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
    //IEnumerable<Product> GetAllProduct();
}
