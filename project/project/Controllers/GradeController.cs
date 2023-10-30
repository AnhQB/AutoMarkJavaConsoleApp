using Microsoft.AspNetCore.Mvc;
using project.DTO;
using project.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Razor.Hosting;

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

                    return Ok(ExamResultService.GetSingleton().ReformatGrade(result));
                }
                else {
                    Dictionary<string, ScoreExamResultTestDTO> resultTest = 
                        await ExamResultService.GetSingleton().AutoScoringAsyncTest(file, examId);
                    return Ok(ExamResultService.GetSingleton().ReformatGradeTest(resultTest));
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
        [HttpPost("Export")]
        public IActionResult ExportToExcel([FromBody] List<ExamResultStudent> exportData)
        {
            /*// Tạo một đối tượng DataTable với hai cột "ID" và "Name"
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PaperNo", typeof(int));
            dataTable.Columns.Add("StudentId", typeof(int));
            dataTable.Columns.Add("Mark", typeof(double));
            dataTable.Columns.Add("GradeNote", typeof(string));

            // Thêm hai hàng dữ liệu vào bảng
            foreach(var item in exportData)
            {
                dataTable.Rows.Add(item.PaperNo, item.StudentId, item.Mark, item.GradeNote);
            }*/

            byte[] reportBytes;
            using (var package = ExamService.GetSingleton().getApplicantsStatistics(exportData))
            {
                reportBytes = package.GetAsByteArray();
            }
            return File(reportBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Grade_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");

            //return new EmptyResult();
        }
    }
}
