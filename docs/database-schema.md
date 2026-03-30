# Jellyfin Database Schema

## Overview

Jellyfin uses a **SQLite** database managed via **Entity Framework Core**. The database file is located at `<data-dir>/data/jellyfin.db`.

The canonical source of truth for the schema is [`JellyfinDbContext.cs`](../src/Jellyfin.Database/Jellyfin.Database.Implementations/JellyfinDbContext.cs), which exposes all DbSets. Entity definitions live in [`Entities/`](../src/Jellyfin.Database/Jellyfin.Database.Implementations/Entities/) and Fluent API configurations in [`ModelConfiguration/`](../src/Jellyfin.Database/Jellyfin.Database.Implementations/ModelConfiguration/).

### Tables by Domain

| Domain | Tables |
|---|---|
| **User Management** (9) | Users, Permissions, Preferences, AccessSchedules, ImageInfos, DisplayPreferences, ItemDisplayPreferences, CustomItemDisplayPreferences, HomeSections |
| **Media Library** (12) | BaseItems, AncestorIds, Chapters, MediaStreamInfos, AttachmentStreamInfos, BaseItemProviders, BaseItemImageInfos, BaseItemMetadataFields, BaseItemTrailerTypes, KeyframeData, TrickplayInfos, MediaSegments |
| **Normalized Values** (2) | ItemValues, ItemValuesMap |
| **People** (2) | Peoples, PeopleBaseItemMap |
| **User–Item Interaction** (1) | UserData |
| **System** (4) | ActivityLogs, ApiKeys, Devices, DeviceOptions |

---

## Entity-Relationship Diagram

