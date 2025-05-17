using Microsoft.AspNetCore.Mvc;
using WebAPIi.Models;
using BL.Dto;
using DataAccessLayer.Exceptions;
using BL.Interfase;
using Domain;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly ITeacherSubject _teacherSubject;

        public TeacherSubjectController(ITeacherSubject teachsSub)
        {          
            _teacherSubject = teachsSub;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<TeacherSubjectDto>>> Get()
        {
            try
            {
                var data = _teacherSubject.GetAll();
                return Ok(ApiResponse<List<TeacherSubjectDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<TeacherSubjectDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<TeacherSubjectDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<List<TeacherSubjectDto>>> Delete(Guid id)
        {
            try
            {
                var data = _teacherSubject.ShangeStatus(id, 0);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        } 
        
        [HttpPost]
        public ActionResult<ApiResponse<List<TeacherSubjectDto>>> Add(TeacherSubjectDto studentDto)
        {
            try
            {
                var data = _teacherSubject.Add(studentDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<TeacherSubjectDto>> Get(Guid id)
        {
            try
            {
                var data = _teacherSubject.GetById(id);
                return Ok(ApiResponse<TeacherSubjectDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

        [HttpPut]
        public ActionResult<ApiResponse<List<TeacherSubjectDto>>> put(TeacherSubjectDto city)
        {
            try
            {
                var data = _teacherSubject.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<TeacherSubjectDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }


    }
}
