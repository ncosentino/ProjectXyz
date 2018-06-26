using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Framework.ViewWelding.Api;

namespace ProjectXyz.Framework.ViewWelding
{
    public sealed class ViewWelderFactory : IViewWelderFactoryFacade
    {
        private readonly List<Tuple<CanWeldDelegate, CreateWelderDelegate>> _mappings;

        public ViewWelderFactory()
        {
            _mappings = new List<Tuple<CanWeldDelegate, CreateWelderDelegate>>();
        }

        public TViewWelder Create<TViewWelder>(object parent, object child)
            where TViewWelder : IViewWelder
        {
            var supportedMapping = _mappings.FirstOrDefault(x => x.Item1(parent, child, typeof(TViewWelder)));
            if (supportedMapping == null)
            {
                throw new InvalidOperationException(
                    $"No supported view welder regiserted for welding '{child}' into '{parent}' via welder type '{typeof(TViewWelder)}'.");
            }

            var welder = supportedMapping.Item2(parent, child);
            return (TViewWelder)welder;
        }

        public void Register(
            CanWeldDelegate canWeldCallback,
            CreateWelderDelegate createWelderCallback)
        {
            _mappings.Add(new Tuple<CanWeldDelegate, CreateWelderDelegate>(
                canWeldCallback,
                createWelderCallback));
        }
    }
}
