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

        var lineType1 = Environment.NewLine;
        Assert.Equal(
            Serializer.SerializeCsv(list),
            $"Id,UserName{lineType1}1,Jone{lineType1}2,Anna{lineType1}"
       );
        Assert.Equal(
           Serializer.SerializeCsv(typeof(DummyData), list),
           $"Id,UserName{lineType1}1,Jone{lineType1}2,Anna{lineType1}"
      );

        var lineType2 = "\n";
        Assert.Equal(
            Serializer.SerializeCsv(list, lineType2),
            $"Id,UserName{lineType2}1,Jone{lineType2}2,Anna{lineType2}"
       );
    }
}
