using DinoSkore.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinoSkore.Api.Controllers;

[ApiController]
[Route("competitions")]
public class CompetitionsController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompetitions()
    {
        var data = await dbContext.Competitions.ToListAsync();

        return new OkObjectResult(data);
    }
}
