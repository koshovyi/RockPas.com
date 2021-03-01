using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;

namespace RockPaperScissors.Common
{
	public class JsonConvert
	{

		private const int DefaultBlock = 4096;

		private static readonly JsonSerializer _serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
		{
			DateTimeZoneHandling = DateTimeZoneHandling.Utc,
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
		});

		public static T FromMessage<T>(byte[] message)
		{
			using (MemoryStream stream = new MemoryStream(message))
			{
				using (StreamReader textReader = new StreamReader(stream, Encoding.UTF8))
				{
					using (JsonTextReader reader = new JsonTextReader(textReader))
					{
						return _serializer.Deserialize<T>(reader);
					}
				}
			}
		}

		public static T FromMessage<T>(string message)
		{
			using (StringReader textReader = new StringReader(message))
			{
				using (JsonTextReader reader = new JsonTextReader(textReader))
				{
					return _serializer.Deserialize<T>(reader);
				}
			}
		}

		public static byte[] ToMessage<T>(T @object)
		{
			using (MemoryStream stream = new MemoryStream(DefaultBlock))
			{
				using (StreamWriter textWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					using (JsonTextWriter writer = new JsonTextWriter(textWriter))
					{
						_serializer.Serialize(writer, @object, typeof(T));
					}
				}

				return stream.ToArray();
			}
		}

		public static string ToMessageString<T>(T @object)
		{
			StringBuilder result = new StringBuilder(DefaultBlock);
			using (StringWriter textWriter = new StringWriter(result))
			{
				using (JsonTextWriter writer = new JsonTextWriter(textWriter))
				{
					_serializer.Serialize(writer, @object, typeof(T));
				}
			}

			return result.ToString();
		}

	}

}
