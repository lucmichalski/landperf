using System;
namespace LandPerf.Models
{
  public class Report
  {
    public int Id { get; }
    public int UrlId { get; set; }
    public string FetchTime { get; set; }
    public int Performance { get; set; }

  }

}