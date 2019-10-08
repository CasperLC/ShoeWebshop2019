using System.Collections.Generic;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.DomainServices
{
    public interface IShoeRepository
    {
        //Returns a single shoe with the corresponding Id
        Shoe ReadShoe(int id);

        //Returns a list of all shoes
        List<Shoe> ReadAllShoes(Filter filter = null);

        //Creates a new shoe and returns the created shoe
        Shoe CreateShoe(Shoe shoeToCreate);

        //Updates the info of the shoe with the same Id to the shoe given as parameter
        Shoe UpdateShoe(Shoe updatedShoe);

        //Deletes a shoe with the given Id and returns the deleted object
        Shoe DeleteShoe(int id);

        //Gets number of shoes in db
        int Count();
    }
}