```mermaid
erDiagram

    %% ──────────────────────────────────────
    %% User Management
    %% ──────────────────────────────────────

    Users {
        Guid Id PK
        string Username UK
        string Password
        string AuthenticationProviderId
        string PasswordResetProviderId
        bool MustUpdatePassword
        bool EnableLocalPassword
        bool EnableAutoLogin
        int InvalidLoginAttemptCount
        int MaxActiveSessions
        DateTime LastLoginDate
        DateTime LastActivityDate
        bool RememberAudioSelections
        bool RememberSubtitleSelections
        SubtitlePlaybackMode SubtitleMode
        bool HidePlayedInLatest
        bool DisplayMissingEpisodes
        bool EnableNextEpisodeAutoPlay
        bool PlayDefaultAudioTrack
        string AudioLanguagePreference
        string SubtitleLanguagePreference
        int MaxParentalRatingScore
        int MaxParentalRatingSubScore
        int RemoteClientBitrateLimit
        string CastReceiverId
        SyncPlayUserAccessType SyncPlayAccess
        long InternalId
        uint RowVersion
    }

    Permissions {
        int Id PK
        Guid UserId FK
        PermissionKind Kind
        bool Value
        uint RowVersion
    }

    Preferences {
        int Id PK
        Guid UserId FK
        PreferenceKind Kind
        string Value
        uint RowVersion
    }

    AccessSchedules {
        int Id PK
        Guid UserId FK
        DynamicDayOfWeek DayOfWeek
        double StartHour
        double EndHour
    }

    ImageInfos {
        int Id PK
        Guid UserId FK
        string Path
        DateTime LastModified
    }

    DisplayPreferences {
        int Id PK
        Guid UserId FK
        Guid ItemId
        string Client
        bool ShowSidebar
        bool ShowBackdrop
        ScrollDirection ScrollDirection
        int SkipForwardLength
        int SkipBackwardLength
        ChromecastVersion ChromecastVersion
        bool EnableNextVideoInfoOverlay
        string DashboardTheme
        string TvHome
        IndexingKind IndexBy
    }

    HomeSections {
        int Id PK
        int DisplayPreferencesId FK
        int Order
        HomeSectionType Type
    }

    ItemDisplayPreferences {
        int Id PK
        Guid UserId FK
        Guid ItemId
        string Client
        ViewType ViewType
        bool RememberIndexing
        IndexingKind IndexBy
        bool RememberSorting
        string SortBy
        SortOrder SortOrder
    }

    CustomItemDisplayPreferences {
        int Id PK
        Guid UserId
        Guid ItemId
        string Client
        string Key
        string Value
    }

    %% ──────────────────────────────────────
    %% Media Library
    %% ──────────────────────────────────────

    BaseItems {
        Guid Id PK
        string Type
        string Name
        string SortName
        string ForcedSortName
        string OriginalTitle
        string Path
        string Overview
        string Tagline
        bool IsFolder
        bool IsVirtualItem
        bool IsMovie
        bool IsSeries
        bool IsRepeat
        bool IsLocked
        bool IsInMixedFolder
        string MediaType
        Guid ParentId FK
        Guid TopParentId
        Guid SeriesId
        Guid SeasonId
        Guid OwnerId
        string PresentationUniqueKey
        string PrimaryVersionId
        string SeriesPresentationUniqueKey
        string SeriesName
        string SeasonName
        string EpisodeTitle
        string ExternalSeriesId
        string ShowId
        int IndexNumber
        int ParentIndexNumber
        int ProductionYear
        float CommunityRating
        float CriticRating
        string OfficialRating
        string CustomRating
        string CleanName
        long RunTimeTicks
        DateTime PremiereDate
        DateTime DateCreated
        DateTime DateModified
        DateTime DateLastRefreshed
        DateTime DateLastSaved
        DateTime DateLastMediaAdded
        DateTime StartDate
        DateTime EndDate
        string Genres
        string Studios
        string Tags
        string ProductionLocations
        string ExtraIds
        int ExtraType
        string Artists
        string AlbumArtists
        string Album
        string ExternalId
        string ExternalServiceId
        float LUFS
        float NormalizationGain
        int TotalBitrate
        int Width
        int Height
        long Size
        int Audio
        int InheritedParentalRatingValue
        int InheritedParentalRatingSubValue
        string UnratedType
        string ChannelId
        string Data
    }

    AncestorIds {
        Guid ItemId PK_FK
        Guid ParentItemId PK_FK
    }

    Chapters {
        Guid ItemId PK_FK
        int ChapterIndex PK
        long StartPositionTicks
        string Name
        string ImagePath
        DateTime ImageDateModified
    }

    MediaStreamInfos {
        Guid ItemId PK_FK
        int StreamIndex PK
        int StreamType
        string Codec
        string Language
        string ChannelLayout
        string Profile
        string AspectRatio
        string Path
        bool IsInterlaced
        int BitRate
        int Channels
        int SampleRate
        bool IsDefault
        bool IsForced
        bool IsExternal
        int Height
        int Width
        float AverageFrameRate
        float RealFrameRate
        float Level
        string PixelFormat
        int BitDepth
        bool IsAnamorphic
        int RefFrames
        string CodecTag
        string Comment
        string NalLengthSize
        bool IsAvc
        string Title
        string TimeBase
        string CodecTimeBase
        string ColorPrimaries
        string ColorSpace
        string ColorTransfer
        int DvVersionMajor
        int DvVersionMinor
        int DvProfile
        int DvLevel
        int RpuPresentFlag
        int ElPresentFlag
        int BlPresentFlag
        int DvBlSignalCompatibilityId
        bool IsHearingImpaired
        int Rotation
        string KeyFrames
        int Hdr10PlusPresentFlag
    }

    AttachmentStreamInfos {
        Guid ItemId PK_FK
        int Index PK
        string Codec
        string CodecTag
        string Comment
        string Filename
        string MimeType
    }

    BaseItemProviders {
        Guid ItemId PK_FK
        string ProviderId PK
        string ProviderValue
    }

    BaseItemImageInfos {
        Guid Id PK
        Guid ItemId FK
        string Path
        DateTime DateModified
        int ImageType
        int Width
        int Height
        blob Blurhash
    }

    BaseItemMetadataFields {
        int Id PK
        Guid ItemId PK_FK
    }

    BaseItemTrailerTypes {
        int Id PK
        Guid ItemId PK_FK
    }

    KeyframeData {
        Guid ItemId PK_FK
        long TotalDuration
        string KeyframeTicks
    }

    TrickplayInfos {
        Guid ItemId PK_FK
        int Width PK
        int Bandwidth
        int Height
        int Interval
        int ThumbnailCount
        int TileHeight
        int TileWidth
    }

    MediaSegments {
        Guid Id PK
        Guid ItemId FK
        MediaSegmentType Type
        long StartTicks
        long EndTicks
        string SegmentProviderId
    }

    %% ──────────────────────────────────────
    %% Normalized Values
    %% ──────────────────────────────────────

    ItemValues {
        Guid ItemValueId PK
        int Type
        string Value UK
        string CleanValue
    }

    ItemValuesMap {
        Guid ItemValueId PK_FK
        Guid ItemId PK_FK
    }

    %% ──────────────────────────────────────
    %% People
    %% ──────────────────────────────────────

    Peoples {
        Guid Id PK
        string Name
        string PersonType
    }

    PeopleBaseItemMap {
        Guid ItemId PK_FK
        Guid PeopleId PK_FK
        string Role PK
        int ListOrder
        int SortOrder
    }

    %% ──────────────────────────────────────
    %% User-Item Interaction
    %% ──────────────────────────────────────

    UserData {
        Guid ItemId PK_FK
        Guid UserId PK
        string CustomDataKey PK
        float Rating
        long PlaybackPositionTicks
        int PlayCount
        bool IsFavorite
        DateTime LastPlayedDate
        bool Played
        int AudioStreamIndex
        int SubtitleStreamIndex
        bool Likes
        DateTime RetentionDate
    }

    %% ──────────────────────────────────────
    %% System
    %% ──────────────────────────────────────

    ActivityLogs {
        int Id PK
        string Name
        string Overview
        string ShortOverview
        string Type
        Guid UserId
        string ItemId
        DateTime DateCreated
        LogLevel LogSeverity
        uint RowVersion
    }

    ApiKeys {
        int Id PK
        DateTime DateCreated
        DateTime DateLastActivity
        string Name
        string AccessToken UK
    }

    Devices {
        int Id PK
        Guid UserId FK
        string AccessToken
        string AppName
        string AppVersion
        string DeviceName
        string DeviceId
        bool IsActive
        DateTime DateCreated
        DateTime DateModified
        DateTime DateLastActivity
    }

    DeviceOptions {
        int Id PK
        string DeviceId UK
        string CustomName
    }

    %% ──────────────────────────────────────
    %% Relationships
    %% ──────────────────────────────────────

    Users ||--o{ Permissions : "has"
    Users ||--o{ Preferences : "has"
    Users ||--o{ AccessSchedules : "has"
    Users ||--o| ImageInfos : "profile image"
    Users ||--o{ DisplayPreferences : "has"
    Users ||--o{ ItemDisplayPreferences : "has"
    Users ||--o{ Devices : "owns"

    DisplayPreferences ||--o{ HomeSections : "contains"

    BaseItems ||--o{ BaseItems : "parent-child"
    BaseItems ||--o{ AncestorIds : "ancestor of"
    BaseItems ||--o{ Chapters : "has"
    BaseItems ||--o{ MediaStreamInfos : "has"
    BaseItems ||--o{ AttachmentStreamInfos : "has"
    BaseItems ||--o{ BaseItemProviders : "identified by"
    BaseItems ||--o{ BaseItemImageInfos : "has"
    BaseItems ||--o{ BaseItemMetadataFields : "locks"
    BaseItems ||--o{ BaseItemTrailerTypes : "has"
    BaseItems ||--o| KeyframeData : "has"
    BaseItems ||--o{ TrickplayInfos : "has"
    BaseItems ||--o{ MediaSegments : "has"
    BaseItems ||--o{ UserData : "tracked by"
    BaseItems ||--o{ ItemValuesMap : "tagged with"
    BaseItems ||--o{ PeopleBaseItemMap : "features"

    ItemValues ||--o{ ItemValuesMap : "applied to"
    Peoples ||--o{ PeopleBaseItemMap : "appears in"
```

