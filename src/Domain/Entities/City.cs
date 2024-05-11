namespace ManagementSystem.Domain.Entities;
public class City : BaseEntity<string>
{
    public string? StateId { get; set; }
    public string? CityName { get; set; }
    public bool? Status { get; set; }
    public State State { get; set; } = null!;
    public ICollection<UserProfile> UserProfiles { get; set; } = [];
}
