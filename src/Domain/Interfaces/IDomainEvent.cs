namespace ManagementSystem.Domain.Interfaces;
public interface IDomainEvent
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    void AddDomainEvent(BaseEvent domainEvent);
    void ClearDomainEvents();
    void RemoveDomainEvent(BaseEvent domainEvent);

}
