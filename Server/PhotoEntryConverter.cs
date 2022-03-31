﻿using Server.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    internal static class Converter
    {
        public static PhotoEntry ToContract(this Provider.Models.PhotoEntry model)
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

        public static Provider.Models.PhotoEntry ToModel(this PhotoEntry contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.PhotoEntry
            {
                Caption = contract.Caption,
                FileId = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
                Photographer = contract.Photographer.ToModel(),
                Id = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
                Theme = contract.Theme.ToModel(),
                UploadedOn = contract.UploadedOn ?? System.DateTime.MinValue,
            };
        }

        public static IEnumerable<PhotoEntry> ToContract(this IEnumerable<Provider.Models.PhotoEntry> model)
        {
            return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<PhotoEntry>();
        }

        public static IEnumerable<Provider.Models.PhotoEntry> ToModel(this IEnumerable<PhotoEntry> model)
        {
            return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Provider.Models.PhotoEntry>();
        }

        public static Photographer ToContract(this Provider.Models.Photographer model)
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

        public static Provider.Models.Photographer ToModel(this Photographer contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.Photographer
            {
                Id = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
                UploaderName = contract.UploaderName,
            };
        }

        public static PhotoTheme ToContract(this Provider.Models.PhotoTheme model)
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

        public static Provider.Models.PhotoTheme ToModel(this PhotoTheme contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.PhotoTheme
            {
                ContestDate = contract.ContestDate ?? System.DateTime.MinValue,
                Theme = contract.Theme,
            };
        }

        public static PhotographerVoteDetails ToContract(this Provider.Models.PhotographerVoteDetails model)
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

        public static Provider.Models.PhotographerVoteDetails ToModel(this PhotographerVoteDetails contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Provider.Models.PhotographerVoteDetails
            {
                Id = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
                FirstVote = contract.FirstVote.ToModel(),
                Photographer = contract.Photographer.ToModel(),
                SecondVote = contract.SecondVote.ToModel(),
                ThirdVote = contract.ThirdVote.ToModel(),
                Theme = contract.Theme.ToModel(),
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

            return new Provider.Models.ScoreDetail
            {
                PhotoEntry = contract.PhotoEntry.ToModel(),
                Score = contract.Score,
                Id = new Provider.Models.Id { ReferenceId = contract.ReferenceId },
            };
        }
    }
}