---

## Table Reference

### User Management

#### Users

Central user account table. Every user-facing entity cascades from here.

| Column | Type | Constraints |
|---|---|---|
| Id | Guid | **PK** |
| Username | string(255) | **Unique**, Required |
| Password | string(65535) | Nullable |
| AuthenticationProviderId | string(255) | Required |
| PasswordResetProviderId | string(255) | Required |
| MustUpdatePassword | bool | |
| EnableLocalPassword | bool | |
| EnableAutoLogin | bool | |
| InvalidLoginAttemptCount | int | |
| LoginAttemptsBeforeLockout | int? | |
| MaxActiveSessions | int | |
| LastLoginDate | DateTime? | |
| LastActivityDate | DateTime? | |
| RememberAudioSelections | bool | |
| RememberSubtitleSelections | bool | |
| SubtitleMode | SubtitlePlaybackMode | |
| HidePlayedInLatest | bool | |
| DisplayMissingEpisodes | bool | |
| DisplayCollectionsView | bool | |
| EnableNextEpisodeAutoPlay | bool | |
| EnableUserPreferenceAccess | bool | |
| PlayDefaultAudioTrack | bool | |
| AudioLanguagePreference | string(255)? | |
| SubtitleLanguagePreference | string(255)? | |
| MaxParentalRatingScore | int? | |
| MaxParentalRatingSubScore | int? | |
| RemoteClientBitrateLimit | int? | |
| InternalId | long | |
| CastReceiverId | string(32)? | |
| SyncPlayAccess | SyncPlayUserAccessType | |
| RowVersion | uint | Concurrency token |

