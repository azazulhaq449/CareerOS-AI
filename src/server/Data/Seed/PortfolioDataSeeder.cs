using CareerOS.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Data.Seed;

/// <summary>
/// One-time bootstrap of the resume content that used to live in the
/// InMemory* repositories. Idempotent — checks for existing data before
/// inserting, so it's safe to run on every startup. This is a stopgap until
/// the Phase 1 admin panel can edit this content directly.
/// </summary>
public static class PortfolioDataSeeder
{
    public static async Task SeedAsync(CareerOSDbContext context)
    {
        if (await context.Profiles.AnyAsync())
        {
            return;
        }

        context.Profiles.Add(new ProfileEntity
        {
            Id = Guid.NewGuid(),
            Name = "Azaz ul Haq",
            Initials = "AH",
            Title = "Senior Software Engineer",
            Roles = ["Senior .NET Engineer", "Cloud Solutions Architect", "AI-Integrated Developer"],
            Location = "Manchester, United Kingdom",
            Email = "azaz.ulhaqq@gmail.com",
            Phone = "+44 7774 031733",
            PhoneHref = "tel:+447774031733",
            LinkedInUrl = "https://www.linkedin.com/in/azaz-ul-haq-dev/",
            Summary =
            [
                "Software Developer with 8+ years of experience delivering .NET-based web and enterprise applications within Agile environments. Microsoft Azure-certified developer with hands-on experience across the full software development lifecycle, from requirements analysis and system design to implementation and deployment.",
                "Strong expertise in .NET Core (.NET 6-8), C#, MVC, RESTful Web APIs, WCF, SQL Server, and CMS platforms including Umbraco and Optimizely. Proven experience building and deploying cloud-native solutions on Microsoft Azure, including Azure App Services, Azure Functions, Cosmos DB, and secure API integrations.",
                "Experienced in implementing AI-powered features by integrating OpenAI APIs for automation and enhanced user experiences. Recognised as a Top-Rated Upwork developer, known for collaborative teamwork, mentoring junior developers, and delivering scalable, high-quality solutions.",
            ],
        });

        context.Strengths.AddRange(
            new StrengthEntity
            {
                Id = Guid.NewGuid(),
                Icon = "uil-server",
                Title = "End-to-End .NET Delivery",
                Description = "8+ years shipping .NET 6-8 web and enterprise applications, from requirements and system design through implementation, testing and deployment.",
                SortOrder = 0,
            },
            new StrengthEntity
            {
                Id = Guid.NewGuid(),
                Icon = "uil-cloud-computing",
                Title = "Cloud-Native on Azure",
                Description = "Azure-certified (AZ-204) architect of App Services, Functions, Container Apps and Cosmos DB solutions, applying Clean Architecture and DDD.",
                SortOrder = 1,
            },
            new StrengthEntity
            {
                Id = Guid.NewGuid(),
                Icon = "uil-robot",
                Title = "AI-Powered Features",
                Description = "Integrates OpenAI APIs for automation, prompt engineering and self-learning workflows that enhance real-world user experiences.",
                SortOrder = 2,
            });

        context.ExperienceEntries.AddRange(
            new ExperienceEntryEntity
            {
                Id = Guid.NewGuid(),
                Company = "Digital Reward Group",
                Location = "Manchester, UK",
                Role = "Senior Software Developer",
                Period = "March 2024 – Present",
                Projects = "KidsPass, DRG Admin, B2B Portal, Public & Partner APIs",
                Highlights =
                [
                    "Design, develop and maintain .NET 8 RESTful APIs, Web APIs and MVC applications supporting high-traffic, multi-brand platforms including KidsPass, PopcornPass, FamilyPass and GlobalHotelPass.",
                    "Architect scalable n-tier and microservices-based solutions, applying SOLID principles, Clean Architecture and domain-driven design (DDD).",
                    "Implement secure, compliant payment solutions using Adyen (Drop-in, Express Checkout, Apple Pay, Google Pay, 3DS, recurring billing) and Google Wallet.",
                    "Manage authentication and authorisation using Microsoft Entra ID with external identity providers and role-based access control (RBAC).",
                    "Deploy and operate services on Azure App Services, Functions and Container Apps, using Azure Key Vault for secrets management.",
                ],
                SortOrder = 0,
            },
            new ExperienceEntryEntity
            {
                Id = Guid.NewGuid(),
                Company = "Autocab",
                Location = "Manchester, UK",
                Role = "Software Developer",
                Period = "Aug 2023 – March 2024",
                Projects = "Meter Companion, Booking & Dispatch Ghost API",
                Highlights =
                [
                    "Developed and maintained .NET 7 Web APIs with a strong focus on RESTful API design and backend performance.",
                    "Designed and implemented n-tier architecture following SOLID principles, improving scalability and maintainability across services.",
                    "Built a Blazor application from scratch, delivering interactive, data-driven UIs with a modern component-based architecture.",
                    "Integrated Azure AD B2C authentication and authorisation to secure user access and identity management.",
                ],
                SortOrder = 1,
            },
            new ExperienceEntryEntity
            {
                Id = Guid.NewGuid(),
                Company = "Strategic Systems International",
                Location = "UK",
                Role = "Senior Software Engineer",
                Period = "March 2019 – Aug 2023",
                Projects = "WORKS Performance Monitor, WORKS Material Tower Cluster Controller",
                Highlights =
                [
                    "Implemented SMT industrial messaging architecture (CFX) with RabbitMQ for real-time production line monitoring.",
                    "Developed backend service layers using .NET, C#, Task Parallel Library and WCF Services.",
                    "Participated in migrating the web frontend from AngularJS to Angular 13.",
                    "Practised Unit Test Driven Development (UTDD), running workshops and documentation for the wider team.",
                    "Mentored team members and provided solutions for complex technical issues across multiple projects.",
                ],
                SortOrder = 2,
            },
            new ExperienceEntryEntity
            {
                Id = Guid.NewGuid(),
                Company = "Ai logica",
                Location = "Remote, UK",
                Role = "Full Stack .NET Developer",
                Period = "July 2017 – March 2019",
                Projects = "Deal.Bargains, Millimeter",
                Highlights =
                [
                    "Built an e-commerce web application in .NET using Umbraco CMS and Web API, handling over 10,000 brands with SQL Server and MongoDB.",
                    "Enhanced search capability by integrating Apache Solr and built a module for uploading images to Amazon S3.",
                    "Gained exposure to blockchain technology, contributing to a blockchain core project for crypto wallet development.",
                    "Troubleshot and resolved bugs across .NET applications, working closely with senior developers on multiple products.",
                ],
                SortOrder = 3,
            });

        context.ProjectEntries.AddRange(
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "KidsPass",
                Organisation = "Digital Reward Group",
                Icon = "uil-ticket",
                Description = "Multi-brand membership platform sharing one codebase across KidsPass, PopcornPass, FamilyPass and GlobalHotelPass, with Adyen/Google Wallet payments and Entra ID auth.",
                Tags = [".NET 8", "Azure", "Adyen", "Entra ID", "Quartz.NET"],
                SortOrder = 0,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "Meter Companion",
                Organisation = "Autocab",
                Icon = "uil-taxi",
                Description = "Greenfield Blazor application for taxi dispatch and booking, with RESTful backend services and a Corporate Portal built on MudBlazor and QuickGrid.",
                Tags = ["Blazor", ".NET 7", "MudBlazor", "Azure AD B2C"],
                SortOrder = 1,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "Instamail AI",
                Organisation = "Upwork",
                Icon = "uil-robot",
                Description = "Task scheduling and automation platform integrating Google API and Microsoft Graph for email/calendar management, with OpenAI-driven automation workflows.",
                Tags = [".NET 8", "Quartz.NET", "OpenAI API", "MongoDB"],
                SortOrder = 2,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "Pattern.com",
                Organisation = "Upwork",
                Icon = "uil-sitemap",
                Description = "Cloud-integrated automation platform combining Azure Functions, Pipedrive and Snowflake APIs with UiPath for repetitive task automation, plus a React front end.",
                Tags = ["Azure Functions", "UiPath", "React", ".NET APIs"],
                SortOrder = 3,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "WORKS Performance Monitor",
                Organisation = "Strategic Systems International",
                Icon = "uil-chart-line",
                Description = "Real-time monitoring system for ASMPT SMT production lines (Germany) built on a microservice architecture with RabbitMQ messaging.",
                Tags = ["Angular 13", ".NET", "WCF", "RabbitMQ", "TPL"],
                SortOrder = 4,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "WORKS Material Tower Cluster Controller",
                Organisation = "Strategic Systems International",
                Icon = "uil-database",
                Description = "Software for managing material storage and retrieval across Material Towers, built with strong OOP and data structure design.",
                Tags = [".NET MVC", "C#", "WCF", "TPL"],
                SortOrder = 5,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "Deal.Bargains",
                Organisation = "Ai logica",
                Icon = "uil-shopping-cart",
                Description = "E-commerce affiliate platform for category, brand and retailer management, with SignalR real-time updates and Apache Solr search.",
                Tags = [".NET MVC", "Umbraco", "SQL Server", "MongoDB", "SignalR"],
                SortOrder = 6,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "Millimeter",
                Organisation = "Ai logica",
                Icon = "uil-link-alt",
                Description = "Secure blockchain system for land data storage using peer-to-peer programming and high-level encryption.",
                Tags = [".NET", "WCF", "Blockchain"],
                SortOrder = 7,
            },
            new ProjectEntryEntity
            {
                Id = Guid.NewGuid(),
                Name = "National History Museum",
                Organisation = "Ai logica",
                Icon = "uil-university",
                Description = "Museum website covering exhibits, press releases, events and volunteer management, with integrated e-commerce and payments.",
                Tags = [".NET MVC", "SQL Server"],
                SortOrder = 8,
            });

