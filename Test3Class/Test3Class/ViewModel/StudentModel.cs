using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Test3Class.ViewModel
{
    public class StudentModel
    {
        
            public int StudentId { get; set; }
            [Required, StringLength(40)]
            public string StudentName { get; set; }
            [Required]
            public int Roll { get; set; }
            
            public IFormFile Photo { get; set; }
            [Required]
            public bool Status { get; set; }
            [Required]
            public int SubjectId { get; set; }
           
        
    }
}
