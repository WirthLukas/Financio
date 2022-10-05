namespace LeoBase.Backend.Contracts.Entities;

public interface IVersionable<TVersion>
{
    /// <summary>
    /// Die Version dieses Datenbank-Objektes. Diese Version wird beim Erzeugen (Insert) 
    /// automatisch und mit jeder Änderung neu gesetzt. 
    /// </summary>
    TVersion RowVersion { get; set; }
}
