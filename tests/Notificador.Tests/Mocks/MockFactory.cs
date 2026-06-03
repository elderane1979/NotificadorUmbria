using Notificador.Contracts.Interfaces;
using Notificador.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Notificador.Tests.Mocks
{

    internal static class MockFactory
    {
        // Store factory functions so each call produces a fresh instance (no shared state across tests)
        private static readonly Dictionary<Type, Func<object>> _factories = new Dictionary<Type, Func<object>>()
        {
            { typeof(IUmbriaClient), () => new UmbriaClientMock() },
            { typeof(IHtmlParser), () => new HtmlParserMock() },
            { typeof(ISettingsProvider), () => new SettingsProviderMock() },
            { typeof(ICryptoService), () => new CryptoServiceMock() }
        };

        // Returns the interface implementation (the .Object of a Moq.Mock<T> or a concrete fake)
        public static T GetMock<T>() where T: class
        {
            if (_factories.TryGetValue(typeof(T), out var factory))
            {
                var instance = factory();

                if (instance is Moq.Mock<T> moq)
                {
                    return moq.Object;
                }

                return (T)instance;
            }

            throw new NotImplementedException($"No mock implemented for type {typeof(T).FullName}");
        }

        // Returns the Moq.Mock<T> instance when the factory produces one; otherwise null.
        // Tests that need to override setups or call Verify should prefer creating their own Moq.Mock<T>
        // and pass it explicitly to the system under test, but this helper can be useful when
        // the factory is known to produce a Moq.Mock<T>.
        public static Moq.Mock<T> GetMockInstance<T>() where T: class
        {
            if (_factories.TryGetValue(typeof(T), out var factory))
            {
                var instance = factory();
                if (instance is Moq.Mock<T> moq)
                {
                    return moq;
                }
            }

            return null;
        }
    }
}