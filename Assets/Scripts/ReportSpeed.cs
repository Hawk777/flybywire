using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ReportSpeed : MonoBehaviour {
	[Tooltip("The animator to which the speed will be passed.")]
	public Animator animator;

	private void FixedUpdate() {
		animator.SetFloat("Speed", GetComponent<Rigidbody2D>().velocity.magnitude);
	}
}
