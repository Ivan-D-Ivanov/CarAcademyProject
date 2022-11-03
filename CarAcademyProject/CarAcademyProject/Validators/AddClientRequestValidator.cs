using CarAcademyProjectModels.Request;
using FluentValidation;

namespace CarAcademyProject.Validators
{
    public class AddClientRequestValidator : AbstractValidator<ClientRequest>
    {
        public AddClientRequestValidator()
        {
            RuleFor(x=>x.Age).NotEmpty().GreaterThan(18).LessThan(100);
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(25);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(25);
        }
    }
}
