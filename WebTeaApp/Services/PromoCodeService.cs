using WebTeaApp.Models;

namespace WebTeaApp.Services
{
    internal class PromoCodeService
    {
        public static string ApplyPromoCode(Tea tea)
        {
            string priceString;
            decimal price;

            if (tea.PromotionCodeTea != null)
            {
                price = tea.Price - (tea.Price * ((decimal)tea.PromotionCodeTea.PromoAmount / 100));
                priceString = price.ToString("C") + " - " + tea.PromotionCodeTea.PromoCode + "  - " + tea.PromotionCodeTea.PromoAmount + "%";
            }
            else
            {
                price = tea.Price;
                priceString = price.ToString("C");
            }

            return priceString;
        }
    }
}
