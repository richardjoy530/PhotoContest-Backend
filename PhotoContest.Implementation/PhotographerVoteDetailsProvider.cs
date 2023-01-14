#region

using System;
using System.Collections.Generic;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
///     Database access layer of <see cref="PhotographerVoteDetails" />
/// </summary>
public class PhotographerVoteDetailsProvider : IProvider<PhotographerVoteDetails>
{
    /// <inheritdoc />
    public PhotographerVoteDetails Insert(PhotographerVoteDetails model)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Delete(string referenceId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IEnumerable<PhotographerVoteDetails> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public PhotographerVoteDetails GetById(string referenceId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Update(PhotographerVoteDetails model, string referenceId)
    {
        throw new NotImplementedException();
    }
}