namespace Mimbly.Application.Contracts.Dtos.Company;

using System;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public record CompanyDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Guid? ParentId { get; init; }

    public CompanyDto ParentCompany { get; init; }

    public ICollection<CompanyDto> ChildCompanyList { get; init; }

    public ICollection<CompanyContactDto> ContactList { get; init; }

    public ICollection<MimboxDto> MimboxList { get; init; }
}