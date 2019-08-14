﻿using System;
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
      string fetchTime = lhr.fetchTime;
      double performance = lhr.categories.performance.score;

      Report report = new Report
      {
        UrlId = url.Id,
        FetchTime = fetchTime,
        Performance = performance
      };

      LighthouseRepository.SetReport(_config, report);
    }
    public async Task<dynamic> runLightHouse([FromServices] INodeServices nodeServices, string url)
    {
      dynamic lhr = await nodeServices.InvokeAsync<dynamic>("./lighthouse", url);
      return lhr;
    }
  }
}
