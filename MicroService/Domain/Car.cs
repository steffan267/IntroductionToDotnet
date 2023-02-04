namespace Domain;

/*
 * This is our domain model that we should protect as much as possible.
 * We do not expose it outside of our domain without using integration models / view models / dto's
 */
public class Car
{
    public string? Id { get; set; }
}