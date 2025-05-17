using Microsoft.AspNetCore.Mvc;
using WebAPIi.Models;
using BL.Dto;
using DataAccessLayer.Exceptions;
using BL.Interfase;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGrade _Grade;

        public GradeController(IGrade grade)
        {
          
            _Grade = grade;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<GradeDto>>> Get()
        {
            try
            {
                var data = _Grade.GetAll();
                return Ok(ApiResponse<List<GradeDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<GradeDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<GradeDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<List<GradeDto>>> Delete(Guid id)
        {
            try
            {
                var data = _Grade.ShangeStatus(id, 0);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        } 
        
        [HttpPost]
        public ActionResult<ApiResponse<List<GradeDto>>> Add(GradeDto studentDto)
        {
            try
            {
                var data = _Grade.Add(studentDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<GradeDto>> Get(Guid id)
        {
            try
            {
                var data = _Grade.GetById(id);
                return Ok(ApiResponse<GradeDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }



        [HttpPut]
        public ActionResult<ApiResponse<List<GradeDto>>> put(GradeDto city)
        {
            try
            {
                var data = _Grade.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<GradeDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    }
}
