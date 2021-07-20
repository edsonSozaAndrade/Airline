using Airport.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Airport.PageModels
{
    public class DetailFlightModel:PageModel
    {
        public async Task<Airline> OnGetAsync(string flightNumber)
        {
            return null;
        }
    }
}
