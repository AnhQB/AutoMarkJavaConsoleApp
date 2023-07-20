using Microsoft.AspNetCore.Mvc;
using project.DTO;
using project.Services;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> ProcessRarAsync([FromForm] IFormFile file,
            [FromForm] int examId, [FromForm] int type)
        {
            try
            {
                if (type == 0)
                {
                    Dictionary<string, Dictionary<string, ScoreExamResultDTO>> result
                        = await ExamResultService.GetSingleton().AutoScoringAsync(file, examId);
                    return Ok(result);
                }
                else {
                    Dictionary<string, ScoreExamResultTestDTO> resultTest = 
                        await ExamResultService.GetSingleton().AutoScoringAsyncTest(file, examId);
                    return Ok(resultTest);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = ExamResultService.GetSingleton().GetExamResults();
                if (!data.Any())
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GradeDetail")]
        public IActionResult GetGradeDetail(int examresultId)
        {
            try
            {
                var data = ExamResultService.GetSingleton().GetGradeDetail(examresultId);
                if (!data.Any())
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
