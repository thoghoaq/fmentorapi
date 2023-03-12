using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/donation")]
    [ApiController]
    public class DonateController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public DonateController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Create a donation
        /// </summary>
        /// <param name="donateRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public DonateResponseModel CreateDonate(DonateRequestModel donateRequestModel)
        {
            return _paymentService.CreateDonate(donateRequestModel);
        }
    }
}
