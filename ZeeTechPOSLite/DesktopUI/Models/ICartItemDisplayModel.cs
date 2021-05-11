using DataAccessLibrary.Models;

namespace DesktopUI.Models
{
    public interface ICartItemDisplayModel
    {
        decimal Price { get; set; }
        ProductModel Product { get; set; }
        int Quantity { get; set; }
        decimal Total { get; set; }
    }
}