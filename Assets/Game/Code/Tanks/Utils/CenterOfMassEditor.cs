using Game.Code.Extensions;
using Game.Code.Tanks;
using UnityEngine;

namespace Game.Code.Editor
{
	[ExecuteInEditMode]
	public class CenterOfMassEditor : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private TankUnitView _tankView;

		[ContextMenu("Apply COM")]
		private void Apply()
		{
			Vector3 result = transform.position.Mult(_tankView.transform.localScale);
			_rigidbody.centerOfMass = result;
		}
		
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, 0.5f);
		}
	}
}