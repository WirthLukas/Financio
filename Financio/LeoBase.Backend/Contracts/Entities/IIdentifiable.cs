namespace LeoBase.Backend.Contracts.Entities;

public interface IIdentifiable<TId>
{
    /// <summary>
    /// Eindeutige Identitaet des Objektes.
    /// </summary>
    TId Id { get; set; }
}
