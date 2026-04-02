#pragma warning disable SA1300 // The name of a C# element does not begin with an upper-case letter. - disabled due to legacy requirement.
using Jellyfin.Data.Attributes;

namespace Jellyfin.Data.Enums;

/// <summary>
/// Collection type.
/// </summary>
public enum CollectionType
{
    /// <summary>
    /// Unknown collection.
    /// </summary>
    unknown = 0,

    /// <summary>
    /// Tv shows collection.
    /// </summary>
    tvshows = 2,

    /// <summary>
    /// Music collection.
    /// </summary>
    music = 3,

    /// <summary>
    /// Music videos collection.
    /// </summary>
    musicvideos = 4,

    /// <summary>
    /// Books collection.
    /// </summary>
    books = 8,

    /// <summary>
    /// Photos collection.
    /// </summary>
    photos = 9,

    /// <summary>
    /// Playlists collection.
    /// </summary>
    playlists = 11,

    /// <summary>
    /// Folders collection.
    /// </summary>
    folders = 12,

    /// <summary>
    /// Tv show series collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvshowseries = 101,

    /// <summary>
    /// Tv genres collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvgenres = 102,

    /// <summary>
    /// Tv genre collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvgenre = 103,

    /// <summary>
    /// Tv latest collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvlatest = 104,

    /// <summary>
    /// Tv next up collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvnextup = 105,

    /// <summary>
    /// Tv resume collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvresume = 106,

    /// <summary>
    /// Tv favorite series collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvfavoriteseries = 107,

    /// <summary>
    /// Tv favorite episodes collection.
    /// </summary>
    [OpenApiIgnoreEnum]
    tvfavoriteepisodes = 108
}
