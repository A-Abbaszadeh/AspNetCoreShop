using Application.Discounts.AddNewDiscountService;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.EndPoint.Binders
{
    public class DiscountEntityBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string fieldName = bindingContext.FieldName;
            AddNewDiscountDto discountDto = new AddNewDiscountDto()
            {
                Name = bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.Name)}").Values.ToString(),

                UsePercentage = bool.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.UsePercentage)}").FirstValue.ToString()),

                DiscountPercentage = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.DiscountPercentage)}").Values.ToString()),

                DiscountAmount = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.DiscountAmount)}").Values.ToString()),

                StartDate = PersianDateTime.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.StartDate)}").Values.ToString()),

                EndDate = PersianDateTime.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.EndDate)}").Values.ToString()),

                RequiredCouponCode = bool.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.RequiredCouponCode)}").FirstValue.ToString()),

                CouponCode = bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.CouponCode)}").Values.ToString(),

                DiscountTypeId = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.DiscountTypeId)}").Values.ToString()),

                LimitationTimes = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.LimitationTimes)}").Values.ToString()),

                DiscountLimitationId = int.Parse(bindingContext.ValueProvider
                    .GetValue($"{fieldName}.{nameof(discountDto.DiscountLimitationId)}").Values.ToString()),
            };

            var appliedToCatalogItem = bindingContext.ValueProvider.GetValue($"{fieldName}.{nameof(discountDto.AppliedToCatalogItem)}");
            
            if (!string.IsNullOrEmpty(appliedToCatalogItem.Values))
            {
                discountDto.AppliedToCatalogItem = appliedToCatalogItem.Values.ToString().Split(',').Select(a => Int32.Parse(a)).ToList();
            }

            bindingContext.Result = ModelBindingResult.Success(discountDto);
            return Task.CompletedTask;
        }
    }
}
