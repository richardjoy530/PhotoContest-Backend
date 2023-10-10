using System;

#pragma warning disable CS1591

namespace PhotoContest.Implementation.Ado.DataRecords
{
    [Flags]
    public enum ContestParams
    {
        None = 0b0,
        Theme = 0b1,
        EndDate = 0b10,
        All = 0b111
    }

    [Flags]
    public enum FileInfoParams
    {
        None = 0b0,
        Path = 0b1,
        All = 0b11
    }

    [Flags]
    public enum ScoreInfoParams
    {
        None = 0b0,
        SubmissionId = 0b1,
        Score = 0b10,
        All = 0b111
    }

    [Flags]
    public enum SubmissionParams
    {
        None = 0b0,
        ContestId = 0b1,
        FileInfoId = 0b10,
        Caption = 0b100,
        UploadedOn = 0b1000,
        UserId = 0b10000,
        RefId = 0b100000,
        All = 0b1111111
    }

    [Flags]
    public enum UserInfoParams
    {
        None = 0b0,
        Name = 0b1,
        Email = 0b10,
        RefId = 0b100,
        RegistrationDate = 0b1000,
        All = 0b11111
    }

    [Flags]
    public enum VoteInfoParams
    {
        None = 0b0,
        FirstId = 0b1,
        SecondId = 0b10,
        ThirdId = 0b100,
        ContestId = 0b1000,
        UserId = 0b10000,
        All = 0b111111
    }
}