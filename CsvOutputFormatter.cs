﻿using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMgmtAPI
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(OrganizationDto).IsAssignableFrom(type) || typeof(IEnumerable<OrganizationDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<OrganizationDto>)
            {
                foreach (var organization in (IEnumerable<OrganizationDto>)context.Object)
                {
                    FormatCsv(buffer, organization);
                }
            }
            else
            {
                FormatCsv(buffer, (OrganizationDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer,OrganizationDto organization)
        {
            buffer.AppendLine($"{organization.Id},\"{organization.OrgName},\"{organization.FullAddress}\"");
        }

    }
}