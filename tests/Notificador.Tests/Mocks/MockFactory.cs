using Notificador.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Notificador.Tests.Mocks
{

    internal static class MockFactory
    {
        private static readonly Dictionary<Type, Object> _mocks = new Dictionary<Type, Object>()
        {
            { typeof(IUmbriaClient), new UmbriaClientMock() }
        };

        public static T GetMock<T>()
        {
            if (_mocks.ContainsKey(typeof(T)))
            {
                return (T)_mocks[typeof(T)];
            }

            throw new NotImplementedException($"No mock implemented for type {typeof(T).FullName}");
        }
    }
}