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
                FileId = model.FileId,
                Photographer = model.Photographer.ToContract(),
                ReferenceId = model.ReferenceId,
                Theme = model.Theme,
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
                FileId = contract.FileId,
                Photographer = contract.Photographer.ToModel(),
                ReferenceId = contract.ReferenceId,
                Theme = contract.Theme,
                UploadedOn = contract.UploadedOn,
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
                Id = model.Id,
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
                Id = contract.Id,
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
                ContestDate = contract.ContestDate,
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
                FirstVoteId = model.FirstVoteId,
                Photographer = model.Photographer.ToContract(),
                SecondVoteId = model.SecondVoteId,
                ThirdVoteId = model.ThirdVoteId,
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
                FirstVoteId = contract.FirstVoteId,
                Photographer = contract.Photographer.ToModel(),
                SecondVoteId = contract.SecondVoteId,
                ThirdVoteId = contract.ThirdVoteId,
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
                EntryId = model.EntryId,
                Score = model.Score,
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
                EntryId = contract.EntryId,
                Score = contract.Score,
            };
        }
    }
}
