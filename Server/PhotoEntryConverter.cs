using Server.Contracts;

namespace Server
{
    public static class Converter
    {
        public static PhotoEntry ToContract(this Provider.Models.PhotoEntry model)
        {
            return new PhotoEntry
            {
                Caption = model.Caption,
                FileId = model.FileId,
                Photographer = model.Photographer,
                ReferenceID = model.ReferenceID,
                Theme = model.Theme,
                UploadedOn = model.UploadedOn
            };
        }

        public static Provider.Models.PhotoEntry ToModel(this PhotoEntry contract)
        {
            return new Provider.Models.PhotoEntry
            {
                Caption = contract.Caption,
                FileId = contract.FileId,
                Photographer = contract.Photographer,
                ReferenceID = contract.ReferenceID,
                Theme = contract.Theme,
                UploadedOn = contract.UploadedOn
            };
        }
    }
}
