
using BeFaster.Core.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs.Validators
{
    public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
    {
        private readonly IProductRepository _repository;
        private Dictionary<string, IProduct> _productLookup;

        public CheckoutCommandValidator(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Skus)
                .NotNull()                
                .WithErrorCode(ValidationErrors.SKUSIsRequired.Code.ToString())
                .WithMessage(ValidationErrors.SKUSIsRequired.Message);

            RuleFor(x => ValidSKUs(x.Skus)==false)
                .Equal(false)                
                .WithErrorCode(ValidationErrors.SKUSIsInvalid.Code.ToString())
                .WithMessage(ValidationErrors.SKUSIsInvalid.Message);
        }

        public async Task<bool> ValidSku(string sku)
        {
            if (_productLookup == null)
            {
                var validSkus = await _repository.GetAll();
                _productLookup = validSkus.ToDictionary(k => k.Sku, v => v);                
            }

            return _productLookup.ContainsKey(sku);

        }

        public IEnumerable<dynamic> GetCart(string skus)
        {
            return skus.GroupBy(c => c).Select(c => new { Sku = c.Key, Count = c.Count() });
        }

        public bool ValidSKUs(string skus)
        {
            bool validSkus = true;

            var cartItems = GetCart(skus);
            cartItems.ToList().ForEach(async sku =>
            {
                if(!await ValidSku(sku.Sku.ToString()))
                {
                    validSkus = false;
                    return;
                }
            });

            return validSkus;
        }
    }
}
