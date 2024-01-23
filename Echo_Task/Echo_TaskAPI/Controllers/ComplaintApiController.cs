using AutoMapper;
using Domain.DTOs;
using Domain.Entiteis;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace Echo_TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintApiController : ControllerBase
    {
        private readonly IComplaintSvc _Complaint;
        private readonly IDemandSvc _Demand;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ComplaintApiController(IComplaintSvc complaint, IConfiguration configuration, IMapper mapper, IWebHostEnvironment environment)
        {
            _Complaint = complaint;
            _Configuration = configuration;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpPost]
        [Route("AddComplaint")]
        public async Task<ReturnResult> AddComplaint([FromBody] ComplaintDto complaintDto)
        {
            Complaint complaint = new Complaint();
            Guid complaintId = Guid.NewGuid();
            int result;
            try
            {
                complaintDto.Id = complaintId;
                complaintDto.Status = (int)Enums.ComplaintStatus.Pending;
                complaint = _mapper.Map<Complaint>(complaintDto);
                complaint.Status = (int)Enums.ComplaintStatus.Pending;
                result = await _Complaint.Insert(complaint);
                if (result > 0)
                {
                    //if (complaintDto.Demands != null && complaintDto.Demands.Count > 0)
                    //{
                    //    foreach (var item in complaint.Demands)
                    //    {
                    //        item.Complaint_Id = complaintId;
                    //        await _Demand.Insert(item);
                    //    }
                    //}
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Success.ToString(),
                        Code = "200",
                        Data = complaintDto,
                        Message = "Your complaint added successfully\n please wait our response"
                    };
                }
                else
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpGet]
        [Route("GetAllComplaints")]
        public async Task<ReturnResult> GetAll()
        {
            List<Complaint> complaints = new List<Complaint>();
            try
            {
                complaints = (List<Complaint>)await _Complaint.GetAll();
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Success.ToString(),
                    Code = "200",
                    Data = complaints,
                    Message = "Success"
                };
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpGet]
        [Route("GetComplaint/{Id}")]
        public async Task<ReturnResult> GetById(Guid Id)
        {
            Complaint complaint = new Complaint();
            try
            {
                complaint = await _Complaint.Get(Id);
                if (complaint == null)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
                else
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Success.ToString(),
                        Code = "200",
                        Data = complaint,
                        Message = ""
                    };
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpPost]
        [Route("DeleteComplaint/{Id}")]
        public async Task<ReturnResult> Delete(Guid Id)
        {
            int result = 0;
            Complaint complaint = new Complaint();
            List<Demand> demand = new List<Demand>();
            try
            {
                complaint = await _Complaint.Get(Id);
                if (complaint != null)
                {
                    complaint.ModifyOn = DateTime.Now;
                    complaint.IsDeleted = true;
                    result = await _Complaint.Delete(complaint);
                }
                if (result > 0)
                {
                    demand = await _Demand.GetAllByComplaintId(Id);
                    if (demand != null && demand.Count > 0)
                    {
                        foreach (var item in demand)
                        {
                            item.IsDeleted = true;
                            item.ModifyOn = DateTime.Now;
                            await _Demand.Delete(item);
                        }
                    }
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Success.ToString(),
                        Code = "200",
                        Data = null,
                        Message = "The complaint deleted successfully...!"
                    };
                }
                else
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpPost]
        [Route("UpdateComplaint/{Id}")]
        public async Task<ReturnResult> Update(Guid Id, [FromBody] ComplaintDto complaintDto)
        {
            Complaint complaint = new Complaint();
            Complaint getComplaint = new Complaint();
            int result;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Please check your data..!"
                    };
                }
                else
                {
                    getComplaint = await _Complaint.Get(Id);
                    if (getComplaint == null)
                    {
                        return new ReturnResult
                        {
                            Status = Enums.ResultStatus.Error.ToString(),
                            Code = "900",
                            Data = null,
                            Message = "Somthing went wrong...! \n Please try again later"
                        };
                    }
                    else
                    {
                        complaint = _mapper.Map<Complaint>(complaintDto);
                        result = await _Complaint.Update(complaint);
                        if (result > 0)
                        {
                            return new ReturnResult
                            {
                                Status = Enums.ResultStatus.Success.ToString(),
                                Code = "200",
                                Data = complaintDto,
                                Message = "Your complaint updated successfully...!"
                            };
                        }
                        else
                        {
                            return new ReturnResult
                            {
                                Status = Enums.ResultStatus.Error.ToString(),
                                Code = "900",
                                Data = null,
                                Message = "Somthing went wrong...! \n Please try again later"
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpPost]
        [Route("RejectComplaint/{Id}")]
        public async Task<ReturnResult> Reject(Guid Id)
        {
            Complaint complaint = new Complaint();
            int result;
            try
            {
                complaint = await _Complaint.Get(Id);
                if (complaint == null)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
                else if (complaint.Status == (int)Enums.ComplaintStatus.Approved)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "This Complaint already approved"
                    };
                }
                else if (complaint.Status == (int)Enums.ComplaintStatus.Rejected)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "This Complaint already rejected"
                    };
                }
                complaint.Status = (int)Enums.ComplaintStatus.Rejected;
                complaint.ModifyOn = DateTime.Now;
                result = await _Complaint.Update(complaint);
                if (result > 0)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Success.ToString(),
                        Code = "200",
                        Data = complaint,
                        Message = "The complaint rejected successfully."
                    };
                }
                else
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }

        [HttpPost]
        [Route("ApproveComplaint/{Id}")]
        public async Task<ReturnResult> Approve(Guid Id)
        {
            Complaint complaint = new Complaint();
            int result;
            try
            {
                complaint = await _Complaint.Get(Id);
                if (complaint == null)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
                else if (complaint.Status == (int)Enums.ComplaintStatus.Approved)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "This Complaint already approved"
                    };
                }
                else if (complaint.Status == (int)Enums.ComplaintStatus.Rejected)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "This Complaint already rejected"
                    };
                }
                complaint.Status = (int)Enums.ComplaintStatus.Approved;
                complaint.ModifyOn = DateTime.Now;
                result = await _Complaint.Update(complaint);
                if (result > 0)
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Success.ToString(),
                        Code = "200",
                        Data = complaint,
                        Message = "The complaint approved successfully."
                    };
                }
                else
                {
                    return new ReturnResult
                    {
                        Status = Enums.ResultStatus.Error.ToString(),
                        Code = "900",
                        Data = null,
                        Message = "Somthing went wrong...! \n Please try again later"
                    };
                }
            }
            catch (Exception)
            {
                return new ReturnResult
                {
                    Status = Enums.ResultStatus.Error.ToString(),
                    Code = "900",
                    Data = null,
                    Message = "Somthing went wrong...! \n Please try again later"
                };
            }
        }
    }
}
