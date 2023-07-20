using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using project.DTO;
using project.Models;
using project.Services;
using project.Services.IServices;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = ExamService.GetSingleton().GetExamsDistinct();
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

        [HttpGet("All")]
        public IActionResult GetAll ()
        {
            try
            {
                var data = ExamService.GetSingleton().GetExams();
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

        [HttpGet("AllNo")]
        public IActionResult GetAllNo()
        {
            try
            {
                var data = ExamService.GetSingleton().GetExamNo();
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

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = ExamService.GetSingleton().GetExam(id);
                if (data == null)
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

        [HttpGet("{id}/{paperNo}")]
        public IActionResult GetByIdPerNo(int id, int paperNo)
        {
            try
            {
                var data = ExamService.GetSingleton().GetExam(id, paperNo);
                if (data == null)
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

        [HttpPost]
        public IActionResult Add(ExamDTO exam)
        {
            try
            {
                ExamService.GetSingleton().Add(exam);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(ExamDTO exam)
        {
            try
            {
                ExamService.GetSingleton().Update(exam);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/{paperNo}")]
        public IActionResult Delete(int id, int paperNo)
        {
            try
            {
                ExamService.GetSingleton().Delete(id, paperNo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [EnableQuery]
        [HttpGet("OData")]
        public IActionResult GetOData()
        {
            
            return Ok(ExamService.GetSingleton().GetAllExams());
            
            //return Ok(ExamService.GetSingleton().GetAllExams());
        }
    }
}
