using FluentValidation;

namespace Api.Endpoints.CarsController.GetCar;

public class GetCarRequestValidator : AbstractValidator<GetCarRequest>
{
    public GetCarRequestValidator()
    {
        RuleFor(car => car.Id).NotEmpty();
    }
}