﻿using System;
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




namespace LandPerf.api
{
  [Route("api/[controller]")]
  public class LighthouseController : Controller
  {

    private IConfiguration _config;
    private INodeServices _nodeServices;
    private LighthouseBLL _BLL;

    public LighthouseController(IConfiguration Configuration, INodeServices NodeServices)
    {
      _config = Configuration;
      _nodeServices = NodeServices;
      _BLL = new LighthouseBLL(Configuration, NodeServices);
    }


    [HttpGet]
    public async Task<IEnumerable<Url>> Get()
    {
      IEnumerable<Url> urls = await LighthouseRepository.GetUrls(_config);

      foreach (Url url in urls)
      {
        await _BLL.runLightHouseAndSetReport(url);
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