**Indexes:** `Username` (unique)

---

#### Permissions

Per-user permission flags (e.g. IsAdministrator, EnableMediaPlayback).

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid? | **FK → Users**, Cascade delete |
| Kind | PermissionKind | |
| Value | bool | |
| RowVersion | uint | Concurrency token |

**Indexes:** `(UserId, Kind)` unique, filtered on `UserId IS NOT NULL`

---

#### Preferences

Per-user preference key-value pairs (e.g. ordered views, blocked tags).

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid? | **FK → Users**, Cascade delete |
| Kind | PreferenceKind | |
| Value | string(65535) | Required |
| RowVersion | uint | Concurrency token |

**Indexes:** `(UserId, Kind)` unique, filtered on `UserId IS NOT NULL`

---

#### AccessSchedules

Time-of-day access windows per user.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid | **FK → Users**, Cascade delete |
| DayOfWeek | DynamicDayOfWeek | |
| StartHour | double | |
| EndHour | double | |

---

#### ImageInfos

User profile images (one-to-one with Users).

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid? | **FK → Users**, Cascade delete |
| Path | string(512) | Required |
| LastModified | DateTime | |

---

#### DisplayPreferences

Per-user, per-item, per-client UI display settings.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid | **FK → Users**, Cascade delete |
| ItemId | Guid | |
| Client | string(32) | Required |
| ShowSidebar | bool | |
| ShowBackdrop | bool | |
| ScrollDirection | ScrollDirection | |
| SkipForwardLength | int | |
| SkipBackwardLength | int | |
| ChromecastVersion | ChromecastVersion | |
| EnableNextVideoInfoOverlay | bool | |
| DashboardTheme | string(32)? | |
| TvHome | string(32)? | |
| IndexBy | IndexingKind? | |

