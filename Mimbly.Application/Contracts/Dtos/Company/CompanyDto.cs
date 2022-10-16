namespace Mimbly.Application.Contracts.Dtos.Company;

using System;
using Mimbly.Domain.Entities;

public record CompanyDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Guid? ParentId { get; init; }

    public Company ParentCompany { get; init; }

    public ICollection<Company> ChildCompanyList { get; init; }

    public ICollection<CompanyContact> ContactList { get; init; }

    public ICollection<Mimbox> MimboxList { get; init; }
}