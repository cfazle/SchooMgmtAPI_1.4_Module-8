using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurataion
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasData
           (
               new Submission
               {
                   Id = new Guid("80abbca8-611d-4b20-b5de-024705493d5a"),
                   SubmissionTitle = "Module 1 submission",
                   CreatedDate = Convert.ToDateTime("11/16/2019"),
                   UpdatedDate = Convert.ToDateTime("06/24/2020"),
                   Score = 100,
                  AssignmentId = new Guid("80abbca8-664d-4b20-b5de-024705498a4b")                  

               },
                  new Submission
                  {
                      Id = new Guid("80abbca8-612d-4b20-bbde-024705493d5a"),
                      SubmissionTitle = "Module 2 submission",
                      CreatedDate = Convert.ToDateTime("11/16/2019"),
                      UpdatedDate = Convert.ToDateTime("06/24/2020"),
                      Score = 100,
                      AssignmentId = new Guid("80abbca8-624d-4b20-b5de-024705495a1e")

                  },
                   new Submission
                   {
                       Id = new Guid("80abbca8-613d-4b20-bcde-024705493d5a"),
                       SubmissionTitle = "Module 2 submission",
                       CreatedDate = Convert.ToDateTime("11/16/2019"),
                       UpdatedDate = Convert.ToDateTime("06/24/2020"),
                       Score = 100,
                       AssignmentId = new Guid("80abbca8-66ed-4b20-b5de-024705495e1e")

                   },
                    new Submission
                    {
                        Id = new Guid("80abbca8-614d-4b20-bcde-024705493d5a"),
                        SubmissionTitle = "Module 2 submission",
                        CreatedDate = Convert.ToDateTime("11/16/2019"),
                        UpdatedDate = Convert.ToDateTime("06/24/2020"),
                        Score = 100,
                        AssignmentId = new Guid("80abbca8-662d-4b20-b5de-024705495a2e")

                    }
              ); 
        }

    }

}
