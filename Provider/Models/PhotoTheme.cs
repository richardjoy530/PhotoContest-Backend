﻿using System;

namespace Provider.Models
{
    /// <summary>
    /// Contains details of PhotoTheme
    /// </summary>
    public class PhotoTheme
    {
        /// <summary>
        /// Theme of the photo contest
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Date of the contest
        /// </summary>
        public DateTime ContestDate { get; set; }
    }
}