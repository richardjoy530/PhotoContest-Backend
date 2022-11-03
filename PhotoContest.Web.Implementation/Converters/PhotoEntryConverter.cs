using PhotoContest.Web.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PhotoContest.Web.Converter
{
    internal static class PhotoEntryConverter
    {
        public static PhotoEntry ToContract(this Models.PhotoEntry model)
        {
            if (model == null)
            {
                return null;
            }

            return new PhotoEntry
            {
                Caption = model.Caption,
                FileId = model.FileId.ReferenceId,
                Photographer = model.Photographer.ToContract(),
                ReferenceId = model.Id.ReferenceId,
                Theme = model.Theme.ToContract(),
                UploadedOn = model.UploadedOn,
            };
        }

        public static Models.PhotoEntry ToModel(this PhotoEntry contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Models.PhotoEntry(contract.ReferenceId)
            {
                Caption = contract.Caption,
                FileId = new Models.Id { ReferenceId = contract.ReferenceId },
                Photographer = contract.Photographer.ToModel(),
                Theme = contract.Theme.ToModel(),
                UploadedOn = contract.UploadedOn ?? System.DateTime.MinValue,
            };
        }

        public static IEnumerable<PhotoEntry> ToContract(this IEnumerable<Models.PhotoEntry> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<PhotoEntry>();
        }

        public static IEnumerable<Models.PhotoEntry> ToModel(this IEnumerable<PhotoEntry> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.PhotoEntry>();
        }

        public static Photographer ToContract(this Models.Photographer model)
        {
            if (model == null)
            {
                return null;
            }

            return new Photographer
            {
                ReferenceId = model.Id.ReferenceId,
                UploaderName = model.UploaderName,
            };
        }

        public static Models.Photographer ToModel(this Photographer contract)
        {
            return new Models.Photographer(contract.ReferenceId)
            {
                UploaderName = contract.UploaderName,
            };
        }

        public static IEnumerable<Photographer> ToContract(this IEnumerable<Models.Photographer> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Photographer>();
        }

        public static IEnumerable<Models.Photographer> ToModel(this IEnumerable<Photographer> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.Photographer>();
        }

        public static PhotoTheme ToContract(this Models.PhotoTheme model)
        {
            if (model == null)
            {
                return null;
            }

            return new PhotoTheme
            {
                ContestDate = model.ContestDate,
                Theme = model.Theme,
                ReferenceId = model.Id.ReferenceId,
            };
        }

        public static Models.PhotoTheme ToModel(this PhotoTheme contract)
        {
            return new Models.PhotoTheme(contract.ReferenceId)
            {
                ContestDate = contract.ContestDate ?? System.DateTime.MinValue,
                Theme = contract.Theme,
            };
        }

        public static IEnumerable<PhotoTheme> ToContract(this IEnumerable<Models.PhotoTheme> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<PhotoTheme>();
        }

        public static IEnumerable<Models.PhotoTheme> ToModel(this IEnumerable<PhotoTheme> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.PhotoTheme>();
        }

        public static PhotographerVoteDetails ToContract(this Models.PhotographerVoteDetails model)
        {
            if (model == null)
            {
                return null;
            }

            return new PhotographerVoteDetails
            {
                ReferenceId = model.Id.ReferenceId,
                FirstVote = model.FirstVote.ToContract(),
                Photographer = model.Photographer.ToContract(),
                SecondVote = model.SecondVote.ToContract(),
                ThirdVote = model.ThirdVote.ToContract(),
                Theme = model.Theme.ToContract(),
            };
        }

        public static Models.PhotographerVoteDetails ToModel(this PhotographerVoteDetails contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Models.PhotographerVoteDetails(contract.ReferenceId)
            {
                FirstVote = contract.FirstVote.ToModel(),
                Photographer = contract.Photographer.ToModel(),
                SecondVote = contract.SecondVote.ToModel(),
                ThirdVote = contract.ThirdVote.ToModel(),
                Theme = contract.Theme.ToModel(),
            };
        }

        public static ScoreDetail ToContract(this Models.ScoreDetail model)
        {
            if (model == null)
            {
                return null;
            }

            return new ScoreDetail
            {
                PhotoEntry = model.PhotoEntry.ToContract(),
                Score = model.Score,
                ReferenceId = model.Id.ReferenceId,
            };
        }

        public static Models.ScoreDetail ToContract(this ScoreDetail contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Models.ScoreDetail(contract.ReferenceId)
            {
                PhotoEntry = contract.PhotoEntry.ToModel(),
                Score = contract.Score,
            };
        }
    }
}
