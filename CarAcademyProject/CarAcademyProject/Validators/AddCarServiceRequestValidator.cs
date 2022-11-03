using CarAcademyProjectModels.Request;
using FluentValidation;

namespace CarAcademyProject.Validators
{
    public class AddCarServiceRequestValidator : AbstractValidator<CarServiceRquest>
    {
        public AddCarServiceRequestValidator()
        {
            RuleFor(x => x.MDifficult).NotEmpty().IsInEnum();
            RuleFor(x => x.ManipulationDescription).NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(x => x.CarPlateNumber).NotEmpty().Matches("^[A-Z]{2}[0-9]{4}[A-Z]{2}$");
            RuleFor(x => x.ClientName).NotEmpty().Matches("^[A-Z]{1}[a-z]*\\s[A-Z]{1}[a-z]*$");
        }
    }
}
