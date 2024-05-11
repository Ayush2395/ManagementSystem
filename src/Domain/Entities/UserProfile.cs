namespace ManagementSystem.Domain.Entities;
public class UserProfile : BaseEntity<string>
{
    private string? _firstName { get; set; }
    public string? FirstName
    {
        get => _firstName; set
        {
            _firstName = value;
            if (_firstName is not null || value is not null)
            {
                AddDomainEvent(new UserCreatedEvent(this));
            }
        }
    }
    public string? LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CountryId { get; set; }
    public string? StateId { get; set; }
    public string? CityId { get; set; }
    public Country Country { get; set; } = null!;
    public State State { get; set; } = null!;
    public City Cities { get; set; } = null!;
}
