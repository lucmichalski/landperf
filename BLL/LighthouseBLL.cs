using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using LandPerf.Repository;
using LandPerf.Models;
using Microsoft.AspNetCore.NodeServices;



namespace LandPerf.BLL
{
  public class LighthouseBLL
  {

    private static IConfiguration _config;
    private static INodeServices _nodeServices;

    public LighthouseBLL(IConfiguration Configuration, INodeServices NodeServices)
    {
      _config = Configuration;
      _nodeServices = NodeServices;
    }

    /// <summary>Run a Lighthouse performance audit and set the report and perf metrics in the database.
    /// <para>Does not run reports in parallel</para>
    /// </summary>
    public async Task runLightHouseAndSetReport(Url url)
    {
      dynamic lhr = await runLightHouse(_nodeServices, url.Name);
      Report report = createReport(lhr, url);
      IEnumerable<PerfMetric> perfMetrics = createPerfMetrics(lhr);
      int reportId = await LighthouseRepository.SetReport(_config, report);
      foreach (PerfMetric perfMetric in perfMetrics)
      {
        await LighthouseRepository.SetPerfMetric(_config, reportId, perfMetric);
      }
    }
    public async Task<dynamic> runLightHouse([FromServices] INodeServices nodeServices, string url)
    {
      dynamic lhr = await nodeServices.InvokeAsync<dynamic>("./lighthouse", url);
      return lhr;
    }

    private Report createReport(dynamic lhr, Url url)
    {
      DateTime lhrFetchTime = lhr.fetchTime;
      int performance = lhr.categories.performance.score * 100;
      TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
      Report report = new Report
      {
        UrlId = url.Id,
        FetchTime = TimeZoneInfo.ConvertTimeFromUtc(lhrFetchTime, cstZone).ToString(),
        Performance = performance
      };
      return report;
    }
    private IEnumerable<PerfMetric> createPerfMetrics(dynamic lhr)
    {
      int performance = lhr.categories.performance.score * 100;
      var lhrFCP = lhr.audits["first-contentful-paint"];
      var lhrFMP = lhr.audits["first-meaningful-paint"];
      var lhrSI = lhr.audits["speed-index"];
      var lhrEIL = lhr.audits["estimated-input-latency"];
      var lhrMPF = lhr.audits["max-potential-fid"];
      var lhrTBT = lhr.audits["total-blocking-time"];
      var lhrTTFB = lhr.audits["time-to-first-byte"];
      var lhrI = lhr.audits["interactive"];
      var lhrFCI = lhr.audits["first-cpu-idle"];
      IEnumerable<dynamic> metrics = new[] { lhrFCP, lhrFMP, lhrSI, lhrEIL, lhrMPF, lhrTBT, lhrTTFB, lhrI, lhrFCI };

      IEnumerable<PerfMetric> basePerfMetrics = metrics.Select(metric => new PerfMetric
      {
        DisplayValue = metric.displayValue,
        NumericValue = metric.numericValue,
        Score = metric.score * 100,
        Title = metric.title
      });

      IEnumerable<PerfMetric> performanceMetric = new[] {new PerfMetric {
        DisplayValue = performance.ToString(),
        NumericValue = performance,
        Score = performance,
        Title = "Overall Performance"
      }};

      IEnumerable<PerfMetric> perfMetrics = performanceMetric.Concat(basePerfMetrics);
      return perfMetrics;
    }
  }
}
