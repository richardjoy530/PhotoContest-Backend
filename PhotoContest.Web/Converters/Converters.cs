using PhotoContest.Models;
using PhotoContest.Web.Contracts;

namespace PhotoContest.Web.Converters;

internal static class ConverterExtensions
{
    public static ContestResponse ToResponse(this Contest data)
    {
        return new ContestResponse
        {
            EndDate = data.EndDate,
            Id = data.Id,
            Theme = data.Theme
        };
    }
    
    public static Contest ToModel(this ContestRequest data)
    {
        return new Contest
        {
            EndDate = data.EndDate,
            Theme = data.Theme
        };
    }
}