        context.SkillGroups.AddRange(
            new SkillGroupEntity
            {
                Id = Guid.NewGuid(),
                Category = "Backend & APIs",
                Icon = "uil-server",
                Items = [".NET Core", ".NET MVC", "C#", "Web API", "WCF", "SignalR"],
                SortOrder = 0,
            },
            new SkillGroupEntity
            {
                Id = Guid.NewGuid(),
                Category = "Cloud & DevOps",
                Icon = "uil-cloud-computing",
                Items = ["Microsoft Azure", "AWS", "Azure Functions", "Azure App Services", "Git", "TFS"],
                SortOrder = 1,
            },
            new SkillGroupEntity
            {
                Id = Guid.NewGuid(),
                Category = "Frontend",
                Icon = "uil-window-section",
                Items = ["React", "Angular", "Blazor", "HTML/CSS/Bootstrap", "JavaScript", "WPF"],
                SortOrder = 2,
            },
            new SkillGroupEntity
            {
                Id = Guid.NewGuid(),
                Category = "Data & Messaging",
                Icon = "uil-database",
                Items = ["SQL Server", "MongoDB", "RabbitMQ"],
                SortOrder = 3,
            },
            new SkillGroupEntity
            {
                Id = Guid.NewGuid(),
                Category = "Platforms & Automation",
                Icon = "uil-cog",
                Items = ["Umbraco", "Optimizely", "UiPath"],
                SortOrder = 4,
            });

        context.Certifications.AddRange(
            new CertificationEntity { Id = Guid.NewGuid(), Name = "Azure Associate Developer (AZ-204)", Date = "March 2021", SortOrder = 0 },
            new CertificationEntity { Id = Guid.NewGuid(), Name = "Power Platform Fundamentals (PL-900)", Date = "January 2022", SortOrder = 1 },
            new CertificationEntity { Id = Guid.NewGuid(), Name = "RPA Developer Foundation Training", Date = "August 2019", SortOrder = 2 });

        context.EducationEntries.Add(new EducationEntryEntity
        {
            Id = Guid.NewGuid(),
            School = "Government College University",
            Degree = "Bachelor of Computer Science",
            Location = "Lahore, Pakistan",
            Date = "June 2017",
        });

        await context.SaveChangesAsync();
    }
}
