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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LandPerf.api
{
  [Route("api/[controller]")]
  public class LighthouseController : Controller
  {

    private static IConfiguration _config;
    private static INodeServices _nodeServices;

    public LighthouseController(IConfiguration Configuration, INodeServices NodeServices)
    {
      _config = Configuration;
      _nodeServices = NodeServices;
    }


    [HttpGet]
    public async Task<IEnumerable<Url>> Get()
    {
      IEnumerable<Url> urls = await LighthouseRepository.GetUrls(_config);

      foreach (Url url in urls)
      {
        await runLightHouseAndSetReport(url);
      }
      return urls;
    }

    [HttpGet("{id}")]
    public async Task<IEnumerable<Report>> Get(int id)
    {
      var reports = await LighthouseRepository.GetReportsByUrlId(_config, id);
      return reports;
    }



    //Where do these non API methods go?
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
      string lhrFetchTime = lhr.fetchTime;
      int performance = lhr.categories.performance.score * 100;
      Report report = new Report
      {
        UrlId = url.Id,
        FetchTime = lhrFetchTime,
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

      IEnumerable<PerfMetric> perfMetrics = basePerfMetrics.Concat(performanceMetric);
      return perfMetrics;
    }
  }
}
