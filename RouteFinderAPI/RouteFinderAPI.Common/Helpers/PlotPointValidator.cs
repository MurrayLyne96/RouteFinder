using System.Linq;
namespace RouteFinderAPI.Common.Helpers;
public class PlotPointValidator
{
    public static bool AllPlotPointsUnique(int[] PlotPointOrders) =>
        PlotPointOrders.Distinct().Count() == PlotPointOrders.Count();

    public static bool AllPlotPointsinOrder(int[] PlotpointOrders)
    {
        int lastPoint = 0;
        foreach (var point in PlotpointOrders)
        {
            if (point != lastPoint) return false;
            lastPoint++;
        }
        return true;
    }
}