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
    public class ClassController : ControllerBase
    {
        private readonly IClass _classes;

        public ClassController(IClass classes)
        {
          
            _classes = classes;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ClassDto>>> Get()
        {
            try
            {
                var data = _classes.GetAll();
                return Ok(ApiResponse<List<ClassDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<ClassDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<ClassDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<List<ClassDto>>> Delete(Guid id)
        {
            try
            {
                var data = _classes.ShangeStatus(id, 0);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        } 
        
        [HttpPost]
        public ActionResult<ApiResponse<List<ClassDto>>> Add(ClassDto studentDto)
        {
            try
            {
                var data = _classes.Add(studentDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ClassDto>> Get(Guid id)
        {
            try
            {
                var data = _classes.GetById(id);
                return Ok(ApiResponse<ClassDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

        [HttpPut]
        public ActionResult<ApiResponse<List<ClassDto>>> put(ClassDto city)
        {
            try
            {
                var data = _classes.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ClassDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

    }
}
