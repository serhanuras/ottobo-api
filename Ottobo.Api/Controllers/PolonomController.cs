using Microsoft.AspNetCore.Mvc;
using Ottobo.Api.Attributes;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class PolonomController:ControllerBase
    {
        /// <summary>
        /// Navigasyonu başlatan api.
        /// </summary>
        /// <returns></returns>
        [HttpPut("/map/navigate_start")]
        public ActionResult NavigateStart([FromBody] NavigateLocation navigateLocation)
        {

            return Ok();
        }
        
        /// <summary>
        /// Get Robot Position
        /// </summary>
        /// <returns></returns>
        [HttpGet("/robot_position_yaw")]
        public ActionResult<InitPose> NavigateStart()
        {

            return Ok(new InitPose()
            {
                X = "0.0620791527752",
                Y = "0.144764130959",
                Yaw = "-0.0077826343616321115"
            });
        }
        
        
        /// <summary>
        /// Get Robot Position
        /// </summary>
        /// <returns></returns>
        [HttpGet("/robot_status")]
        public ActionResult RobotStatus()
        {

            return Ok(new 
            {
                Status = "idle"
            });
        }
        
        
        /// <summary>
        /// Get Robot Position
        /// </summary>
        /// <returns></returns>
        [HttpPut("/set_idle")]
        public ActionResult RobotSetIdle()
        {

            return Ok(new 
            {
            });
        }
        
    }

    public class NavigateLocation
    {
        public int MapId { get; set; }
        
        public InitPose InitPose { get; set; }
        
    }

    public class InitPose
    {
        public string X { get; set; }
        
        public string Y { get; set; }
        
        public string Theta { get; set; }
        
        public string Yaw { get; set; }
        
    }
}