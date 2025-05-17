using Microsoft.AspNetCore.Mvc;
using WebAPIi.Models;
using BL.Dto;
using DataAccessLayer.Exceptions;
using BL.Interfase;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher _teacher;

        public TeacherController(ITeacher teacher)
        {
          
            _teacher = teacher;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<TeacherDto>>> Get()
        {
            try
            {
                var data = _teacher.GetAll();
                return Ok(ApiResponse<List<TeacherDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<TeacherDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<TeacherDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<List<TeacherDto>>> Delete(Guid id)
        {
            try
            {
                var data = _teacher.ShangeStatus(id, 0);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        } 
        
        [HttpPost]
        public ActionResult<ApiResponse<List<TeacherDto>>> Add(TeacherDto studentDto)
        {
            try
            {
                var data = _teacher.Add(studentDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<TeacherDto>> Get(Guid id)
        {
            try
            {
                var data = _teacher.GetById(id);
                return Ok(ApiResponse<TeacherDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

        [HttpPut]
        public ActionResult<ApiResponse<List<TeacherDto>>> put(TeacherDto city)
        {
            try
            {
                var data = _teacher.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

    }
}
