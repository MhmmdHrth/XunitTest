﻿using IntegrationTesting.Components;
using Xunit;

namespace IntegrationTesting
{
    [CollectionDefinition(nameof(AnimalCollection))]
    public class AnimalCollection : ICollectionFixture<AnimalFixture>
    {
    }
}