**Indexes:** `(UserId, ItemId, Client)` unique

---

#### HomeSections

Ordered list of home-screen sections per display preference.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| DisplayPreferencesId | int | **FK → DisplayPreferences**, Cascade delete |
| Order | int | |
| Type | HomeSectionType | |

---

#### ItemDisplayPreferences

Per-user, per-item, per-client view/sort preferences for library views.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid | **FK → Users**, Cascade delete |
| ItemId | Guid | |
| Client | string(32) | Required |
| ViewType | ViewType | |
| RememberIndexing | bool | |
| IndexBy | IndexingKind? | |
| RememberSorting | bool | |
| SortBy | string(64) | Default `"SortName"` |
| SortOrder | SortOrder | Default `Ascending` |

---

#### CustomItemDisplayPreferences

Extensible key-value display preferences per user/item/client.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid | |
| ItemId | Guid | |
| Client | string(32) | Required |
| Key | string | Required |
| Value | string? | |

**Indexes:** `(UserId, ItemId, Client, Key)` unique

---

### Media Library

#### BaseItems

Core media item table. Every movie, episode, series, season, folder, and special is a row here. Self-referential via `ParentId`.

| Column | Type | Constraints |
|---|---|---|
| Id | Guid | **PK** |
| Type | string | Required |
| Name | string? | |
| SortName | string? | |
| ForcedSortName | string? | |
| OriginalTitle | string? | |
| Path | string? | |
| Overview | string? | |
| Tagline | string? | |
| IsFolder | bool | |
| IsVirtualItem | bool | |
| IsMovie | bool | |
| IsSeries | bool | |
| IsRepeat | bool | |
| IsLocked | bool | |
| IsInMixedFolder | bool | |
| MediaType | string? | |
| ParentId | Guid? | **FK → BaseItems (self)**, Cascade delete |
| TopParentId | Guid? | |
| SeriesId | Guid? | |
| SeasonId | Guid? | |
| OwnerId | Guid? | |
| PresentationUniqueKey | string? | |
| PrimaryVersionId | string? | |
| SeriesPresentationUniqueKey | string? | |
| SeriesName | string? | |
| SeasonName | string? | |
| EpisodeTitle | string? | |
| ExternalSeriesId | string? | |
| ShowId | string? | |
| IndexNumber | int? | |
| ParentIndexNumber | int? | |
| ProductionYear | int? | |
| CommunityRating | float? | |
| CriticRating | float? | |
| OfficialRating | string? | |
| CustomRating | string? | |
| CleanName | string? | |
| RunTimeTicks | long? | |
| PremiereDate | string? | |
| DateCreated | string? | |
| DateModified | string? | |
| DateLastRefreshed | string? | |
| DateLastSaved | string? | |
| DateLastMediaAdded | string? | |
| StartDate | string? | |
| EndDate | string? | |
| Genres | string? | |
| Studios | string? | |
| Tags | string? | |
| ProductionLocations | string? | |
| ExtraIds | string? | |
| ExtraType | int? | |
| Artists | string? | |
| AlbumArtists | string? | |
| Album | string? | |
| ExternalId | string? | |
| ExternalServiceId | string? | |
| LUFS | float? | |
| NormalizationGain | float? | |
| TotalBitrate | int? | |
| Width | int? | |
| Height | int? | |
| Size | long? | |
| Audio | int? | |
| InheritedParentalRatingValue | int? | |
| InheritedParentalRatingSubValue | int? | |
| UnratedType | string? | |
| ChannelId | string? | |
| Data | string? | |

