using System.Runtime.Serialization;

namespace Wzdx.v4.WorkZones
{
	/// <summary>
	/// The type of work zone road event.
	/// </summary>
	public enum WorkZoneType
	{
		/// <summary>
		/// The road event statically placed - not moving.
		/// </summary>
		[EnumMember(Value = "static")]
		Static = 1,
		/// <summary>
		/// The road event is actively moving on the roadway.As opposed to planned-moving-area, the road event geometry changes at the operation moves.
		/// </summary>
		[EnumMember(Value = "moving")]
		Moving = 2,
		/// <summary>
		/// The planned extent of a moving operation. The active work area will be somewhere within this road event. As opposed to moving, the road event geometry typically does not actively change.
		/// </summary>
		[EnumMember(Value = "planned-moving-area")]
		PlannedMovingArea = 3,
	}
}
