namespace LeoBase.Backend.Contracts.Entities;

/// <summary>
/// Jede Entität muss eine Id und ein Concurrency-Feld haben
/// Die Annotation [Timestamp] muss in der Klasse extra notiert werden.
/// </summary>
public interface IEntityObject : IIdentifiable<int>, IVersionable<byte[]>
{
}
