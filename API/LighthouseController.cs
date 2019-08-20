using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using LandPerf.Repository;
using LandPerf.Models;
using LandPerf.BLL;
using Microsoft.AspNetCore.NodeServices;




namespace LandPerf.API
{
  [Route("api/[controller]")]
  public class LighthouseController : Controller
  {

    private IConfiguration _config;
    private INodeServices _nodeServices;
    private LighthouseBLL _LighthouseBLL;

    public LighthouseController(IConfiguration Configuration, INodeServices NodeServices)
    {
      _config = Configuration;
      _nodeServices = NodeServices;
      _LighthouseBLL = new LighthouseBLL(Configuration, NodeServices);
    }


    [HttpGet("reports/set")]
    public async Task<IEnumerable<Url>> SetLighthouseReports()
    {
      IEnumerable<Url> urls = await LighthouseRepository.GetUrls(_config);

      foreach (Url url in urls)
      {
        await _LighthouseBLL.runLightHouseAndSetReport(url);
      }
      return urls;
    }

    [HttpGet("reports/{id}")]
    public async Task<IEnumerable<Report>> GetReports(int id)
    {
      var reports = await LighthouseRepository.GetReportsByUrlId(_config, id);
      return reports;
    }

    [HttpGet("perfmetrics/{reportId}")]
    public async Task<IEnumerable<PerfMetric>> GetPerfMetricsByReportId(int reportId)
    {
      var perfMetrics = await LighthouseRepository.GetPerfMetricsByReportId(_config, reportId);
      return perfMetrics;
    }


    [HttpGet("urls")]
    public async Task<IEnumerable<Url>> GetUrls()
    {
      IEnumerable<Url> urls = await LighthouseRepository.GetUrls(_config);
      return urls;
    }


  }
}