**Indexes:**
- `Path`
- `ParentId`
- `PresentationUniqueKey`
- `(Id, Type, IsFolder, IsVirtualItem)`
- `(TopParentId, Id)`
- `(Type, SeriesPresentationUniqueKey, PresentationUniqueKey, SortName)`
- `(Type, SeriesPresentationUniqueKey, IsFolder, IsVirtualItem)`
- `(Type, TopParentId, StartDate)`
- `(Type, TopParentId, Id)`
- `(Type, TopParentId, PresentationUniqueKey)`
- `(Type, TopParentId, IsVirtualItem, PresentationUniqueKey, DateCreated)`
- `(IsFolder, TopParentId, IsVirtualItem, PresentationUniqueKey, DateCreated)`
- `(MediaType, TopParentId, IsVirtualItem, PresentationUniqueKey)`

**Seed Data:** Placeholder row `00000000-0000-0000-0000-000000000001`

---

#### AncestorIds

Transitive parent-child hierarchy for fast ancestor queries (e.g. "all items under library X").

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| ParentItemId | Guid | **PK**, **FK → BaseItems** |

**Indexes:** `ParentItemId`

---

#### Chapters

Scene/chapter markers within a media item.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| ChapterIndex | int | **PK** |
| StartPositionTicks | long | Required |
| Name | string? | |
| ImagePath | string? | |
| ImageDateModified | DateTime? | |

---

#### MediaStreamInfos

Audio, video, and subtitle streams within a media file.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| StreamIndex | int | **PK** |
| StreamType | int | |
| Codec | string? | |
| Language | string? | |
| ChannelLayout | string? | |
| Profile | string? | |
| AspectRatio | string? | |
| Path | string? | |
| IsInterlaced | bool | |
| BitRate | int? | |
| Channels | int? | |
| SampleRate | int? | |
| IsDefault | bool | |
| IsForced | bool | |
| IsExternal | bool | |
| Height | int? | |
| Width | int? | |
| AverageFrameRate | float? | |
| RealFrameRate | float? | |
| Level | float? | |
| PixelFormat | string? | |
| BitDepth | int? | |
| IsAnamorphic | bool? | |
| RefFrames | int? | |
| CodecTag | string? | |
| Comment | string? | |
| NalLengthSize | string? | |
| IsAvc | bool? | |
| Title | string? | |
| TimeBase | string? | |
| CodecTimeBase | string? | |
| ColorPrimaries | string? | |
| ColorSpace | string? | |
| ColorTransfer | string? | |
| DvVersionMajor | int? | |
| DvVersionMinor | int? | |
| DvProfile | int? | |
| DvLevel | int? | |
| RpuPresentFlag | int? | |
| ElPresentFlag | int? | |
| BlPresentFlag | int? | |
| DvBlSignalCompatibilityId | int? | |
| IsHearingImpaired | bool | |
| Rotation | int? | |
| KeyFrames | string? | |
| Hdr10PlusPresentFlag | int? | |

**Indexes:** `StreamIndex`, `StreamType`, `(StreamIndex, StreamType)`, `(StreamIndex, StreamType, Language)`

---

#### AttachmentStreamInfos

Embedded attachments (fonts, cover art, etc.) in a media file.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| Index | int | **PK** |
| Codec | string? | |
| CodecTag | string? | |
| Comment | string? | |
| Filename | string? | |
| MimeType | string? | |

---

#### BaseItemProviders

External provider IDs (IMDB, TMDB, TVDB, AniDB, etc.) linked to items.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| ProviderId | string | **PK** |
| ProviderValue | string | Required |

**Indexes:** `(ProviderId, ProviderValue, ItemId)`

---

#### BaseItemImageInfos

Image metadata for items (poster, backdrop, banner, etc.).

| Column | Type | Constraints |
|---|---|---|
| Id | Guid | **PK** |
| ItemId | Guid | **FK → BaseItems** |
| Path | string | Required |
| DateModified | DateTime? | |
| ImageType | int | |
| Width | int | |
| Height | int | |
| Blurhash | blob? | |

**Indexes:** `ItemId`

---

#### BaseItemMetadataFields

