using UnityEngine;

namespace Game.Code.Extensions
{
	public static class VectorExtensions
	{
		public static Vector3 WithX(this Vector3 source, float value) 
			=> new(value, source.y, source.z);
		
		public static Vector3 WithY(this Vector3 source, float value) 
			=> new(source.x, value, source.z);
		
		public static Vector3 WithZ(this Vector3 source, float value) 
			=> new(source.x, source.y, value);

		public static Vector3 Mult(this Vector3 source, Vector3 other) =>
			new(source.x * other.x, source.y * other.y, source.z * other.z);
	}
}