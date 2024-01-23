using Domain.DTOs;
using Infrastructure.Common;
using Infrastructure.Enums;
using Infrastructure.Refit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Echo_Task.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly IComplaintAPI _complaintAPI;
        private readonly IWebHostEnvironment _environment;
        private readonly IHost host;

        public ComplaintsController(IComplaintAPI complaintAPI, IWebHostEnvironment webHostEnvironment, IHost host)
        {
            _complaintAPI = complaintAPI;
            _environment = webHostEnvironment;
            this.host = host;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllComplaints()
        {
            List<ComplaintDto> Complaints = new List<ComplaintDto>();
            ReturnResult returnResult = await _complaintAPI.GetAllComplaints();
            if (returnResult.Status == Enums.ResultStatus.Error.ToString())
            {
                return BadRequest(returnResult);
            }
            else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
            {
                return RedirectToAction("Login", "Account");
            }
            return Ok(returnResult);
        }

        [HttpGet]
        [Route("AddComplaint")]
        public IActionResult Add()
        {
            return View("Create");
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateComplaint(ComplaintDto complaint)
        {
            ReturnResult returnResult;            
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Please check your data..!"
                    });
                }
                var fileExtension = Path.GetExtension(complaint.UserIdentity.FileName);
                string nameOfImage = $"{fileExtension}";
                var filePath = Path.Combine(_environment.WebRootPath, "Images", nameOfImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await complaint.UserIdentity.CopyToAsync(stream);
                }
                complaint.UserIdentityFileName = nameOfImage;
                var usersClient = host.Services.GetRequiredService<IComplaintAPI>();
                returnResult = await usersClient.Add(complaint);
                if (returnResult.Status == Enums.ResultStatus.Error.ToString())
                {
                    return BadRequest(returnResult);
                }
                else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
                {
                    return RedirectToAction("Login", "Account");
                }
                return Ok(returnResult);
            }
            catch (Refit.ApiException ex)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = ex.Message +"Somthing went wrong...! \n Please try again later"
                });
            }
            catch (Exception)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                });
            }
        }

        [HttpPost]
        [Route("Delete/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid complaintId)
        {
            try
            {
                ReturnResult returnResult = await _complaintAPI.Delete(complaintId);
                if (returnResult.Status == Enums.ResultStatus.Error.ToString())
                {
                    return BadRequest(returnResult);
                }
                else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
                {
                    return RedirectToAction("Login", "Account");
                }
                return Ok(returnResult);
            }
            catch (Exception)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                });
            }
        }

        [HttpPost]
        [Route("Update/{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] ComplaintDto complaint)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Please check your data..!"
                    });
                }
                ReturnResult returnResult = await _complaintAPI.Update(Id, complaint);
                if (returnResult.Status == Enums.ResultStatus.Error.ToString())
                {
                    return BadRequest(returnResult);
                }
                else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
                {
                    return RedirectToAction("Login", "Account");
                }
                return Ok(returnResult);
            }
            catch (Exception)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                });
            }
        }

        [HttpPost]
        [Route("Reject/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(Guid Id)
        {
            try
            {
                ReturnResult returnResult = await _complaintAPI.Reject(Id);
                if (returnResult.Status == Enums.ResultStatus.Error.ToString())
                {
                    return BadRequest(returnResult);
                }
                else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
                {
                    return RedirectToAction("Login", "Account");
                }
                return Ok(returnResult);
            }
            catch (Exception)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                });
            }
        }
        [HttpPost]
        [Route("Approve/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(Guid Id)
        {
            try
            {
                ReturnResult returnResult = await _complaintAPI.Approve(Id);
                if (returnResult.Status == Enums.ResultStatus.Error.ToString())
                {
                    return BadRequest(returnResult);
                }
                else if (returnResult.Status == Enums.ResultStatus.NotAuthorize.ToString())
                {
                    return RedirectToAction("Login", "Account");
                }
                return Ok(returnResult);
            }
            catch (Exception)
            {
                return BadRequest(new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                });
            }
        }
    }
}
