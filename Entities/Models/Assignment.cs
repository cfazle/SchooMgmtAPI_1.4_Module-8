using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
   public  class Assignment
    {

        [Column("AssignmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Assignment title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the title is 30 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Assignment Description is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the Description is 200 characters.")]
        public string Description { get; set; }


        [ForeignKey(nameof(Enrollment))]
           
        public Guid EnrollmentId { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
