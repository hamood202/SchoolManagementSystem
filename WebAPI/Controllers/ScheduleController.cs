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
    public class ScheduleController : ControllerBase
    {
        private readonly ISchedule _Schedule;

        public ScheduleController(ISchedule Schedule)
        {
          
            _Schedule = Schedule;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ScheduleDto>>> Get()
        {
            try
            {
                var data = _Schedule.GetAll();
                return Ok(ApiResponse<List<ScheduleDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<ScheduleDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<ScheduleDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<List<ScheduleDto>>> Delete(Guid id)
        {
            try
            {
                var data = _Schedule.ShangeStatus(id, 0);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        } 
        
        [HttpPost]
        public ActionResult<ApiResponse<List<ScheduleDto>>> Add(ScheduleDto studentDto)
        {
            try
            {
                var data = _Schedule.Add(studentDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ScheduleDto>> Get(Guid id)
        {
            try
            {
                var data = _Schedule.GetById(id);
                return Ok(ApiResponse<ScheduleDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }


        [HttpPut]
        public ActionResult<ApiResponse<List<ScheduleDto>>> put(ScheduleDto city)
        {
            try
            {
                var data = _Schedule.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<ScheduleDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

    }
}
