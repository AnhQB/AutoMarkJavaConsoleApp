using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.DTO;
using project.Services;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCaseController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = TestCaseService.GetSingleton().GetTestCases();
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
                var data = TestCaseService.GetSingleton().GetTestCase(id);
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
        public IActionResult Add(TestCaseDTO TestCase)
        {
            try
            {
                TestCaseService.GetSingleton().Add(TestCase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(TestCaseDTO TestCase)
        {
            try
            {
                TestCaseService.GetSingleton().Update(TestCase);
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
                TestCaseService.GetSingleton().Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
