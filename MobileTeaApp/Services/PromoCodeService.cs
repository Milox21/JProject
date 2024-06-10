using MobileTeaApp.Models;

namespace MobileTeaApp.Services
{
    internal class PromoCodeService
    {
        public string ApplyPromoCode(Tea tea)
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
