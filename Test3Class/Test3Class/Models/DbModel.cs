using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test3Class.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required,StringLength(40)]
        public string SubjectName { get; set; }
        public virtual ICollection<Student> Student { get; set; }

    }
    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(40)]
        public string StudentName { get; set; }
        [Required]
        public int Roll { get; set; }
        [Required, StringLength(140)]
        public string Photo { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required,ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options){}
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
