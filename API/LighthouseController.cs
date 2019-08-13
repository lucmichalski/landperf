using System;
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
    public async Task<Report> Get(int id)
    {
      // Insert all urls into table. Four brands with at least 3 routes a piece plus dev sites. Save sql for all of this somehow.
      // Need to loop through urls, run lighthouse, and set data one by one.
      IEnumerable<Url> urls = await Lighthouse.GetUrls(_config);
      Url url = urls.FirstOrDefault();
      dynamic lhr = await runLightHouse(_nodeServices, url.Name);
      string fetchTime = lhr.fetchTime;
      double performance = lhr.categories.performance.score;

      Report report = new Report
      {
        UrlId = url.Id,
        FetchTime = fetchTime,
        Performance = performance
      };

      Lighthouse.SetReport(_config, report);
      return report;
    }
    public async Task<dynamic> runLightHouse([FromServices] INodeServices nodeServices, string url)
    {
      dynamic lhr = await nodeServices.InvokeAsync<dynamic>("./lighthouse", url);
      return lhr;
    }
  }
}
