namespace Mimbly.Application.Common.Mappings;

using Dapper.FluentMap.Mapping;
using Mimbly.Domain.Entities.POCOs;

public class MimboxPocoMapping : EntityMap<MimboxPoco>
{
    public MimboxPocoMapping()
    {
        //Map(m => m.MimboxContact)
        //    .ToColumn("Mimbox_Contact");

        //Map(m => m.MimboxLocation)
        //    .ToColumn("Mimbox_Location");

        //Map(m => m.MimboxLog)
        //    .ToColumn("Mimbox_Log");

        //Map(m => m.MimboxModel)
        //    .ToColumn("Mimbox_Model");

        //Map(m => m.MimboxStatus)
        //    .ToColumn("Mimbox_Status");

        //Map(m => m.MimboxErrorLog)
        //    .ToColumn("Mimbox_Error_Log");


        Map(m => m.MimboxContact.Id)
            .ToColumn("Mimbox_Contact.Id");

        Map(m => m.MimboxContact.Title)
            .ToColumn("Title");

        Map(m => m.MimboxContact.FirstName)
            .ToColumn("First_name");

        Map(m => m.MimboxContact.LastName)
           .ToColumn("Last_name");

        Map(m => m.MimboxContact.Email)
            .ToColumn("Email");

        Map(m => m.MimboxContact.PhoneNumber)
           .ToColumn("Phone_number");

        Map(m => m.MimboxContact.MimboxId)
          .ToColumn("Mimbox_Id");

        ////////////////////

        Map(m => m.MimboxLocation.Id)
            .ToColumn("Mimbox_Location.Id");

        Map(m => m.MimboxLocation.Country)
            .ToColumn("Country");

        Map(m => m.MimboxLocation.Region)
            .ToColumn("Region");

        Map(m => m.MimboxLocation.PostalCode)
           .ToColumn("PostalCode");

        Map(m => m.MimboxLocation.City)
            .ToColumn("City");

        Map(m => m.MimboxLocation.StreetAddress)
           .ToColumn("Street_address");

        ///////////////

        Map(m => m.MimboxLog.Id)
            .ToColumn("Mimbox_Log.Id");

        Map(m => m.MimboxLog.Log)
            .ToColumn("Log");

        Map(m => m.MimboxLog.CreatedAt)
            .ToColumn("Created_At");

        Map(m => m.MimboxLog.MimboxId)
           .ToColumn("Mimbox_Id");

        ///////////////

        Map(m => m.MimboxModel.Id)
            .ToColumn("Mimbox_Model.Id");

        Map(m => m.MimboxModel.Name)
            .ToColumn("Name");

        ///////////////

        Map(m => m.MimboxStatus.Id)
            .ToColumn("Mimbox_Status.Id");

        Map(m => m.MimboxStatus.Name)
            .ToColumn("Name");


        ///////////////

        //ERROR LOG
    }
}
