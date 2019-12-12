using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5b
{
    /// <summary>
    /// the is the Companion object, we use it to store the doctor companion data that comes from the database.
    /// </summary>
    class Companion
    {
        //setting the Doctor properties:
        public string Name { get; }
        public string Actor { get; }
        public int DoctorID { get; }
        public string StoryID { get; }

        /// <summary>
        ///  the Companion class constructre, its setting and assigning the appropriate data to the appropriat property
        /// </summary>
        /// <param name="name">Companion name</param>
        /// <param name="actor">Companion actor</param>
        /// <param name="doctorId">Companion doctorId</param>
        /// <param name="storyId">Companion storyId</param>
        public Companion(string name, string actor, int doctorId, string storyId)
        {
            Name = name;
            Actor = actor;
            DoctorID = doctorId;
            StoryID = storyId;
        }
    }
}
