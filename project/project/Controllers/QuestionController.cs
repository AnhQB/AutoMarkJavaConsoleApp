using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.DTO;
using project.Services;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = QuestionService.GetSingleton().GetQuestions();
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
                var data = QuestionService.GetSingleton().GetQuestion(id);
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

        [HttpGet("{examId}/{paperNo}")]
        public IActionResult Get(int examId, int paperNo)
        {
            try
            {
                var data = QuestionService.GetSingleton().GetQuestion(examId, paperNo);
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
        public IActionResult Add(QuestionDTO question)
        {
            try
            {
                QuestionService.GetSingleton().Add(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(QuestionDTO question)
        {
            try
            {
                QuestionService.GetSingleton().Update(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                QuestionService.GetSingleton().Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
