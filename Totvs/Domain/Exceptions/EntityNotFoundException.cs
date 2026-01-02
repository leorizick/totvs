namespace Totvs.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public string EntityId { get; }

        public EntityNotFoundException(string entityName, string entityId)
            : base($"{entityName} with id '{entityId}' not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
