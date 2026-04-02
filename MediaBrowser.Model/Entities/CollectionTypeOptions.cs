#pragma warning disable SA1300 // Lowercase required for backwards compat.

namespace MediaBrowser.Model.Entities;

/// <summary>
/// The collection type options.
/// </summary>
public enum CollectionTypeOptions
{
    /// <summary>
    /// TV Shows.
    /// </summary>
    tvshows = 1,

    /// <summary>
    /// Music.
    /// </summary>
    music = 2,

    /// <summary>
    /// Music Videos.
    /// </summary>
    musicvideos = 3,

    /// <summary>
    /// Books.
    /// </summary>
    books = 6,

    /// <summary>
    /// Mixed Movies and TV Shows.
    /// </summary>
    mixed = 7
}
