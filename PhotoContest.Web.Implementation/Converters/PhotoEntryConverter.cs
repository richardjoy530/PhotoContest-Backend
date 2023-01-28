#region

using System;
using System.Collections.Generic;
using System.Linq;
using PhotoContest.Models;
using PhotoContest.Web.Contracts;
using Contest = PhotoContest.Web.Contracts.Contest;
using Submission = PhotoContest.Web.Contracts.Submission;

#endregion

namespace PhotoContest.Web.Implementation.Converters;

internal static class PhotoEntryConverter
{
    public static Submission ToContract(this Models.Submission model)
    {
        if (model == null) return null;

        return new Submission
        {
            Caption = model.Caption,
            FileId = model.FileInfo.ReferenceId,
            Photographer = model.UserInfo.ToContract(),
            ReferenceId = model.Id.ReferenceId,
            Contest = model.Contest.ToContract(),
            UploadedOn = model.UploadedOn
        };
    }

    public static Models.Submission ToModel(this Submission contract)
    {
        if (contract == null) return null;

        return new Models.Submission(contract.ReferenceId)
        {
            Caption = contract.Caption,
            FileInfo = new Id {ReferenceId = contract.ReferenceId},
            UserInfo = contract.Photographer.ToModel(),
            Contest = contract.Contest.ToModel(),
            UploadedOn = contract.UploadedOn ?? DateTime.MinValue
        };
    }

    public static IEnumerable<Submission> ToContract(this IEnumerable<Models.Submission> model)
    {
        return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Submission>();
    }

    public static IEnumerable<Models.Submission> ToModel(this IEnumerable<Submission> model)
    {
        return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.Submission>();
    }

    public static Photographer ToContract(this Models.UserInfo model)
    {
        if (model == null) return null;

        return new Photographer
        {
            ReferenceId = model.Id.ReferenceId,
            UploaderName = model.Name
        };
    }

    public static Models.UserInfo ToModel(this Photographer contract)
    {
        return new Models.UserInfo(contract.ReferenceId)
        {
            Name = contract.UploaderName
        };
    }

    public static IEnumerable<Photographer> ToContract(this IEnumerable<Models.UserInfo> model)
    {
        return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Photographer>();
    }

    public static IEnumerable<Models.UserInfo> ToModel(this IEnumerable<Photographer> model)
    {
        return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.UserInfo>();
    }

    public static Contest ToContract(this Models.Contest model)
    {
        if (model == null) return null;

        return new Contest
        {
            ContestDate = model.EndDate,
            Theme = model.Theme,
            ReferenceId = model.Id.ReferenceId
        };
    }

    public static Models.Contest ToModel(this Contest contract)
    {
        return new Models.Contest(contract.ReferenceId)
        {
            EndDate = contract.ContestDate ?? DateTime.MinValue,
            Theme = contract.Theme
        };
    }

    public static IEnumerable<Contest> ToContract(this IEnumerable<Models.Contest> model)
    {
        return model?.Select(ToContract).ToArray() ?? Enumerable.Empty<Contest>();
    }

    public static IEnumerable<Models.Contest> ToModel(this IEnumerable<Contest> model)
    {
        return model?.Select(ToModel).ToArray() ?? Enumerable.Empty<Models.Contest>();
    }

    public static PhotographerVoteDetails ToContract(this Models.VoteInfo model)
    {
        if (model == null) return null;

        return new PhotographerVoteDetails
        {
            ReferenceId = model.Id.ReferenceId,
            FirstVote = model.FirstSubmission.ToContract(),
            Photographer = model.UserInfo.ToContract(),
            SecondVote = model.SecondSubmission.ToContract(),
            ThirdVote = model.ThirdSubmission.ToContract(),
            Theme = model.Contest.ToContract()
        };
    }

    public static Models.VoteInfo ToModel(this PhotographerVoteDetails contract)
    {
        if (contract == null) return null;

        return new Models.VoteInfo(contract.ReferenceId)
        {
            FirstSubmission = contract.FirstVote.ToModel(),
            UserInfo = contract.Photographer.ToModel(),
            SecondSubmission = contract.SecondVote.ToModel(),
            ThirdSubmission = contract.ThirdVote.ToModel(),
            Contest = contract.Theme.ToModel()
        };
    }

    public static ScoreDetail ToContract(this Models.ScoreInfo model)
    {
        if (model == null) return null;

        return new ScoreDetail
        {
            Submission = model.Submission.ToContract(),
            Score = model.Score,
            ReferenceId = model.Id.ReferenceId
        };
    }

    public static Models.ScoreInfo ToContract(this ScoreDetail contract)
    {
        if (contract == null) return null;

        return new Models.ScoreInfo(contract.ReferenceId)
        {
            Submission = contract.Submission.ToModel(),
            Score = contract.Score
        };
    }
}