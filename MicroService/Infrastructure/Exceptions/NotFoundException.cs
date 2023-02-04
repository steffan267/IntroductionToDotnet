namespace Infrastructure.Exceptions;

/*
 * Following some of the practices that allows to create as lean code as possible, we can use exceptions for their
 * actual purpose. When an exception circumstance happens, we will throw these exceptions and let them be caught
 * by the exception middleware.
 *
 * This will also mean we will also have to consider in the method definition, if something is exceptional.
 * e.g:
 * public Task<Car> GetCar(string id){
 *      databaseContext.Cars.FirstOrDefault( car => car.Id == id) ?? throw new NotFoundException();
 * }
 *
 * or a case where we know beforehand that it is acceptable that no value exists:
 *
 * public Task<Car?> GetCarOrDefault(string id){
 *      databaseContext.Cars.FirstOrDefault( car => car.Id == id);
 * }
 *
 *p.s - "OrDefault()" -> is a common method for null if no matches on criteria
 *
 * NB: this is not using exceptions to control the flow of the system, as we will only use them when the system hits
 * an unforeseen error (case by case)
 */
public class NotFoundException : Exception
{
    public NotFoundException(string s) : base(s)
    {
    }
}