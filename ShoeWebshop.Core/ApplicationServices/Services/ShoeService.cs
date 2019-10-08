using System.Collections.Generic;
using System.IO;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices.Services
{
    public class ShoeService: IShoeService
    {
        private IShoeRepository shoeRepo;

        public ShoeService(IShoeRepository shoeRepository)
        {
            this.shoeRepo = shoeRepository;
        }

        public Shoe ReadShoe(int id)
        {
            return shoeRepo.ReadShoe(id);
        }

        public List<Shoe> ReadAllShoes()
        {
            return shoeRepo.ReadAllShoes();
        }

        public Shoe CreateShoe(Shoe shoeToCreate)
        {
            if (string.IsNullOrEmpty(shoeToCreate.ProductName))
            {
                throw new InvalidDataException("You can NOT create a shoe without a product name.");
            }

            if (shoeToCreate.Price < 0)
            {
                throw new InvalidDataException("The price of a shoe can NOT be less than 0");
            }

            return shoeRepo.CreateShoe(shoeToCreate);
        }

        public Shoe UpdateShoe(Shoe updatedShoe)
        {
            if (string.IsNullOrEmpty(updatedShoe.ProductName))
            {
                throw new InvalidDataException("You can NOT create a shoe without a product name.");
            }

            if (updatedShoe.Price < 0)
            {
                throw new InvalidDataException("The price of a shoe can NOT be less than 0");
            }

            return shoeRepo.UpdateShoe(updatedShoe);
        }

        public Shoe DeleteShoe(int id)
        {
            return shoeRepo.DeleteShoe(id);
        }

        public List<Shoe> AllFilteredShoes(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("Current page and items pr page must be more than 0 or 0");
            }

            if ((filter.CurrentPage - 1) * filter.ItemsPrPage >= shoeRepo.Count())
            {
                throw new InvalidDataException("Index out of bound to high page");
            }
            return shoeRepo.ReadAllShoes(filter);
        }
    }
}