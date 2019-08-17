using System;
namespace LandPerf.Models
{
  public class PerfMetric
  {

    public int ReportId { get; set; }
    public string DisplayValue { get; set; }
    public double Score { get; set; }
    public double NumericValue { get; set; }
    public string Title { get; set; }

  }
}
