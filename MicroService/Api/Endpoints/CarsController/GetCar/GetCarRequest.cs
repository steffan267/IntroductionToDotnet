using System.ComponentModel.DataAnnotations;

namespace Api.Endpoints.CarsController.GetCar;

public class GetCarRequest
{
    [Required] //this attribute means that it must be present in the request json e.g  {  Id = null } is acceptable, while { } would be 422.
    public string Id { get; set; } = null!;
}