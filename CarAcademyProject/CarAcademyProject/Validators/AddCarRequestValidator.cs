using CarAcademyProjectModels.Request;
using FluentValidation;

namespace CarAcademyProject.Validators
{
    public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarRequestValidator()
        {
            RuleFor(x => x.Model).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.PlateNumber).NotEmpty().Matches("^[A-Z]{2}[0-9]{4}[A-Z]{2}$");
        }
    }
}
