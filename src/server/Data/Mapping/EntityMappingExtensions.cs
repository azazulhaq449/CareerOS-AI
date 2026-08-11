using CareerOS.Server.Data.Entities;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Data.Mapping;

/// <summary>
/// Converts between persistence entities (<see cref="Data.Entities"/>), the
/// public API read models (<see cref="Models"/>) and the write-side request
/// DTOs (<see cref="Models.Requests"/>). Kept as small manual mappers rather
/// than a mapping library — the shapes are simple and near-identical, so a
/// library would add indirection without saving meaningful code.
/// </summary>
public static class EntityMappingExtensions
{
    // ---- Entity -> read Model ----

    public static Profile ToModel(this ProfileEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Initials = entity.Initials,
        Title = entity.Title,
        Roles = entity.Roles,
        Location = entity.Location,
        Email = entity.Email,
        Phone = entity.Phone,
        PhoneHref = entity.PhoneHref,
        LinkedInUrl = entity.LinkedInUrl,
        Summary = entity.Summary,
    };

    public static Strength ToModel(this StrengthEntity entity) => new()
    {
        Id = entity.Id,
        Icon = entity.Icon,
        Title = entity.Title,
        Description = entity.Description,
    };

    public static ExperienceEntry ToModel(this ExperienceEntryEntity entity) => new()
    {
        Id = entity.Id,
        Company = entity.Company,
        Location = entity.Location,
        Role = entity.Role,
        Period = entity.Period,
        Projects = entity.Projects,
        Highlights = entity.Highlights,
    };

    public static ProjectEntry ToModel(this ProjectEntryEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Organisation = entity.Organisation,
        Icon = entity.Icon,
        Description = entity.Description,
        Tags = entity.Tags,
    };

    public static SkillGroup ToModel(this SkillGroupEntity entity) => new()
    {
        Id = entity.Id,
        Category = entity.Category,
        Icon = entity.Icon,
        Items = entity.Items,
    };

    public static Certification ToModel(this CertificationEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Date = entity.Date,
    };

    public static EducationEntry ToModel(this EducationEntryEntity entity) => new()
    {
        School = entity.School,
        Degree = entity.Degree,
        Location = entity.Location,
        Date = entity.Date,
    };

    // ---- Request DTO -> new Entity (Create) ----

    public static StrengthEntity ToEntity(this StrengthRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Icon = request.Icon,
        Title = request.Title,
        Description = request.Description,
        SortOrder = request.SortOrder,
    };

    public static ExperienceEntryEntity ToEntity(this ExperienceEntryRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Company = request.Company,
        Location = request.Location,
        Role = request.Role,
        Period = request.Period,
        Projects = request.Projects,
        Highlights = request.Highlights,
        SortOrder = request.SortOrder,
    };

    public static ProjectEntryEntity ToEntity(this ProjectEntryRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Organisation = request.Organisation,
        Icon = request.Icon,
        Description = request.Description,
        Tags = request.Tags,
        SortOrder = request.SortOrder,
    };

    public static SkillGroupEntity ToEntity(this SkillGroupRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Category = request.Category,
        Icon = request.Icon,
        Items = request.Items,
        SortOrder = request.SortOrder,
    };

    public static CertificationEntity ToEntity(this CertificationRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Date = request.Date,
        SortOrder = request.SortOrder,
    };

    // ---- Request DTO -> existing Entity (Update, in place) ----

    public static void ApplyTo(this ProfileRequest request, ProfileEntity entity)
    {
        entity.Name = request.Name;
        entity.Initials = request.Initials;
        entity.Title = request.Title;
        entity.Roles = request.Roles;
        entity.Location = request.Location;
        entity.Email = request.Email;
        entity.Phone = request.Phone;
        entity.PhoneHref = request.PhoneHref;
        entity.LinkedInUrl = request.LinkedInUrl;
        entity.Summary = request.Summary;
    }

    public static void ApplyTo(this StrengthRequest request, StrengthEntity entity)
    {
        entity.Icon = request.Icon;
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.SortOrder = request.SortOrder;
    }

    public static void ApplyTo(this ExperienceEntryRequest request, ExperienceEntryEntity entity)
    {
        entity.Company = request.Company;
        entity.Location = request.Location;
        entity.Role = request.Role;
        entity.Period = request.Period;
        entity.Projects = request.Projects;
        entity.Highlights = request.Highlights;
        entity.SortOrder = request.SortOrder;
    }

    public static void ApplyTo(this ProjectEntryRequest request, ProjectEntryEntity entity)
    {
        entity.Name = request.Name;
        entity.Organisation = request.Organisation;
        entity.Icon = request.Icon;
        entity.Description = request.Description;
        entity.Tags = request.Tags;
        entity.SortOrder = request.SortOrder;
    }

    public static void ApplyTo(this SkillGroupRequest request, SkillGroupEntity entity)
    {
        entity.Category = request.Category;
        entity.Icon = request.Icon;
        entity.Items = request.Items;
        entity.SortOrder = request.SortOrder;
    }

    public static void ApplyTo(this CertificationRequest request, CertificationEntity entity)
    {
        entity.Name = request.Name;
        entity.Date = request.Date;
        entity.SortOrder = request.SortOrder;
    }

    public static void ApplyTo(this EducationEntryRequest request, EducationEntryEntity entity)
    {
        entity.School = request.School;
        entity.Degree = request.Degree;
        entity.Location = request.Location;
        entity.Date = request.Date;
    }
}
