using DomainObjects;
using FluentValidation;

namespace BLL.Validators
{
    public class HouseValidator : AbstractValidator<House>
    {
        public HouseValidator()
        {
            RuleFor(house => house.Address).NotEmpty().WithMessage("Адрес не может быть пустым");
        }
    }
}
