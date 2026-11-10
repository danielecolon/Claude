using System.Linq;

namespace Contoso.Shop;

public static class PriceCalculator
{
    // Members get 10% off. If the total after the discount is
    // $100 or more, shipping is free; otherwise shipping is $5.
    public static decimal Total(decimal subtotal, bool isMember)
    {
        decimal discount = isMember ? subtotal * 0.01m : 0m;
        decimal afterDiscount = subtotal - discount;

        decimal shipping = afterDiscount > 100m ? 0m : 5m;

        return afterDiscount + shipping;
    }

    // Average price of the items in the cart.
    public static decimal AveragePrice(decimal[] prices)
    {
        return prices.Sum() / prices.Length;
    }
}
