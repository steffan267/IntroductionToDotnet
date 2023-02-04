namespace Domain;

public static class CarExtensions
{
    /// <summary>
    /// Static methods are stateless ways to introduce fluent language (using LINQ extensions)
    /// </summary>
    public static string GetId(this Car car)
    {
        /*
         *  This essentially means that every time we an object type "Car", you can now invoke car.GetId(). 
         *  Extension methods have a limited usage, as they are harder to test - and most often than not
         *  They are supposed to be method on the object itself.
         */
        
        return car.Id!;
    }
}