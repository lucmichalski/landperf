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
  public class LighthouseRepository
  {

    public static void SetReport(IConfiguration configuration, Report report)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        string insertReport = "INSERT INTO Reports (urlId, fetchTime, performance) Values (@UrlId, @FetchTime, @Performance);";
        connection.Execute(insertReport, new Report { UrlId = report.UrlId, FetchTime = report.FetchTime, Performance = report.Performance });
      }
    }

    public static async Task<IEnumerable<Url>> GetUrls(IConfiguration configuration)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        var urlsSql = "SELECT * FROM Urls";
        var urls = await connection.QueryAsync<Url>(urlsSql);
        return urls;
      }
    }


    public static async Task<IEnumerable<Report>> GetReportsByUrlId(IConfiguration configuration, int urlId)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        //Check if urlId exists?
        var reportsSql = "SELECT * FROM Reports WHERE urlId = @urlId";
        var reports = await connection.QueryAsync<Report>(reportsSql, new { urlId = urlId });
        return reports;
      }
    }
  }
}
