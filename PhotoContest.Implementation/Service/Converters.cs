using PhotoContest.Models;

namespace PhotoContest.Implementation.Service
{
    internal static class Converters
    {
        public static Contest ToModel(Ado.DataRecords.Contest dataRecord)
        {
            return new Contest
            {
                Theme = dataRecord.Theme,
                EndDate = dataRecord.EndDate,
                Id = dataRecord.Id
            };
        }
    }
}