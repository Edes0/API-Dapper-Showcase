namespace Mimbly.Application.Queries.Mimbox.GetById;

using global::Mimbly.Application.Contracts.Dtos.Mimbox;

public class MimboxFilteredById
{
    public MimboxDto Mimbox { get; set; }

    public MimboxFilteredById() => Mimbox = new MimboxDto();
}