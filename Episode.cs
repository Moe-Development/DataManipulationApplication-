using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5b
{
    /// <summary>
    /// the is the Episode object, we use it to store the doctor episode data that comes from the database.
    /// </summary>
    class Episode
    {
        //setting the Doctor properties:
        public string StoryID { get; }
        public int Season { get; }
        public int SeasonYear { get; }
        public string Title { get; }


        /// <summary>
        /// the Episode class constructre, its setting and assigning the appropriate data to the appropriat property
        /// </summary>
        /// <param name="storyId">Episode storyid</param>
        /// <param name="season">Episode seaason</param>
        /// <param name="seasonYear">Episode seasonyear</param>
        /// <param name="title">Episode title</param>
        public Episode(string storyId, int season, int seasonYear, string title)
        {
            StoryID = storyId;
            Season = season;
            SeasonYear = seasonYear;
            Title = title;
        }
       
    }
}

