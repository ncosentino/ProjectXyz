using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var triggerTypeId = new StringIdentifier("Elapsed Time");
            var elapsedTimeTriggerSource = new ElapsedTimeTriggerSource(triggerTypeId);

            var registrar = new TriggerRegistrar();
            registrar.RegisterTriggerSource(elapsedTimeTriggerSource);

            var elapsedTimeExpiryTriggerFactory = new ElapsedTimeExpiryTriggerFactory(triggerTypeId);
            var enchantmentExpiryTriggerFactory = new ExpiryTriggerFactory(elapsedTimeExpiryTriggerFactory);
            var activeEnchantmentManager = new ActiveEnchantmentManager(
                enchantmentExpiryTriggerFactory,
                registrar);

            Console.WriteLine("Press enter to add.");

            activeEnchantmentManager.Add(new EnchantHueHue());
            Console.WriteLine("Count: " + activeEnchantmentManager.Enchantments.Count);

            Console.WriteLine("Press enter to trigger.");
            Console.ReadLine();
            elapsedTimeTriggerSource.Update(new Interval<double>(5));

            Console.WriteLine("Count: " + activeEnchantmentManager.Enchantments.Count);
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        public interface IExpiryTriggerComponent : IComponent
        {
            
        }

        public sealed class ElapsedTimeTrigger :BaseTrigger<IElapsedTimeTriggerContext>
        {
            private readonly Action<IIdentifier, ITriggerContext> _triggerCallback;

            public ElapsedTimeTrigger(
                IIdentifier registerableTriggerTypeId,
                Action<IIdentifier, ITriggerContext> triggerCallback)
                : base(registerableTriggerTypeId)
            {
                _triggerCallback = triggerCallback;
            }

            public override bool Trigger(IIdentifier triggerTypeId, IElapsedTimeTriggerContext triggerContext)
            {
                _triggerCallback(triggerTypeId, triggerContext);
                return false;
            }
        }

        public interface IExpiryTriggerFactory
        {
            ITrigger Create(
                IExpiryTriggerComponent expiryTriggerComponent,
                Action<ITrigger> triggeredCallback);
        }

        public interface IElapsedTimeExpiryTriggerFactory
        {
            ITrigger Create(
                IElapsedTimeExpiryTriggerComponent elapsedTimeExpiryTriggerComponent,
                Action<ITrigger> triggeredCallback);
        }

        public sealed class ElapsedTimeExpiryTriggerFactory : IElapsedTimeExpiryTriggerFactory
        {
            private readonly IIdentifier _elapsedTimeTriggerTypeId;

            public ElapsedTimeExpiryTriggerFactory(IIdentifier elapsedTimeTriggerTypeId)
            {
                _elapsedTimeTriggerTypeId = elapsedTimeTriggerTypeId;
            }

            public ITrigger Create(
                IElapsedTimeExpiryTriggerComponent elapsedTimeExpiryTriggerComponent,
                Action<ITrigger> triggeredCallback)
            {
                var trigger = new ExpiryTrigger(
                    _elapsedTimeTriggerTypeId,
                    elapsedTimeExpiryTriggerComponent.Duration,
                    (t, _, __) => triggeredCallback(t));
                return trigger;
            }
        }


        public sealed class ExpiryTriggerFactory : IExpiryTriggerFactory
        {
            private readonly IElapsedTimeExpiryTriggerFactory _elapsedTimeExpiryTriggerFactory;

            public ExpiryTriggerFactory(IElapsedTimeExpiryTriggerFactory elapsedTimeExpiryTriggerFactory)
            {
                _elapsedTimeExpiryTriggerFactory = elapsedTimeExpiryTriggerFactory;
            }

            public ITrigger Create(
                IExpiryTriggerComponent expiryTriggerComponent,
                Action<ITrigger> triggeredCallback)
            {
                if (expiryTriggerComponent is IElapsedTimeExpiryTriggerComponent)
                {
                    return _elapsedTimeExpiryTriggerFactory.Create(
                        (IElapsedTimeExpiryTriggerComponent)expiryTriggerComponent,
                        triggeredCallback);
                }

                throw new NotSupportedException($"No factory is implemented for '{expiryTriggerComponent}'.");
            }
        }

        public sealed class ActiveEnchantmentManager
        {
            private readonly Dictionary<EnchantHueHue, List<ITrigger>> _activeEnchantments;
            private readonly IExpiryTriggerFactory _expiryTriggerFactory;
            private readonly ITriggerRegistrar _triggerRegistrar;

            public ActiveEnchantmentManager(
                IExpiryTriggerFactory expiryTriggerFactory,
                ITriggerRegistrar triggerRegistrar)
            {
                _activeEnchantments = new Dictionary<EnchantHueHue, List<ITrigger>>();
                _expiryTriggerFactory = expiryTriggerFactory;
                _triggerRegistrar = triggerRegistrar;
            }

            public IReadOnlyCollection<EnchantHueHue> Enchantments => _activeEnchantments.Keys;

            public void Add(EnchantHueHue enchantment)
            {
                foreach (var expiryTrigger in enchantment
                    .Components
                    .Get<IExpiryTriggerComponent>()
                    .Select(x => _expiryTriggerFactory.Create(
                        x,
                        t => HandleEnchantmentExpired(enchantment, t))))
                {
                    if (!_activeEnchantments.ContainsKey(enchantment))
                    {
                        _activeEnchantments[enchantment] = new List<ITrigger>();
                    }

                    _activeEnchantments[enchantment].Add(expiryTrigger);
                    _triggerRegistrar.RegisterTrigger(expiryTrigger);
                }
            }

            public void Remove(EnchantHueHue enchantment)
            {
                foreach (var trigger in _activeEnchantments[enchantment])
                {
                    _triggerRegistrar.UnregisterTrigger(trigger);
                }

                _activeEnchantments.Remove(enchantment);
            }

            private void HandleEnchantmentExpired(EnchantHueHue enchantment, ITrigger trigger)
            {
                if (_activeEnchantments[enchantment].Count == 1)
                {
                    _activeEnchantments.Remove(enchantment);
                }
                else if (!_activeEnchantments[enchantment].Remove(trigger))
                {
                    throw new InvalidOperationException($"Attempted to remove trigger '{trigger}' but the collection did not contain it.");
                }
            }
        }

        public interface IElapsedTimeExpiryTriggerComponent : IExpiryTriggerComponent
        {
            IInterval Duration { get; }
        }

        public sealed class ElapsedTimeExpiryTriggerComponent : IElapsedTimeExpiryTriggerComponent
        {
            public ElapsedTimeExpiryTriggerComponent(IInterval duration)
            {
                Duration = duration;
            }

            public IInterval Duration { get; }
        }

        public sealed class EnchantHueHue : IEntity
        {
            public IComponentCollection Components { get; } = new ComponentCollection(
                new ElapsedTimeExpiryTriggerComponent(new Interval<double>(5)));
        }

        public interface ITriggerRegistrar
        {
            void RegisterTrigger(ITrigger trigger);

            void UnregisterTrigger(ITrigger trigger);
        }

        public interface ITriggerSourceRegistrar
        {
            void RegisterTriggerSource(ITriggerSource triggerSource);
        }

        public sealed class TriggerRegistrar : 
            ITriggerRegistrar,
            ITriggerSourceRegistrar
        {
            private readonly Dictionary<IIdentifier, List<ITrigger>> _triggers;

            public TriggerRegistrar()
            {
                _triggers = new Dictionary<IIdentifier, List<ITrigger>>();
            }

            public void RegisterTriggerSource(ITriggerSource triggerSource)
            {
                triggerSource.Triggered += TriggerSource_Triggered;
            }

            public void RegisterTrigger(ITrigger trigger)
            {
                var triggerTypeId = trigger.RegisterableTriggerTypeId;
                if (!_triggers.ContainsKey(triggerTypeId))
                {
                    _triggers[triggerTypeId] = new List<ITrigger>();
                }

                _triggers[triggerTypeId].Add(trigger);
            }

            public void UnregisterTrigger(ITrigger trigger)
            {
                var triggerTypeId = trigger.RegisterableTriggerTypeId;
                _triggers[triggerTypeId].Remove(trigger);
            }

            private void TriggerSource_Triggered(object sender, TriggerEventArgs e)
            {
                List<ITrigger> triggers;
                if (!_triggers.TryGetValue(e.TriggerTypeId, out triggers))
                {
                    return;
                }

                triggers.RemoveAll(x => !x.Trigger(
                    e.TriggerTypeId,
                    e.TriggerContext));
            }
        }

        public interface IElapsedTimeTriggerContext : ITriggerContext
        {
            IInterval Elapsed { get; }
        }

        public sealed class ElapsedTimeTriggerContext : IElapsedTimeTriggerContext
        {
            public ElapsedTimeTriggerContext(IInterval elapsed)
            {
                Elapsed = elapsed;
            }

            public IInterval Elapsed { get; }
        }

        public sealed class ElapsedTimeTriggerSource : ITriggerSource
        {
            private readonly IIdentifier _triggerTypeIdentifier;

            public ElapsedTimeTriggerSource(IIdentifier triggerTypeIdentifier)
            {
                _triggerTypeIdentifier = triggerTypeIdentifier;
            }

            public event EventHandler<TriggerEventArgs> Triggered;

            public void Update(IInterval elapsed)
            {
                Triggered.InvokeIfExists(
                    this,
                    () =>
                    {
                        var triggerContext = new ElapsedTimeTriggerContext(elapsed);
                        return new TriggerEventArgs(
                            _triggerTypeIdentifier,
                            triggerContext);
                    });
            }
        }

        public interface ITriggerContext
        {
            
        }

        public interface ITriggerSource
        {
            event EventHandler<TriggerEventArgs> Triggered;
        }

        public sealed class TriggerEventArgs : EventArgs
        {
            public TriggerEventArgs(
                IIdentifier triggerTypeId,
                ITriggerContext triggerContext)
            {
                TriggerTypeId = triggerTypeId;
                TriggerContext = triggerContext;
            }

            public IIdentifier TriggerTypeId { get; }

            public ITriggerContext TriggerContext { get; }
        }

        public interface ITrigger
        {
            IIdentifier RegisterableTriggerTypeId { get; }

            bool Trigger(IIdentifier triggerTypeId, ITriggerContext triggerContext);
        }

        public interface ITrigger<in TTriggerContext> : 
            ITrigger
            where TTriggerContext : ITriggerContext
        {
            bool Trigger(IIdentifier triggerTypeId, TTriggerContext triggerContext);
        }

        public abstract class BaseTrigger<TTriggerContext> : 
            ITrigger<TTriggerContext>
            where TTriggerContext : ITriggerContext
        {
            protected BaseTrigger(IIdentifier registerableTriggerTypeId)
            {
                RegisterableTriggerTypeId = registerableTriggerTypeId;
            }

            public IIdentifier RegisterableTriggerTypeId { get; }

            public abstract bool Trigger(IIdentifier triggerTypeId, TTriggerContext triggerContext);

            public bool Trigger(IIdentifier triggerTypeId, ITriggerContext triggerContext)
            {
                return Trigger(triggerTypeId, (TTriggerContext)triggerContext);
            }
        }

        public sealed class ExpiryTrigger : BaseTrigger<IElapsedTimeTriggerContext>
        {
            private readonly Action<ITrigger, IIdentifier, ITriggerContext> _triggerCallback;
            private readonly IInterval _target;

            private IInterval _elapsed;

            public ExpiryTrigger(
                IIdentifier registerableTriggerTypeId,
                IInterval duration,
                Action<ITrigger, IIdentifier, ITriggerContext> triggerCallback)
                : base(registerableTriggerTypeId)
            {
                _triggerCallback = triggerCallback;
                _target = duration;
            }

            public override bool Trigger(IIdentifier triggerTypeId, IElapsedTimeTriggerContext triggerContext)
            {
                _elapsed = _elapsed == null 
                    ? triggerContext.Elapsed 
                    : _elapsed.Add(triggerContext.Elapsed);

                if (_elapsed.CompareTo(_target) < 0)
                {
                    return true;
                }

                _triggerCallback(
                    this,
                    triggerTypeId, 
                    triggerContext);
                return false;
            }
        }
    }
}