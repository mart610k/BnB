﻿using bnbAPI.DTO;
using bnbAPI.Logic;
using bnbAPI.Service;
using bnbAPI.Static;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomLogic logic = new RoomLogic();

        [HttpGet("list")]
        public IActionResult GetSimpleRooms()
        {
            return Ok(logic.GetSimpleRooms());
        }

        [HttpGet("room")]
        public IActionResult GetDetailedRoom([FromQuery] int id)
        {
            return Ok(logic.GetDetailedRoom(id));
        }
        [HttpGet("search")]
        public IActionResult GetRoomBySearch([FromQuery] string param)
        {
            int[] test = new int[0];
            if (param != null)
            {
                string[] arry = param.Split(",");
                test = new int[arry.Length];
                for (int i = 0; i < arry.Length; i++)
                {
                    test[i] = (Convert.ToInt32(arry[i]));
                }
            }

            return Ok(logic.GetRoomsBySearch(test));
        }

        [HttpPost("create")]
        public IActionResult CreateRoom([FromHeader] string authorization, [FromBody] CreateRoomDTO roomDTO)
        {
            try
            {
                return StatusCode(200, logic.CreateRoom(AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization),roomDTO));

            }
            catch(Exception e)
            {
                MessageDTO message = HttpStatusCodeService.GetMessageDTOFromException(e);

                return StatusCode(message.StatusCode, message);
            }
        }

        [HttpPost("order")]
        public IActionResult OrderRoom([FromHeader] string authorization, [FromBody] BookedRoomDTO bookedroomdto)
        {
            logic.BookRoom(AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization), bookedroomdto);
            return StatusCode(200);
        }
    }
}
