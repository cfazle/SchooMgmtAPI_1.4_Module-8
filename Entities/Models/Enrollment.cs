using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
  public  class Enrollment
    {
        [Column("EnrollmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Attribute name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Attribut Name is 30 characters.")]
        public string AttributeName { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime UpdatedDate { get; set; }

      

        [ForeignKey(nameof(Section))]
        public Guid SectionId { get; set; }

        //  public Organization Organization { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}