Tracks which metadata fields are locked (not overwritten by providers).

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK** (MetadataField enum value) |
| ItemId | Guid | **PK**, **FK → BaseItems** |

---

#### BaseItemTrailerTypes

Tracks trailer type classification for items.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK** (TrailerType enum value) |
| ItemId | Guid | **PK**, **FK → BaseItems** |

---

#### KeyframeData

Keyframe timing data for a media item (used for seeking).

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| TotalDuration | long | Required |
| KeyframeTicks | string? | Serialized array |

---

#### TrickplayInfos

Preview thumbnail tile-sheet metadata (scrubbing thumbnails).

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| Width | int | **PK** |
| Bandwidth | int | |
| Height | int | |
| Interval | int | |
| ThumbnailCount | int | |
| TileHeight | int | |
| TileWidth | int | |

---

#### MediaSegments

Detected segments (intro, credits, preview, recap) for skip functionality.

| Column | Type | Constraints |
|---|---|---|
| Id | Guid | **PK**, Identity |
| ItemId | Guid | **FK → BaseItems** |
| Type | MediaSegmentType | Required |
| StartTicks | long | |
| EndTicks | long | |
| SegmentProviderId | string | Required |

---

### Normalized Values

#### ItemValues

Normalized lookup values for genres, studios, tags, and similar facets.

| Column | Type | Constraints |
|---|---|---|
| ItemValueId | Guid | **PK** |
| Type | int | (0=Tags, 1=InheritedTags, 2=Studios, etc.) |
| Value | string | **Unique** with Type |
| CleanValue | string | |

**Indexes:** `(Type, CleanValue)`, `(Type, Value)` unique

---

#### ItemValuesMap

Join table linking BaseItems to their normalized ItemValues.

| Column | Type | Constraints |
|---|---|---|
| ItemValueId | Guid | **PK**, **FK → ItemValues** |
| ItemId | Guid | **PK**, **FK → BaseItems** |

**Indexes:** `ItemId`

---

### People

#### Peoples

Person records (actors, directors, writers, etc.).

| Column | Type | Constraints |
|---|---|---|
| Id | Guid | **PK** |
| Name | string | Required |
| PersonType | string? | |

**Indexes:** `Name`

---

#### PeopleBaseItemMap

Many-to-many join between people and items, with role information.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| PeopleId | Guid | **PK**, **FK → Peoples** |
| Role | string | **PK** |
| ListOrder | int? | |
| SortOrder | int? | |

**Indexes:** `(ItemId, SortOrder)`, `(ItemId, ListOrder)`

---

### User–Item Interaction

#### UserData

Per-user watch state, playback progress, ratings, and favorites.

| Column | Type | Constraints |
|---|---|---|
| ItemId | Guid | **PK**, **FK → BaseItems** |
| UserId | Guid | **PK** |
| CustomDataKey | string | **PK** |
| Rating | float? | |
| PlaybackPositionTicks | long | |
| PlayCount | int | |
| IsFavorite | bool | |
| LastPlayedDate | DateTime? | |
| Played | bool | |
| AudioStreamIndex | int? | |
| SubtitleStreamIndex | int? | |
| Likes | bool? | |
| RetentionDate | DateTime? | |

**Indexes:** `(ItemId, UserId, Played)`, `(ItemId, UserId, PlaybackPositionTicks)`, `(ItemId, UserId, IsFavorite)`, `(ItemId, UserId, LastPlayedDate)`

---

### System

#### ActivityLogs

Audit log of system and user activity.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| Name | string(512) | Required |
| Overview | string(512)? | |
| ShortOverview | string(512)? | |
| Type | string(256) | Required |
| UserId | Guid | |
| ItemId | string(256)? | |
| DateCreated | DateTime | Required |
| LogSeverity | LogLevel | Default `Information` |
| RowVersion | uint | Concurrency token |

**Indexes:** `DateCreated`

---

#### ApiKeys

