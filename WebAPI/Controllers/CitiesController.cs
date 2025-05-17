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
    public class CitiesController : ControllerBase
    {
        private readonly ICity _City;

        public CitiesController(ICity city)
        {
          
            _City = city;
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<CityDto>>> Get()
        {
            try
            {
                var data = _City.GetAll();
                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(data));

            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse(
                   "data access exception" ,new List<string> { ex.Message }));
            }
            catch (Exception ex) 
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }
    
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CityDto>> Get(Guid id)
        {
            try
            {
                var data = _City.GetById(id);
                return Ok(ApiResponse<CityDto>.SuccessResponse(data));
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

        [HttpPost]
        public ActionResult<ApiResponse<List<CityDto>>> Add(CityDto cityDto)
        {
            try
            {
                var data = _City.Add(cityDto);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

        [HttpPut]
        public ActionResult<ApiResponse<List<CityDto>>> put(CityDto city)
        {
            try
            {
                var data = _City.Update(city);
                return Ok();
            }
            catch (DataAccessException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "data access exception", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return StatusCode(500, ApiResponse<CityDto>.FailResponse(
                   "general exception", new List<string> { ex.Message }));
            }
        }

    }
}
