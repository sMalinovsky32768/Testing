using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testing
{
    class ObservableCollectionConverter : JsonConverter<ObservableCollection<Test>>
    {
        public override ObservableCollection<Test> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                new ObservableCollection<Test>(JsonSerializer.Deserialize< ObservableCollection<Test>>(reader.ValueSpan.ToString()));

        public override void Write(
            Utf8JsonWriter writer,
            ObservableCollection<Test> value,
            JsonSerializerOptions options) =>
                JsonSerializer.Serialize< ObservableCollection<Test>>(value);
    }
}
