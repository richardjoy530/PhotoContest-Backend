using Provider.Models;
using System;
using System.Collections.Generic;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="VoteInfo"/>
    /// </summary>
    public class VoteInfoDetailsProvider : IProvider<VoteInfo>
    {
        /// <inheritdoc/>
        public VoteInfo Insert(VoteInfo model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<VoteInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public VoteInfo GetById(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Update(VoteInfo model, string referenceId)
        {
            throw new NotImplementedException();
        }
    }
}