API key tokens for third-party access.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| DateCreated | DateTime | |
| DateLastActivity | DateTime? | |
| Name | string(64) | Required |
| AccessToken | string | **Unique** |

**Indexes:** `AccessToken` unique

---

#### Devices

Registered client devices per user.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| UserId | Guid | **FK → Users** |
| AccessToken | string | Auto-generated |
| AppName | string(64) | Required |
| AppVersion | string(32) | Required |
| DeviceName | string(64) | Required |
| DeviceId | string(256) | Required |
| IsActive | bool | |
| DateCreated | DateTime | |
| DateModified | DateTime | |
| DateLastActivity | DateTime | |

**Indexes:** `(DeviceId, DateLastActivity)`, `(AccessToken, DateLastActivity)`, `(UserId, DeviceId)`, `DeviceId`

---

#### DeviceOptions

Custom naming/configuration per physical device ID.

| Column | Type | Constraints |
|---|---|---|
| Id | int | **PK**, Identity |
| DeviceId | string | **Unique** |
| CustomName | string? | |

**Indexes:** `DeviceId` unique

---

## Relationship Summary

| Parent | Child | Cardinality | FK Column(s) | On Delete |
|---|---|---|---|---|
| Users | Permissions | One-to-many | `Permissions.UserId` | Cascade |
| Users | Preferences | One-to-many | `Preferences.UserId` | Cascade |
| Users | AccessSchedules | One-to-many | `AccessSchedules.UserId` | Cascade |
| Users | ImageInfos | One-to-one | `ImageInfos.UserId` | Cascade |
| Users | DisplayPreferences | One-to-many | `DisplayPreferences.UserId` | Cascade |
| Users | ItemDisplayPreferences | One-to-many | `ItemDisplayPreferences.UserId` | Cascade |
| Users | Devices | One-to-many | `Devices.UserId` | — |
| DisplayPreferences | HomeSections | One-to-many | `HomeSections.DisplayPreferencesId` | Cascade |
| BaseItems | BaseItems | Self-referential | `BaseItems.ParentId` | Cascade |
| BaseItems | AncestorIds (as Item) | One-to-many | `AncestorIds.ItemId` | — |
| BaseItems | AncestorIds (as Parent) | One-to-many | `AncestorIds.ParentItemId` | — |
| BaseItems | Chapters | One-to-many | `Chapters.ItemId` | — |
| BaseItems | MediaStreamInfos | One-to-many | `MediaStreamInfos.ItemId` | — |
| BaseItems | AttachmentStreamInfos | One-to-many | `AttachmentStreamInfos.ItemId` | — |
| BaseItems | BaseItemProviders | One-to-many | `BaseItemProviders.ItemId` | — |
| BaseItems | BaseItemImageInfos | One-to-many | `BaseItemImageInfos.ItemId` | — |
| BaseItems | BaseItemMetadataFields | One-to-many | `BaseItemMetadataFields.ItemId` | — |
| BaseItems | BaseItemTrailerTypes | One-to-many | `BaseItemTrailerTypes.ItemId` | — |
| BaseItems | KeyframeData | One-to-one | `KeyframeData.ItemId` | — |
| BaseItems | TrickplayInfos | One-to-many | `TrickplayInfos.ItemId` | — |
| BaseItems | MediaSegments | One-to-many | `MediaSegments.ItemId` | — |
| BaseItems | UserData | One-to-many | `UserData.ItemId` | — |
| BaseItems | ItemValuesMap | One-to-many | `ItemValuesMap.ItemId` | — |
| BaseItems | PeopleBaseItemMap | One-to-many | `PeopleBaseItemMap.ItemId` | — |
| ItemValues | ItemValuesMap | One-to-many | `ItemValuesMap.ItemValueId` | — |
| Peoples | PeopleBaseItemMap | One-to-many | `PeopleBaseItemMap.PeopleId` | — |
