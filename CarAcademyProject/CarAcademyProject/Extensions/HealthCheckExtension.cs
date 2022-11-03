﻿using CarAcademyProjectModels.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace CarAcademyProject.Extensions
{
    public static class HealthCheckExtension
    {
        public static IApplicationBuilder RegisterHealthChecks(this IApplicationBuilder app)
        {

            return app.UseHealthChecks("/healthz", new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries
                            .Select(x => new IndividualHealthCheckResponse()
                            {
                                Component = x.Key,
                                Status = x.Value.ToString(),
                                Description = x.Value.Description
                            }),
                        HealthCheckDuration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
            });
        }
    }
}