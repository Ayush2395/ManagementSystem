namespace ManagementSystem.Domain.Entities;
public class Country : BaseEntity<string>
{
    public string? CountryName { get; set; }
    public bool? Status { get; set; }
    public ICollection<State> States { get; set; } = [];
    public ICollection<UserProfile> UserProfiles { get; set; } = [];
}
