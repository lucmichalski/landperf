using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        string insertReport = "INSERT INTO Report (url_id, fetchTime, performance) Values (@UrlId, @FetchTime, @Performance);";
        connection.Execute(insertReport, new Report { UrlId = report.UrlId, FetchTime = report.FetchTime, Performance = report.Performance });
      }
    }

    public static async Task<IEnumerable<Url>> GetUrls(IConfiguration configuration)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        var urlsSql = "SELECT * FROM Url";
        var urls = await connection.QueryAsync<Url>(urlsSql);
        return urls;
      }
    }
  }
}
