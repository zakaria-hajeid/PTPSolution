using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PTP.Core.Common;
using PTP.Core.Entities;
using PTP.Core.Servecis;
using PTP.Core.Specification;
using System;
using System.Threading.Tasks;

namespace PTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseConroler<T, M> : ControllerBase, IDisposable where T : EntityBase where M : EntityBaseFilter
    {
        public readonly IMapper _mapper;
        public readonly Specification<T> _specification;

        private readonly IServices<T, M> _Service;
        private readonly ILogger<T> _loger;

        protected IServices<T, M> Service { get; }

        public BaseConroler(IServices<T, M> Service, ILogger<T> loger, IMapper mapper
            , Specification<T> specification
            )
        {
            _loger = loger;
            _Service = Service;
            _mapper = mapper;
            _specification = specification;
            this.Service = Service;

        }
        public async Task<T> GetById(int id)
        {

            return await _Service.getByID(id);

        }
        public async Task<int> Create(T entity)
        {
            return await _Service.Create(entity);
        }

      



        protected TDestination MapProperties<TDestination>(object source)
      where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            return _mapper.Map(source, destination); ;
        }

        protected bool isValidSpecefication(T entity)
        {
            return _specification.IsSatisfiedBy(entity);

        }



    }
}
