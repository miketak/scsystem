using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using SCCL.Core.Interfaces;
using SCCL.Infrastructure;

namespace SCCL.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<ISolutionRepository> mock = new Mock<ISolutionRepository>();
            //mock.Setup(m => m.Id).Returns(new List<Solution>
            //{
            //    new Solution() {Name = "Renewable Energy", Description = "Solution 1 Description"},
            //    new Solution() {Name = "Manufacturing Technology", Description = "Solution mt Description"},
            //    new Solution() {Name = "Healthcare", Description = "Solution ht Description"},
            //    new Solution() {Name = "Agriculture", Description = "Solution ag Description"},
            //    new Solution() {Name = "Chemical Process Design", Description = "Solution cpd Description"},
            //    new Solution() {Name = "Consumer Products", Description = "Solution cp Description"}
            //});
            //_kernel.Bind<ISolutionRepository>().ToConstant(mock.Object);


            //Mock<IServiceRepository> servicemock = new Mock<IServiceRepository>();
            //servicemock.Setup(m => m.Services).Returns(new List<Service>
            //{
            //    new Service() {Name = "CAD Drafting", Description = "Service 1 Description"},
            //    new Service() {Name = "Multiphysics Simulation", Description = "Service msimulation Description"},
            //    new Service() {Name = "R & D", Description = "R & D Description"}
            //});
            //_kernel.Bind<IServiceRepository>().ToConstant(servicemock.Object);

            _kernel.Bind<ISolutionRepository>().To<SolutionRepository>();
            _kernel.Bind<IServiceRepository>().To<ServiceRepository>();
            _kernel.Bind<ITestimonialRepository>().To<TestimonialRepository>();
            _kernel.Bind<IContactMessageRepository>().To<ContactMessageRepository>();
            _kernel.Bind<IPortfolioRepository>().To<PortfolioRepository>();
        }
    }
}