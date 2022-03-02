using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtualAccountAutomation.Core.Interfaces;
using VirtualAccountAutomation.Infrastructure.Dtos;
using VirtualAccountAutomation.Infrastructure.Models;

namespace VirtualAccountAutomation.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VirtualAccountAutomationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VirtualAccountAutomationController(IUnitOfWork unitOfWork, 
                                                  IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet]
        [Route("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var virtualAccounts = await unitOfWork.VirtualAccounts.GetAllAsync();
            
            var virtualAccountsDtos = mapper.Map<IEnumerable<VirtualAccountRequestDto>>(virtualAccounts);
            return Ok (virtualAccountsDtos);
        }

        [HttpGet]
        [Route("GetAccountByMerchantId/{id}")]
        public async Task<IActionResult> GetAccountByMerchantId(string id)
        {
            var data = await unitOfWork.VirtualAccounts.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount(VirtualAccountRequestDto accountDto)
        {
            var virtualAccount = mapper.Map<VirtualAccountRequest>(accountDto);
            var data = await unitOfWork.VirtualAccounts.AddAsync(virtualAccount);
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await unitOfWork.VirtualAccounts.DeleteAsync(id);
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<IActionResult> Update(VirtualAccountRequest pet)
        {
            var data = await unitOfWork.VirtualAccounts.UpdateAsync(pet);
            return Ok(data);
        }





    }
}