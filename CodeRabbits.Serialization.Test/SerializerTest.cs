// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Runtime.Serialization;

namespace CodeRabbits.Serialization.Test;

public class DummyData
{
    public int? Id { get; set; }

    [DisplayName("UserName")]
    public string? Name { get; set; }

    [IgnoreDataMember]
    public string? Ignore { get; set; }
}

public class SerializerTest
{
    [Fact]
    public void SerializerIntegratedCase()
    {
        var list = new List<DummyData>
        {
            new DummyData { Id = 1, Name = "Jone", Ignore = "foo" },
            new DummyData { Id = 2, Name = "Anna", Ignore = "bar" },
        };

        var newline = Environment.NewLine;
        Assert.Equal(
            $"Id,UserName{newline}1,Jone{newline}2,Anna{newline}",
            Serializer.SerializeCsv(list)
       );
    }
}
