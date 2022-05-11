using WebApi.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace WebApi
{
    internal static class Converter
    {
        public static Submission ToContract(this Provider.Models.Submission model)
        {
            if (model == null)
            {
                return null;
            }

            return new Submission
            {
                Caption = model.Caption,
                FileId = model.FileId.ReferenceId,
                Photographer = model.Photographer.ToContract(),
                ReferenceId = model.Id.ReferenceId,
                Theme = model.Theme.ToContract(),
                UploadedOn = model.UploadedOn,
            };
        }

        public static Provider.Models.Submission ToModel(this Submission contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.Submission(contract.ReferenceId)
            {
                Caption = contract.Caption,
                FileId = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
                Photographer = contract.Photographer.ToModel(),
                Theme = contract.Theme.ToModel(),
                UploadedOn = contract.UploadedOn ?? System.DateTime.MinValue,
            };
        }

        public static IEnumerable<Submission> ToContract(this IEnumerable<Provider.Models.Submission> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Submission>();
        }

        public static IEnumerable<Provider.Models.Submission> ToModel(this IEnumerable<Submission> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Provider.Models.Submission>();
        }

        public static User ToContract(this Provider.Models.User model)
        {
            if (model == null)
            {
                return null;
            }

            return new User
            {
                ReferenceId = model.Id.ReferenceId,
                UploaderName = model.UploaderName,
            };
        }

        public static Provider.Models.User ToModel(this User contract)
        {
            return new Provider.Models.User(contract.ReferenceId)
            {
                UploaderName = contract.UploaderName,
            };
        }

        public static IEnumerable<User> ToContract(this IEnumerable<Provider.Models.User> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<User>();
        }

        public static IEnumerable<Provider.Models.User> ToModel(this IEnumerable<User> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Provider.Models.User>();
        }

        public static Contest ToContract(this Provider.Models.Contest model)
        {
            if (model == null)
            {
                return null;
            }

            return new Contest
            {
                EndDate = model.EndDate,
                Theme = model.Theme,
                ReferenceId = model.Id.ReferenceId,
            };
        }

        public static Provider.Models.Contest ToModel(this Contest contract)
        {
            return new Provider.Models.Contest(contract.ReferenceId)
            {
                EndDate = contract.EndDate ?? System.DateTime.MinValue,
                Theme = contract.Theme,
            };
        }

        public static IEnumerable<Contest> ToContract(this IEnumerable<Provider.Models.Contest> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Contest>();
        }

        public static IEnumerable<Provider.Models.Contest> ToModel(this IEnumerable<Contest> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Provider.Models.Contest>();
        }

        public static VoteInfo ToContract(this Provider.Models.VoteInfo model)
        {
            if (model == null)
            {
                return null;
            }

            return new VoteInfo
            {
                ReferenceId = model.Id.ReferenceId,
                FirstPick = model.FirstPick.ToContract(),
                Photographer = model.Photographer.ToContract(),
                SecondPick = model.SecondPick.ToContract(),
                ThirdPick = model.ThirdPick.ToContract(),
                Theme = model.Contest.ToContract(),
            };
        }

        public static Provider.Models.VoteInfo ToModel(this VoteInfo contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.VoteInfo(contract.ReferenceId)
            {
                FirstPick = contract.FirstPick.ToModel(),
                Photographer = contract.Photographer.ToModel(),
                SecondPick = contract.SecondPick.ToModel(),
                ThirdPick = contract.ThirdPick.ToModel(),
                Contest = contract.Theme.ToModel(),
            };
        }

        public static ScoreDetail ToContract(this Provider.Models.ScoreDetail model)
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

        public static Provider.Models.ScoreDetail ToContract(this ScoreDetail contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.ScoreDetail(contract.ReferenceId)
            {
                PhotoEntry = contract.PhotoEntry.ToModel(),
                Score = contract.Score,
            };
        }
    }
}
