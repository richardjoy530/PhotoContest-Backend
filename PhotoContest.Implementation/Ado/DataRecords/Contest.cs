using System;

namespace PhotoContest.Implementation.Ado.DataRecords
{
    /// <summary>
    /// </summary>
    public class Contest : IDataRecord
    {
        /// <summary>
        /// </summary>
        public DateTime EndDate;

        /// <summary>
        /// </summary>
        public string Theme;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="endDate"></param>
        public Contest(string theme, DateTime endDate)
        {
            Theme = theme;
            EndDate = endDate;
        }

        /// <summary>
        /// 
        /// </summary>
        public Contest()
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    }
}