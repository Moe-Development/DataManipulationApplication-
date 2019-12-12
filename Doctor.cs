using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5b
{
    /// <summary>
    /// this is the Doctor object, we use it to store the doctor data that comes from the database.
    /// </summary>
    class Doctor
    {
        //setting the Doctor properties:
        public int DoctorId { get; }
        public string Actor { get; }
        public int Series { get; }
        public int Age { get; }
        public string Debut { get; }
        public byte[] Picture { get; }

        /// <summary>
        /// the Doctor class constructre, its setting and assigning the appropriate data to the appropriate property
        /// </summary>
        /// <param name="id">Doctor id</param>
        /// <param name="actor">Doctor actor</param>
        /// <param name="series">Doctor series</param>
        /// <param name="age">Doctor age</param>
        /// <param name="debut">Doctor debut</param>
        /// <param name="picture">Doctor picture</param>
        public Doctor(int id, string actor, int series, int age, string debut, byte[] picture)
        {
            DoctorId = id;
            Actor = actor;
            Series = series;
            Age = age;
            Debut = debut;
            Picture = picture;
        } 
        

    }
}
