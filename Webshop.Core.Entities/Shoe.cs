namespace Webshop.Core.Entities
{
    public class Shoe
    {

        public int productid { get; set; } //Unique Id for the product
        public string ProductName { get; set; } //Name of the product
        public double Size { get; set; } //Size of the product
        public string Color { get; set; } //Color of the product
        public double Price { get; set; } //Price of the product
        public string Type { get; set; } //Men, Women or Kids shoe
        public Order Order { get; set; } // The order that the product is a part of

    }
}