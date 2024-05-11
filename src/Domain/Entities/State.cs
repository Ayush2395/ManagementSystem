namespace ManagementSystem.Domain.Entities;
public class State : BaseEntity<string>
{
    public string? CountryId { get; set; }
    public string? StateName { get; set; }
    public bool? Status { get; set; }
    public Country Country { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = [];
    public ICollection<UserProfile> UserProfiles { get; set; } = [];
}
