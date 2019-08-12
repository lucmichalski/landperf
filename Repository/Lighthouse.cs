using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using LandPerf.Models;
using System.Data.SqlClient;
using Dapper;


namespace LandPerf.Repository
{
  public class Lighthouse
  {

    public static void SetReport(IConfiguration configuration, Report report)
    {

      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        string insertReport = "INSERT INTO Report (SiteId, fetchTime, Performance) Values (@SiteId, @fetchTime, @Performance);";
        connection.Execute(insertReport, new Report { SiteId = report.SiteId, FetchTime = report.FetchTime, Performance = report.Performance });
        var reportsSql = "SELECT * FROM Report";
        var allReports = connection.Query<Report>(reportsSql).ToList();

      }

    }
  }
}
