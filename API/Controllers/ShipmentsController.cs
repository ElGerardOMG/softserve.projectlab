using API.DTOs;
using API.implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentsDomain _processor;
        public ShipmentsController(IShipmentsDomain delivieriesDomain)
        {
            _processor = delivieriesDomain;
        }
        [HttpPost]
        public IActionResult CreateDelivery([FromBody] CreateShipmentDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CreateDelivery(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("{idShipment}")]
        public IActionResult GetDelivery(int idShipment)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetDelivery(idShipment)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("GetByUser/{idUser}")]
        public IActionResult GetDeliveriesByUser(int idUser, FilterDTO? filters)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetDeliveriesByUser(idUser, filters)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPut("{idShipment}")]
        public IActionResult UpdateDelivery(int idShipment, [FromBody]ShipmentDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateDelivery(idShipment, obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPatch("{idProductVariant}")]
        public IActionResult UpdateDeliveryStatus(int idProductVariant, string status)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateDeliveryStatus(idProductVariant, status)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpDelete("{idProductVariant}")]
        public IActionResult DeleteDelivery(int idProductVariant)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.DeleteDelivery(idProductVariant)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

    }
}
