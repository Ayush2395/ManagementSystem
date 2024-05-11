namespace ManagementSystem.Domain.Events;
public record UserCreatedEvent(UserProfile User) : BaseEvent;
