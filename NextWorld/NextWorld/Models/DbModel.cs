using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NextWorld.Models
{
    public enum course {wada=1,gave,nt,php };
    public class Trainee
    {
        [Key]
        public int TraineeNo { get; set; }
        [Required,StringLength(40)]
        public string TraineeName { get; set; }
        [Required,StringLength(40),EmailAddress]
        public string TraineeEmail { get; set; }
        [Required,EnumDataType(typeof(course))]
        public course TraineeCourse { get; set; }
    }
    public class TraineeDbContext:DbContext
    {
        public DbSet<Trainee> Trainees { get; set; }
    }
}