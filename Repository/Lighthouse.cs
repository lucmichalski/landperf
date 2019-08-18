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

    public static async Task<int> SetReport(IConfiguration configuration, Report report)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        string insertReportandGetPrimaryKeySql = "INSERT INTO Report (urlId, fetchTime, performance) VALUES (@UrlId, @FetchTime, @Performance); SELECT SCOPE_IDENTITY() as PrimaryKeyId";
        var reportPrimaryKey = await connection.QueryFirstAsync<PKey>(insertReportandGetPrimaryKeySql, new Report { UrlId = report.UrlId, FetchTime = report.FetchTime, Performance = report.Performance });

        return reportPrimaryKey.PrimaryKeyId;
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

    public static async Task SetPerfMetric(IConfiguration configuration, int reportId, PerfMetric perfMetric)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        perfMetric.ReportId = reportId;
        string insertFCPSql = "INSERT INTO PerfMetric(reportId, score, numericValue, displayValue, title) VALUES (@ReportId, @Score, @NumericValue, @DisplayValue, @Title)";
        await connection.ExecuteAsync(insertFCPSql, perfMetric);
      }
    }


    public static async Task<IEnumerable<Report>> GetReportsByUrlId(IConfiguration configuration, int urlId)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        //Check if urlId exists?
        var reportsSql = "SELECT * FROM Report WHERE urlId = @urlId";
        var reports = await connection.QueryAsync<Report>(reportsSql, new { urlId = urlId });
        return reports;
      }
    }
    public static async Task<IEnumerable<PerfMetric>> GetPerfMetricsByReportId(IConfiguration configuration, int reportId)
    {
      using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        connection.Open();
        var perfMetricsSql = "SELECT * FROM PerfMetric WHERE reportId = @reportId";
        var perfMetrics = await connection.QueryAsync<PerfMetric>(perfMetricsSql, new { reportId = reportId });
        return perfMetrics;
      }
    }
  }
}
