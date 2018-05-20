using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MortgageCalculator.Web
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
        }
    }
}