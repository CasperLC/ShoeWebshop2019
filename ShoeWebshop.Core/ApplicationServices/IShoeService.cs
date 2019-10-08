using System.Collections.Generic;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices
{
    public interface IShoeService
    {
        //Returns a single shoe with the corresponding Id
        Shoe ReadShoe(int id);

        //Returns a list of all shoes
        List<Shoe> ReadAllShoes();

        //Creates a new shoe and returns the created shoe
        Shoe CreateShoe(Shoe shoeToCreate);

        //Updates the info of the shoe with the same Id to the shoe given as parameter
        Shoe UpdateShoe(Shoe updatedShoe);

        //Deletes a shoe with the given Id and returns the deleted object
        Shoe DeleteShoe(int id);

        //Gets all shoes with query/filter
        List<Shoe> AllFilteredShoes(Filter filter);

    }
}