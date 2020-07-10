using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.Configurataion
{
    class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasData
           (
               new Enrollment
               {
                   Id = new Guid("80abbca8-664d-4b20-b4de-024705497c5e"),
                   AttributeName = "Student",
                   StartDate = Convert.ToDateTime("05/21/2020"),
                   EndDate = Convert.ToDateTime("08/08/2020"),
                   CreatedDate = Convert.ToDateTime("11/16/2019"),
                   UpdatedDate = Convert.ToDateTime("06/24/2019"),                  
                   SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d5a")
               } ,  
               new Enrollment
               {
                   Id = new Guid("80abbca8-664d-4b20-c5de-024705497c1e"),
                   AttributeName = "Teacher",
                   StartDate = Convert.ToDateTime("05/21/2020"),
                   EndDate = Convert.ToDateTime("08/08/2020"),
                   CreatedDate = Convert.ToDateTime("11/16/2019"),
                   UpdatedDate = Convert.ToDateTime("06/24/2019"),
                   SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d5a")
               },

               new Enrollment
               {
                   Id = new Guid("80abbca8-664d-4b20-d5de-024705497c2c"),
                   AttributeName = "Student",
                   StartDate = Convert.ToDateTime("05/21/2020"),
                   EndDate = Convert.ToDateTime("08/08/2020"),
                   CreatedDate = Convert.ToDateTime("11/16/2019"),
                   UpdatedDate = Convert.ToDateTime("06/24/2019"),
                   SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d5a")
               },


                 new Enrollment
                 {
                     Id = new Guid("80abbca8-664d-4b20-e5de-024705497c2e"),
                     AttributeName = "Student",
                     StartDate = Convert.ToDateTime("05/21/2020"),
                     EndDate = Convert.ToDateTime("08/08/2020"),
                     CreatedDate = Convert.ToDateTime("11/16/2019"),
                     UpdatedDate = Convert.ToDateTime("06/24/2019"),
                     SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d6a")
                 },

                 new Enrollment
                 {
                     Id = new Guid("80abbca8-664d-4b20-b6de-024705497c1e"),
                     AttributeName = "Student",
                     StartDate = Convert.ToDateTime("05/21/2020"),
                     EndDate = Convert.ToDateTime("08/08/2020"),
                     CreatedDate = Convert.ToDateTime("11/16/2019"),
                     UpdatedDate = Convert.ToDateTime("06/24/2019"),                    
                     SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d6a")
                 },
                 new Enrollment
                  {
                Id = new Guid("80abbca8-664d-4b20-c6de-024705497d8e"),
                     AttributeName = "Admin",
                     StartDate = Convert.ToDateTime("05/21/2020"),
                     EndDate = Convert.ToDateTime("08/08/2020"),
                     CreatedDate = Convert.ToDateTime("11/16/2019"),
                     UpdatedDate = Convert.ToDateTime("06/24/2019"),                    
                     SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d6a")
                 },

                 new Enrollment
                 {
                     Id = new Guid("80abbca8-664d-4b20-d6de-024705497d9e"),
                     AttributeName = "Teacher",
                     StartDate = Convert.ToDateTime("05/21/2020"),
                     EndDate = Convert.ToDateTime("08/08/2020"),
                     CreatedDate = Convert.ToDateTime("11/16/2019"),
                     UpdatedDate = Convert.ToDateTime("06/24/2019"),
                     SectionId = new Guid("80abbca8-664d-4b20-b5de-024705497d6a")
                 }
               ); 


        }
    }
}

