using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Core.Entitys;
using PTP.Core.Servecis;
using PTP.Core.Specification;
using PTP.Infrastructure.Proxies.Request;
using PTP.Queing.Meassage;
using PTP.Queuing.RabbitMqService.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleintController : BaseConroler<Cleint, CleintFilter>
    {
        private readonly IHttpContextAccessor contextAccessor;
        public CleintController(ICleintServices Service, ILogger<Cleint> loger, IMapper _mapper
            , Specification<Cleint> specification, IHttpContextAccessor contextAccessor
            ) : base(Service, loger, _mapper, specification)
        {
                this.contextAccessor = contextAccessor;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> add([FromBody] ClientDto entity)
        {

            Cleint Entity = MapProperties<Cleint>(entity);
            if (isValidSpecefication(Entity))
                throw new ValidationException();
            int id = await Create(Entity);
            return StatusCode(StatusCodes.Status201Created, id);


        }
        //move to auth this for register a user
        [AllowAnonymous]
        [HttpPost("AddUsr")]
        public async Task<IActionResult> addUsr([FromBody] CreateUserRequest entity)
        {
           // ResultEntity<int> Response = await cleintServices.CretaUserWithRefit(entity);
            //return StatusCode(StatusCodes.Status201Created, Response);

            // move this commaent to speacefic service and ad the streategy pattern to create
            // speacfic type of MQ meassage;

              CreateUserMessage message = new CreateUserMessage()
              {
                  Name = entity.name,
                  Password = entity.Password,
              };
              var serviceCollection = new ServiceCollection();
              IServiceProvider ServiceProvider=serviceCollection.BuildServiceProvider();
              var Publisher = contextAccessor.HttpContext.RequestServices.GetService<IQuenigService<CreateUserMessage>>();
             await Publisher.PublishToMQ(message);
            return Ok();
        }
        protected ICleintServices cleintServices
        {
            get => Service as ICleintServices;
        }


      

        // [HttpGet("{id}")]
        /* public async Task<IActionResult> add(int id)
         {

             return StatusCode(StatusCodes.Status201Created, id);
         }
        */
    }
}
