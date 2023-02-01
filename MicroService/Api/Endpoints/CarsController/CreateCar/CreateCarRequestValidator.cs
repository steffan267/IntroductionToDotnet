using FluentValidation;

namespace Api.Endpoints.CarsController.CreateCar;

public class CreateCarRequestValidator : AbstractValidator<CreateCarRequest>
{
    public CreateCarRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}