using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Shared.Framework.Entities
{
    public sealed class ComponentCollection : IComponentCollection
    {
        #region Fields
        private readonly List<IComponent> _components;
        #endregion

        #region Constructors
        public ComponentCollection(IComponent component, params IComponent[] components)
            : this(component.Yield().Concat(components))
        {
        }

        public ComponentCollection(IEnumerable<IComponent> components)
        {
            _components = new List<IComponent>(components);
        }

        private ComponentCollection()
            : this(Enumerable.Empty<IComponent>())
        {
        }
        #endregion

        #region Properties
        public static IComponentCollection Empty { get; } = new ComponentCollection();

        public int Count => _components.Count;
        #endregion

        #region Methods
        public IEnumerable<TComponent> Get<TComponent>() 
            where TComponent : IComponent
        {
            return _components.TakeTypes<TComponent>();
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}